using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonMoving : MonoBehaviour {

    public bool yMoving, xMoving;
    public Vector2 yMinAndMax, xMinAndMax;
    public float ySpeed = 0.01f, xSpeed = 0.01f;

    private int yForward = 1, xForward = 1;

	// Use this for initialization
	void Start () {
        print(gameObject.transform.position.y);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 position = gameObject.transform.position;

        if (yMoving)
        {
            position.y += yForward * ySpeed;
            if (position.y > yMinAndMax.y)
            {
                position.y = yMinAndMax.y;
                yForward = -1;
            }
            else if (position.y < yMinAndMax.x)
            {
                position.y = yMinAndMax.x;
                yForward = 1;
            }
        }
        if (xMoving)
        {
            position.x += xForward * xSpeed;
            if (position.x > xMinAndMax.y)
            {
                position.x = xMinAndMax.y;
                xForward = -1;
            }
            else if (position.x < xMinAndMax.x)
            {
                position.x = xMinAndMax.x;
                xForward = 1;
            }
        }
        gameObject.transform.position = position;
    
	}
}
