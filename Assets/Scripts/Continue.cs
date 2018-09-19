using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Continue : MonoBehaviour {

    private Image image;
    private Color color;
    private float frame;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        color = new Color(1, 1, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
        if (frame >= Mathf.PI / 2)
        {
            frame -= Mathf.PI / 2;
        }
        else
        {
            frame += Time.deltaTime;
            color.a = Mathf.Sin(frame*4) / 3 + 0.5f;
            image.color = color;
        }
	}
}
