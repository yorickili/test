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
    public float waveCost = 2f;

    public float moveForce = 365f;          // Amount of force added to move the player left and right.
    public float maxSpeed = 5f;             // The fastest the player can travel in the x axis.
                                            //public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
    public float jumpForce = 500f;
    public float damageForce = 50f;


    //private int tauntIndex;                 // The index of the taunts array indicating the most recent taunt.
    private Transform groundCheck;          // A position marking where to check if the player is grounded.
    //private bool grounded = false;          // Whether or not the player is grounded.
    private bool isJumping = false;
    private bool isMoving = false;
    private string walkDirection = "";
                                            

    private PlayerHealth playerHealth;
    private AnimationControl animationControl;
    private string forward = "right";
    

    void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
        //anim = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Start()
    {
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
    }

    private bool IsGround()
    {
        return Physics2D.Linecast(transform.position, groundCheck.position, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Neon")));
    }



    public void AddWave()
    {
        if (playerHealth.nowhealth > waveCost)   //当前电量大于发波所需电量
        {
            Vector3 WavePosition = new Vector3(this.transform.position.x, this.transform.position.y, 6);
            Instantiate(Wave, WavePosition, this.transform.rotation);
            playerHealth.ReduceHealth(waveCost);
            print("发波后 nowhealth=" + playerHealth.nowhealth);
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
        if (IsGround())
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            //animationControl.Jump();
            //isJumping = 5;
        }
    }

    private void WalkAtUpdate()
    {

        Vector3 dir = new Vector3(5, 0, 0);

        dir = Camera.main.transform.TransformDirection(dir);

        dir.y = 0;

        dir.Normalize();

        Vector3 sp = dir / 10;

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

    public void Walk(string direction)
    {
        walkDirection = direction;
        isMoving = true;
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
        this.enabled = false;
    }
}
