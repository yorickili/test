using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroImages : MonoBehaviour {

    public Sprite[] animations;
    public Sprite[] guides;
    public Sprite bg;
    public bool isStart = true;

    private ETCTouchPad TouchPad;
    private bool isTouching = false;
    private int animationFrame = 0;
    private float now = 0;
    private int guideFrame = 0;

	// Use this for initialization
	void Start () {
        if (isStart)
            GetComponent<Image>().sprite = animations[0];
        else
        {
            GetComponent<Image>().sprite = bg;
            animationFrame = -1;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Toucher>().enabled = false;
        }

        TouchPad = ETCInput.GetControlTouchPad("TouchPad");

        TouchPad.onTouchStart.AddListener(OnTouchPadTouchStart);
        TouchPad.onTouchUp.AddListener(OnTouchPadTouchUp);
        TouchPad.OnDownUp.AddListener(OnTouchPadDownUp);
        TouchPad.OnDownDown.AddListener(OnTouchPadDownDown);
    }

    void OnTouchPadDownUp()
    {
        isTouching = true;
    }

    void OnTouchPadTouchStart()
    {
        isTouching = false;
    }

    void OnTouchPadDownDown()
    {
        isTouching = true;
    }

    void OnTouchPadTouchUp()
    {
        if (!isTouching)
        {
            if (isStart) Next();
            else StartLevel();
        }
    }

    // Update is called once per frame
    void Update () {
        if (animationFrame >= 0)
        {
            if (now >= 0.1)
            {
                ++animationFrame;
                if (animationFrame == animations.Length)
                    animationFrame = 0;
                GetComponent<Image>().sprite = animations[animationFrame];
                now = 0;
            }
            else
            {
                now += Time.deltaTime;
            }
        }
    }

    void StartLevel()
    {
        gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Toucher>().enabled = true;
    }

    void Next() {
        if (guideFrame == guides.Length)
            return;

        if (animationFrame >= 0)
        {
            animationFrame = -1;
            guideFrame = 0;
        }
        else 
        {
            ++guideFrame;
            if (guideFrame == guides.Length)
            {
                //GetComponent<Image>().sprite = bg;
                //GetComponent<MenuEvent>().enabled = true;
                //GameObject.Find("EasyTouchControlsCanvas").SetActive(false);
                SceneManager.LoadScene("Level0");

                return;
            }
        }

        GetComponent<Image>().sprite = guides[guideFrame];
    }
}
