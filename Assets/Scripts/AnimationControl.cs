using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {

    public float speed = 0.1f;
    public Sprite[] originSprite;
    public Sprite[] walkSprites;
    public Sprite[] deathSprites;
    public Sprite[] jumpSprites;

    private Sprite[] nowSprites;
    //private bool forward = true;
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private int nowIndex = 0;
    private float time = 0.0f;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        nowSprites = originSprite;
        nowIndex = 0;
        //spriteRenderer.sprite = Sprites[0];
    }
	
	// Update is called once per frame
	void Update () {
        time += speed;
        if (time >= 1.0f) {
            UpdateSprite();

            time = 0.0f;
        }
	}

    private void UpdateSprite() {
        if (nowIndex == nowSprites.Length) {
            nowSprites = originSprite;
            nowIndex = 0;
        }
        spriteRenderer.sprite = nowSprites[nowIndex++];
    }

    public void Flip() {
        Vector3 scale = player.transform.localScale;
        player.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
    }

    public void Stop() {

        spriteRenderer.sprite = walkSprites[0];
    }

    public void Walk() {
        if (nowSprites != walkSprites) {
            nowSprites = walkSprites;
            nowIndex = 0;
        }
    }

    public void Die() {
        nowSprites = deathSprites;
        nowIndex = 0;
    }

    public void Jump()
    {
        nowSprites = jumpSprites;
        nowIndex = 0;
    }
}
