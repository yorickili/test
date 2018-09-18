using UnityEngine;
using System.Collections;
using System;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;               // Condition for whether the player should jump.
    public bool isSquat = false;
    public bool haveKey = false;
    public GameObject Wave;
    public float waveCost = 2f;

    public float moveForce = 365f;          // Amount of force added to move the player left and right.
    public float maxSpeed = 5f;             // The fastest the player can travel in the x axis.
                                            //public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
    public float jumpForce = 500f;
    public float damageForce = 500f;

    public int neonOn = 0;

    public GameObject interLevelBtn;
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
    private float damageLastTime = 0;
                                            

    private PlayerHealth playerHealth;
    private AnimationControl animationControl;
    private AudioManager audioManager;
    private CameraFollow cameraFollow;
    private string forward = "right";
    private Color changePartColor = new Color(1, 1, 1, 0);

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
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void FixedUpdate()
    {
        bool isGround = IsGround();
        if (isJumping && isGround)
        {
            isJumping = false;
            animationControl.Stop();
            audioManager.PlaySoundAudio(audioManager.downAudio, transform.position);
        }
        else if (!isJumping && !isGround)
        {
            isJumping = true;
            animationControl.Jump();
            audioManager.PlaySoundAudio(audioManager.jumpAudio, transform.position);
        }

        if (isMoving)
            WalkAtUpdate();

        if (lastMoving > 0)
            --lastMoving;

        cameraFollow.TrackPlayer();

        //take damage
        if (damageLastTime > 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Sin(damageLastTime) / 3 + 0.5f);
            damageLastTime -= 0.1f;
            if (damageLastTime <= 0)
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        //neonON
        if (neonOn > 0)
        {
            audioManager.PlayNeonAudio();
            neonOn--;
        }
        else 
        {
            audioManager.StopNeonAudio();
        }

        if (changePartColor.a > 0.01)
        {
            changePartColor.a -= 0.01f;
            if (changePartColor.a < 0.01) 
            {
                changePartColor.a = 0;
                //GameObject.FindGameObjectWithTag("Finish").GetComponent<SpriteRenderer>().color = changePartColor;
                GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>().clearFlags = CameraClearFlags.Nothing;
            }
        }
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
        if (!this.enabled)
            return;

        if (!GameObject.Find("WaveUI").GetComponent<WaveCD>().Turn())
            return;

        //if (playerHealth.nowhealth > waveCost)   //当前电量大于发波所需电量
        //{
            Vector3 WavePosition = new Vector3(this.transform.position.x, this.transform.position.y, 6);
            Instantiate(Wave, WavePosition, this.transform.rotation);
            //GameObject.Find("Battery").GetComponent<Battery>().HandleElectric(-1 * waveCost);
            playerHealth.ReduceHealth(waveCost);

            //waveLast = waveCD;
        //}
        //else
        //{
        //    print("发波失败 nowhealth=" + playerHealth.nowhealth);
        //}

        audioManager.PlaySoundAudio(audioManager.waveAudio, transform.position);
    }

    public void PickUpBattery(float delta)
    {
        playerHealth.IncreaseHealth(delta);
        audioManager.PlaySoundAudio(audioManager.pickupAudio, transform.position);
    }

    public void PickUpKey()
    {
        haveKey = true;
        audioManager.PlaySoundAudio(audioManager.pickkeyAudio, transform.position);
    }

    public void ChangePart()
    {
        changePartColor.a = 1;
        //GameObject.FindGameObjectWithTag("Finish").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        audioManager.PlaySoundAudio(audioManager.changepartAudio, transform.position);
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
        if (!this.enabled) return;
        if (IsGround() || IsSlope())
        {
            print("isMoving: "+isMoving);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce + (lastMoving>0 ? 1000f : 0f)));
            wallJumpCount = 0;
            //animationControl.Jump();
            //isJumping = 5;
        }
        else if (isWallNear)
        {
            if (wallJumpCount < 1)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce + (lastMoving>0 ? 1000f : 0f)));
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
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 800f));

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

        if (!isJumping)
            animationControl.Walk();

        isMoving = false;
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

    public void TakeDamage(float delta, bool alwaysDamage = false)
    {
        if (!alwaysDamage && damageLastTime > 0.0001) return;

        if (forward == "right")
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-damageForce/2, 0f));
        else
            GetComponent<Rigidbody2D>().AddForce(new Vector2(damageForce/2, 0f));
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, damageForce));

        if (!alwaysDamage)
            damageLastTime = 2 * 2 * Mathf.PI;
        animationControl.TakeDamage();
        playerHealth.ReduceHealth(delta);
    }

    public void Death()
    {
        animationControl.Die();
        GetComponent<CapsuleCollider2D>().size = new Vector2(6.4f, 4f);
        this.enabled = false;
        if (!interLevelBtn.activeSelf)
        {
            // Instantiate(interLevelBtn, transform.position, transform.rotation);
            interLevelBtn.SetActive(true);
        }

    }

    public void Squat()
    {
        GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.32f, -1.35f);
        GetComponent<CapsuleCollider2D>().size = new Vector2(6.4f, 13.3f);
        GetComponent<BoxCollider2D>().offset = new Vector2(2.26f, 0.10f);
        GetComponent<BoxCollider2D>().size = new Vector2(3.39f, 7.46f);
        isSquat = true;
    }

    public void Stand()
    {
        GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.32f, -0.17f);
        GetComponent<CapsuleCollider2D>().size = new Vector2(6.4f, 15.6f);
        GetComponent<BoxCollider2D>().offset = new Vector2(2.26f, 1.18f);
        GetComponent<BoxCollider2D>().size = new Vector2(3.39f, 9.63f);
        isSquat = false;
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
