using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Transform cameraTrans = GameObject.FindGameObjectWithTag("LightCamera").transform;
        this.transform.position = new Vector3(cameraTrans.position.x, cameraTrans.position.y, this.transform.position.z);
        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
	}
}
