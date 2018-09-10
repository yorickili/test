using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toucher : MonoBehaviour {

    private ETCTouchPad Joystick;
    private ETCTouchPad TouchPad;

    private bool isMoving = false;
    private bool isTouching = false;
    private string moveDirection;
    private Transform wallCheck;
    private bool walled = false;

    private void Awake()
    {
        wallCheck = transform.Find("wallCheck");
    }
    // Use this for initialization
    void Start () {
        Joystick = ETCInput.GetControlTouchPad("Joystick");
        TouchPad = ETCInput.GetControlTouchPad("TouchPad");
        Joystick.onTouchStart.AddListener(OnJoystickTouchStart);
        Joystick.onTouchUp.AddListener(OnJoystickTouchUp);
        Joystick.OnPressRight.AddListener(OnJoystickPressRight);
        Joystick.OnPressLeft.AddListener(OnJoystickPressLeft);
        TouchPad.onTouchStart.AddListener(OnTouchPadTouchStart);
        TouchPad.onTouchUp.AddListener(OnTouchPadTouchUp);
        TouchPad.OnDownUp.AddListener(OnTouchPadDownUp);
    }
	
	// Update is called once per frame
	void Update () {
        //walled = Physics2D.Linecast(transform.position, wallCheck.position, (1 << LayerMask.NameToLayer("Neon")));
        //Debug.Log(moveDirection);
        //if (!walled && moveDirection == "right") {
        //    Move();
        //}
        Move();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<PlayerControl>().Jump();
        }
        if (Input.GetKey(KeyCode.A))
        {
            isMoving = true;
            moveDirection = "left";
            Move();
            isMoving = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            moveDirection = "right";
            Move();
            isMoving = false;
        }
    }

    void OnJoystickTouchStart ()
    {
        isMoving = true;
    }

    void OnJoystickTouchUp()
    {
        isMoving = false;
    }

    void OnJoystickPressRight ()
    {
        moveDirection = "right";
    }

    void OnJoystickPressLeft()
    {
        moveDirection = "left";
    }

    void Move()
    {
        if (isMoving)
        {
            GetComponent<PlayerControl>().Walk(moveDirection);
        } 
        else 
        {
            GetComponent<PlayerControl>().Stop();
        }
    }

    void OnTouchPadTouchStart ()
    {
        isTouching = false;
    }

    void OnTouchPadTouchUp ()
    {
        if (!isTouching) {
            GetComponent<PlayerControl>().AddWave();
        }
    }

    void OnTouchPadDownUp ()
    {
        isTouching = true;
        GetComponent<PlayerControl>().Jump();
    }
}
