using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    private float scale = 100;
    private bool open = false;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (open && scale > 0)
        {
            scale -= 6;
            if (scale < 0) { scale = 0; open = false; }
            transform.localScale = new Vector3(scale/100, 1, 1);
        }
	}

    public void Open()
    {
        open = true;
    }
}
