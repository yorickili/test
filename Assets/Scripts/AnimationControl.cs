using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{

    public float speed = 0.1f;
    public Sprite[] originSprite;
    public Sprite[] walkLeftFootSprites;
    public Sprite[] walkRightFootSprites;
    public Sprite[] deathSprites;
    public Sprite[] jumpUpSprites;
    public Sprite[] jumpDownSprites;

    private Sprite[] nowSprites;
    //private bool forward = true;
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private int nowIndex = 0;
    private float time = 0.0f;
    private bool leftfoot = true;
    private Vector3 lastPosition;

    delegate void AnimationHandler();
    private AnimationHandler callback;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        nowSprites = originSprite;
        nowIndex = 0;
        //spriteRenderer.sprite = Sprites[0];

        callback = new AnimationHandler(SetOrigin);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += speed;
        if (time >= 1.0f)
        {
            UpdateSprite();

            time = 0.0f;
        }
    }

    private void UpdateSprite()
    {
        if (nowIndex == nowSprites.Length)
        {
            callback();
        }
        spriteRenderer.sprite = nowSprites[nowIndex++];
    }


    //call backs
    private void SetOrigin()
    {
        nowSprites = originSprite;
        nowIndex = 0;
    }

    private void DeathCallback()
    {
        GetComponent<PlayerControl>().enabled = false;
        //todo add death dialog
    }

    private void WalkNextFoot()
    {
        nowSprites = (leftfoot) ? walkRightFootSprites : walkLeftFootSprites;

        nowIndex = 0;
        leftfoot = !leftfoot;
        callback = new AnimationHandler(SetOrigin);
    }

    private void JumpCallback()
    {
        nowSprites = (player.transform.position.y > lastPosition.y) ? jumpUpSprites : jumpDownSprites;
        nowIndex = 0;
        lastPosition = player.transform.position;
    }


    //externed functions
    public void Flip()
    {
        Vector3 scale = player.transform.localScale;
        player.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
    }

    public void Stop()
    {
        SetOrigin();
        callback = new AnimationHandler(SetOrigin);
    }

    public void Walk()
    {
        if (nowSprites == walkLeftFootSprites || nowSprites == walkRightFootSprites)
        {
            //callback = new AnimationHandler(WalkNextFoot);
        }
        else
        {
            WalkNextFoot();
        }
    }

    public void Jump()
    {
        nowSprites = jumpUpSprites;
        nowIndex = 0;
        lastPosition = player.transform.position;
        callback = new AnimationHandler(JumpCallback);
    }

    public void TakeDamage()
    {

    }

    public void Die()
    {
        nowSprites = deathSprites;
        nowIndex = 0;
        callback = new AnimationHandler(DeathCallback);
    }

}
