using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    private float scale = 100;
    private int open = 0;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (open>0 && scale > 0)
        {
            scale -= 6;
            if (scale < 0) { scale = 0; open = 0; }
            transform.localScale = new Vector3(scale/100, 1, 1);
        }

        if (open<0 && scale < 100)
        {
            print(scale);
            scale += 6;
            if (scale > 100) { scale = 100; open = 0; }
            transform.localScale = new Vector3(scale / 100, 1, 1);
        }
	}

    public void Open()
    {
        open = 1;
    }

    public void Close()
    {
        print("close");
        open = -1;
    }
}
