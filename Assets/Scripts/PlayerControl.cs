﻿using UnityEngine;
using System.Collections;
using System;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;         // For determining which way the player is currently facing.
    [HideInInspector]
    public bool jump = false;               // Condition for whether the player should jump.
    public GameObject Wave;

    public float moveForce = 365f;          // Amount of force added to move the player left and right.
    public float maxSpeed = 5f;             // The fastest the player can travel in the x axis.
                                            //public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
    public float jumpForce = 500f;          // Amount of force added when the player jumps.
                                            //public AudioClip[] taunts;				// Array of clips for when the player taunts.
                                            //public float tauntProbability = 50f;	// Chance of a taunt happening.
                                            //public float tauntDelay = 1f;			// Delay for when the taunt should happen.


    private int tauntIndex;                 // The index of the taunts array indicating the most recent taunt.
    private Transform groundCheck;          // A position marking where to check if the player is grounded.
    private bool grounded = false;          // Whether or not the player is grounded.
                                            //private Animator anim;					// Reference to the player's animator component.

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

    }

    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        // If the jump button is pressed and the player is grounded then the player should jump.
        //Move();
        //if (Input.GetButtonDown("Jump"))
        //    Jump();
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

 

    public void AddWave()
    {
        if (playerHealth.nowhealth > playerHealth.waveCost)   //当前电量大于发波所需电量
        {
            Vector3 WavePosition = new Vector3(this.transform.position.x, this.transform.position.y, 6);
            Instantiate(Wave, WavePosition, this.transform.rotation);
            playerHealth.WaveReduceHealth();
            print("发波后 nowhealth=" + playerHealth.nowhealth);

        }
        else
        {
            print("发波失败 nowhealth=" + playerHealth.nowhealth);
        }
    }

    public void Jump()
    {
        //isJumping = true;
        //print(grounded);
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Neon")));
        if (grounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
    }

    public void Walk(string direction)
    {
        Vector3 dir = new Vector3(5, 0, 0);

        dir = Camera.main.transform.TransformDirection(dir);

        dir.y = 0;

        dir.Normalize();

        Vector3 sp = dir / 10;

        if (direction == "right")
        {
            this.transform.position += sp;
        }
        else if (direction == "left")
        {
            this.transform.position -= sp;
        }
    }

    public void Stop()
    {

    }
}
