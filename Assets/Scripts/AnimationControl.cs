using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {

    public Sprite[] Sprites;
    public float speed = 0.1f;
    public bool forward = true;

    private SpriteRenderer spriteRenderer;
    private int nowIndex = 0;
    private float time = 0.0f;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprites[0];
    }
	
	// Update is called once per frame
	void Update () {
        time += speed;
        if (time >= 1.0f) {
            Walk();
            time = 0.0f;
        }
	}

    void Flip() {

    }

    void Stop() {
        spriteRenderer.sprite = Sprites[0];
    }

    void Walk() {
        nowIndex = (nowIndex + 1) % 8;
        spriteRenderer.sprite = Sprites[nowIndex];
    }
}
