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

    void Start () {
        Joystick = ETCInput.GetControlTouchPad("Joystick");
        TouchPad = ETCInput.GetControlTouchPad("TouchPad");
        Joystick.onTouchStart.AddListener(OnJoystickTouchStart);
        Joystick.onMove.AddListener(OnJoyStickMove);
        Joystick.onTouchUp.AddListener(OnJoystickTouchUp);
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

    void OnJoystickTouchStart ()
    {
        isMoving = true;
    }

    void OnJoyStickMove (Vector2 vector)
    {
        Debug.Log("joystick moving" + vector);
        if (vector.x > 4) {
            direction = "right";
        }
        else if (vector.x < -4) {
            direction = "left";
        }
        Debug.Log(vector.x);
    }

    void OnJoystickTouchUp()
    {
        isMoving = false;
    }

    void OnTouchPadTouchStart ()
    {
        isTouching = false;
    }

    void OnTouchPadTouchUp ()
    {
        if (!isTouching && this.enabled)
        {
            GetComponent<PlayerControl>().AddWave();
        }
    }

    void OnTouchPadDownUp ()
    {
        isTouching = true;
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
