using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMove : MonoBehaviour {

    public float speed = 0.001f;

    private float maxy = 1f, miny = -1f;
    private int forward = 1;

	// Use this for initialization
	void Start () {
		
	}

    void Move() {
        Vector3 position = this.transform.position;
        if (forward == 1 && position.y > maxy)
            forward = -1;
        else if (forward == -1 && position.y < miny)
            forward = 1;

        float y = position.y + forward * speed;
        position = new Vector3(position.x, y, position.z);
        this.transform.position = position;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
    }
}
