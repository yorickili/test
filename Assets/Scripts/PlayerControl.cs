using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	public GameObject Wave;

	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	//public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 500f;			// Amount of force added when the player jumps.
	//public AudioClip[] taunts;				// Array of clips for when the player taunts.
	//public float tauntProbability = 50f;	// Chance of a taunt happening.
	//public float tauntDelay = 1f;			// Delay for when the taunt should happen.


	private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;          // Whether or not the player is grounded.
                                            //private Animator anim;					// Reference to the player's animator component.

    private ETCJoystick moveJoystic;

    private PlayerHealth playerHealth;


    void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
        //anim = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
	}

    void Start()
    {
        moveJoystic = ETCInput.GetControlJoystick("New Joystick");
    }

    void Update()
	{
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        // If the jump button is pressed and the player is grounded then the player should jump.
        Move();
        if (Input.GetButtonDown("Jump"))
            Jump();
	}


	void FixedUpdate ()
	{
		// Cache the horizontal input.
		//float h = Input.GetAxis("Horizontal");

		//// The Speed animator parameter is set to the absolute value of the horizontal input.
		////anim.SetFloat("Speed", Mathf.Abs(h));

		//// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		//if(h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
		//	// ... add a force to the player.
		//	GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

		//// If the player's horizontal velocity is greater than the maxSpeed...
		//if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
		//	// ... set the player's velocity to the maxSpeed in the x axis.
		//	GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		//// If the input is moving the player right and the player is facing left...
		//if(h > 0 && !facingRight)
		//	// ... flip the player.
		//	Flip();
		//// Otherwise if the input is moving the player left and the player is facing right...
		//else if(h < 0 && facingRight)
			//// ... flip the player.
			//Flip();
            
	}
	
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    /*
	public IEnumerator Taunt()
	{
		// Check the random chance of taunting.
		float tauntChance = Random.Range(0f, 100f);
		if(tauntChance > tauntProbability)
		{
			// Wait for tauntDelay number of seconds.
			yield return new WaitForSeconds(tauntDelay);

			// If there is no clip currently playing.
			if(!GetComponent<AudioSource>().isPlaying)
			{
				// Choose a random, but different taunt.
				tauntIndex = TauntRandom();

				// Play the new taunt.
				//GetComponent<AudioSource>().clip = taunts[tauntIndex];
				//GetComponent<AudioSource>().Play();
			}
		}
	}


	int TauntRandom()
	{
		// Choose a random index of the taunts array.
		int i = Random.Range(0, taunts.Length);

		// If it's the same as the previous taunt...
		if(i == tauntIndex)
			// ... try another random taunt.
			return TauntRandom();
		else
			// Otherwise return this index.
			return i;
	}*/

	public void AddWave ()
	{
        if (playerHealth.nowhealth > playerHealth.waveCost)   //当前电量大于发波所需电量
        {
            Vector3 WavePosition = new Vector3(this.transform.position.x, this.transform.position.y, 6);
            Instantiate(Wave, WavePosition, this.transform.rotation);
            playerHealth.WaveReduceHealth();
            print("发波后 nowhealth="+playerHealth.nowhealth);

        }
        else
        {
            print("发波失败 nowhealth=" + playerHealth.nowhealth);
        }
	}

    public void Jump ()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (grounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
    }

    public void Move()
    {
        float h = moveJoystic.axisX.axisValue;

        if (h != 0)
        {
            //获取摇杆的方向

            //Vector3 dir = new Vector3(h, 0, 0);

            ////把方向转换到相机的坐标系中

            //dir = Camera.main.transform.TransformDirection(dir);

            ////方向的y轴值始终设为0

            //dir.y = 0;

            ////把方向向量归一化

            //dir.Normalize();

            ////指定一个4倍的向量

            //Vector3 sp = dir / 10;

            //this.transform.position += sp;

            if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
                // ... add a force to the player.
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

            // If the player's horizontal velocity is greater than the maxSpeed...
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
                // ... set the player's velocity to the maxSpeed in the x axis.
                GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (h > 0 && !facingRight)
                // ... flip the player.
                Flip();
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (h < 0 && facingRight)
                // ... flip the player.
                Flip();
        }
    }
}
