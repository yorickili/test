using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btn_offGuideClick : MonoBehaviour {
    private GameObject guideImage;
    private PlayerControl playerControl;
    private GameObject hero;

    // Use this for initialization
    void Start () {
        guideImage = GameObject.Find("GuideImage");
        this.GetComponent<Button>().onClick.AddListener(clickToOffGuide);

        hero = GameObject.FindGameObjectWithTag("Player");
        playerControl = hero.GetComponent<PlayerControl>();
    }

    void clickToOffGuide()
    {

        print(guideImage.name);
        guideImage.SetActive(false);
        
        playerControl.enabled = true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
