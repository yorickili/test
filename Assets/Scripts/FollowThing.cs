using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThing : MonoBehaviour {
    public GameObject thing;
    private Vector3 origin;

	// Use this for initialization
	void Start () {
        origin = this.transform.position - thing.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.transform.position = origin + thing.transform.position;
	}
}
