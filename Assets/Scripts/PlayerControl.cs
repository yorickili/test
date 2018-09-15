using UnityEngine;
using System.Collections;
using System;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;               // Condition for whether the player should jump.
    public bool haveKey = false;
    public GameObject Wave;
    private int waveCD = 100;
    public float waveCost = 2f;
    private int waveLast = 0;

    public float moveForce = 365f;          // Amount of force added to move the player left and right.
    public float maxSpeed = 5f;             // The fastest the player can travel in the x axis.
                                            //public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
    public float jumpForce = 500f;
    public float damageForce = 50f;


    //private int tauntIndex;                 // The index of the taunts array indicating the most recent taunt.
    //private Transform groundCheck;          // A position marking where to check if the player is grounded.

    //private Transform wallCheck;
    //private bool grounded = false;          // Whether or not the player is grounded.
    private bool isJumping = false;
    private bool isMoving = false;
    private int lastMoving = 0;
    //private bool isTiptoeOnGround = false;
    //private bool isHeelOnGround = false;
    private bool isWallNear = false;
    private bool isNeonNear = false;
    private int wallJumpCount = 0;
    private string walkDirection = "";
                                            

    private PlayerHealth playerHealth;
    private AnimationControl animationControl;
    private CameraFollow cameraFollow;
    private string forward = "right";
    

    void Awake()
    {
        //wallCheck = transform.Find("wallCheck");
        //anim = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        //Debug.Log("111111111" + PlayerPrefs.GetInt("round"));
        //PlayerPrefs.SetInt("round", 1);
    }

    void Start()
    {
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        animationControl = GameObject.FindGameObjectWithTag("AnimationManager").GetComponent<AnimationControl>();
    }

    void FixedUpdate()
    {
        bool isGround = IsGround();
        if (isJumping && isGround)
        {
            isJumping = false;
            animationControl.Stop();
        }
        else if (!isJumping && !isGround)
        {
            isJumping = true;
            animationControl.Jump();
        }

        if (isMoving)
            WalkAtUpdate();

        if (waveLast > 0)
            --waveLast;
        if (lastMoving > 0)
            --lastMoving;

        cameraFollow.TrackPlayer();
    }

    private bool IsGround()
    {
        Vector3 ground = new Vector3(0, -8.2f, 0);
        Vector3 groundCheck = new Vector3(ground.x * transform.localScale.x, ground.y * transform.localScale.y, ground.z * transform.localScale.z) + transform.position;
        return Physics2D.Linecast(transform.position, groundCheck, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Neon")));
    }

    private bool IsSlope()
    {
        Vector3 tiptoe = new Vector3(2.2f, -8.2f, 0);
        Vector3 heel = new Vector3(-2.2f, -8.2f, 0);
        Vector3 tiptoeCheck = new Vector3(tiptoe.x * transform.localScale.x, tiptoe.y * transform.localScale.y, tiptoe.z * transform.localScale.z) + transform.position;
        Vector3 heelCheck = new Vector3(heel.x * transform.localScale.x, heel.y * transform.localScale.y, heel.z * transform.localScale.z) + transform.position;

        bool tiptoe1 = Physics2D.Linecast(transform.position, tiptoeCheck, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Neon")));
        bool heel1 = Physics2D.Linecast(transform.position, heelCheck, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Neon")));
        Debug.Log("tiptoe:" + tiptoe1);
        Debug.Log("heel: "+ heel1);
        return (tiptoe1 || heel1) && !(tiptoe1 && heel1);
    }

    private bool IsUpstair()
    {
        Vector3 tiptoe = new Vector3(1.8f, -7.5f, 0);
        Vector3 heel = new Vector3(-1.8f, -7.5f, 0);
        Vector3 tiptoeCheck = new Vector3(tiptoe.x * transform.localScale.x, tiptoe.y * transform.localScale.y, tiptoe.z * transform.localScale.z) + transform.position;
        Vector3 heelCheck = new Vector3(heel.x * transform.localScale.x, heel.y * transform.localScale.y, heel.z * transform.localScale.z) + transform.position;

        bool tiptoe1 = Physics2D.Linecast(transform.position, tiptoeCheck, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Neon")));
        bool heel1 = Physics2D.Linecast(transform.position, heelCheck, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Neon")));
        return (tiptoe1 && !heel1);
    }

    public void AddWave()
    {
        if (waveLast > 0)
        {
            return;
        }

        if (playerHealth.nowhealth > waveCost)   //当前电量大于发波所需电量
        {
            Vector3 WavePosition = new Vector3(this.transform.position.x, this.transform.position.y, 6);
            Instantiate(Wave, WavePosition, this.transform.rotation);
            playerHealth.ReduceHealth(waveCost);

            waveLast = waveCD;
        }
        else
        {
            print("发波失败 nowhealth=" + playerHealth.nowhealth);
        }
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Jump()
    {
        if (IsGround() || IsSlope())
        {
            print("isMoving: "+isMoving);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce + (lastMoving>0 ? 100f : 0f)));
            wallJumpCount = 0;
            //animationControl.Jump();
            //isJumping = 5;
        }
        else if (isWallNear)
        {
            if (wallJumpCount < 1)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce + (lastMoving>0 ? 100f : 0f)));
                wallJumpCount += 1;
            }
        }
    }

    private void WalkAtUpdate()
    {
        Vector3 dir = new Vector3(5, 0, 0);

        dir = GameObject.FindGameObjectWithTag("MainCamera").transform.TransformDirection(dir);

        dir.y = 0;

        if (IsUpstair()) 
        {
            dir.y = -10;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 15f));

        }

        dir.Normalize();

        Vector3 sp = dir / 10;

        if (!isWallNear && !isNeonNear)
        {
            if (walkDirection != forward)
            {
                forward = walkDirection;
                animationControl.Flip();
            }

            if (walkDirection == "right")
            {
                this.transform.position += sp;
            }
            else if (walkDirection == "left")
            {
                this.transform.position -= sp;
            }

            if (!isJumping)
                animationControl.Walk();

            isMoving = false;
        }
        else
        {
            string _forward = (transform.localScale.x > 0) ? "right" : "left";
            if (_forward == "right" && walkDirection == "left")
            {
                this.transform.position -= sp;
            }
            else if (_forward == "left" && walkDirection == "right")
            {
                this.transform.position += sp;
            }
        }
    }

    public void Walk(string direction)
    {
        walkDirection = direction;
        isMoving = true;
        lastMoving = 10;
    }

    public void Stop()
    {

    }

    public void TakeDamage(float delta)
    {
        playerHealth.ReduceHealth(delta);
        if (forward == "right")
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-damageForce, 0f));
        else
            GetComponent<Rigidbody2D>().AddForce(new Vector2(damageForce, 0f));
        animationControl.TakeDamage();
    }

    public void Death()
    {
        animationControl.Die();
        GetComponent<CapsuleCollider2D>().size = new Vector2(6.4f, 4f);
        this.enabled = false;
    }

    public void Squat()
    {
        GetComponent<CapsuleCollider2D>().size = new Vector2(6.4f, 5f);
    }

    public void Stand()
    {
        GetComponent<CapsuleCollider2D>().size = new Vector2(6.4f, 14f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isWallNear = true;
        }
        if (collision.gameObject.tag == "Neon")
        {
            isNeonNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isWallNear = false;
        }
        if (collision.gameObject.tag == "Neon")
        {
            isNeonNear = false;
        }
    }
}
