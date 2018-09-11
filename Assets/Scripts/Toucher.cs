using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toucher : MonoBehaviour {

    private ETCTouchPad Joystick;
    private ETCTouchPad TouchPad;

    private bool isMoving = false;
    private bool isTouching = false;
    private bool isSquating = false;
    private string direction;
    private Transform wallCheck;
    private Transform groundCheck1;
    private Transform groundCheck2;
    private bool grounded1 = false;
    private bool grounded2 = false;
    private bool walled = false;

    void Awake()
    {
        wallCheck = transform.Find("wallCheck");
        groundCheck1 = transform.Find("groundCheck1");
        groundCheck2 = transform.Find("groundCheck2");
    }

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
        TouchPad.OnDownDown.AddListener(OnTouchPadDownDown);
    }
	
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<PlayerControl>().Jump();
        }
        if (Input.GetKey(KeyCode.A))
        {
            isMoving = true;
            direction = "left";
        }
        if (Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            direction = "right";
        }

        walled = Physics2D.Linecast(transform.position, wallCheck.position, (1 << LayerMask.NameToLayer("Neon")));
        if (!walled) {
            if (isMoving)
            {
                GetComponent<PlayerControl>().Walk(direction);
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    isMoving = false;
                }
            }
            else
            {
                GetComponent<PlayerControl>().Stop();
            }
        }
        else
        {
            Vector3 right = new Vector3(0, 0, 1f);
            Vector3 left = new Vector3(0, 0, -1f);
            if (this.transform.forward == right && direction == "left")
            {
                GetComponent<PlayerControl>().Walk("left");
            }
            else if (this.transform.forward == left && direction == "right")
            {
                GetComponent<PlayerControl>().Walk("right");
            }
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
        direction = "right";
    }

    void OnJoystickPressLeft()
    {
        direction = "left";
    }

    void OnTouchPadTouchStart ()
    {
        isTouching = false;
    }

    void OnTouchPadTouchUp ()
    {
        if (!isTouching)
        {
            GetComponent<PlayerControl>().AddWave();
        }
    }

    void OnTouchPadDownUp ()
    {
        isTouching = true;
        //grounded1 = Physics2D.Linecast(transform.position, groundCheck1.position, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Neon")));
        //grounded2 = Physics2D.Linecast(transform.position, groundCheck2.position, (1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Neon")));
        //Debug.Log(grounded1 || grounded2);
        //Debug.Log(grounded2);
        if (isSquating)
        {
            GetComponent<PlayerControl>().Stand();
            isSquating = false;
        }
        else
        {
            GetComponent<PlayerControl>().Jump();
        }
    }

    void OnTouchPadDownDown()
    {
        isTouching = true;
        isSquating = true;
        GetComponent<PlayerControl>().Squat();
    }
}
