using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBlackBGTransform : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RectTransform rectTransform = this.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Screen.width / 5, Screen.height / 5);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
