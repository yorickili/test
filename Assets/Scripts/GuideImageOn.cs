using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuideImageOn : MonoBehaviour
{
    public GameObject guideImage;
    public Sprite image;
    private PlayerControl playerControl;
    private GameObject hero;
    // Use this for initialization
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
        playerControl = hero.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (guideImage)
        {
            if (guideImage.activeSelf == false)
            {
                guideImage.SetActive(true);
                guideImage.GetComponent<Image>().sprite = image;
                playerControl.enabled = false;
                Destroy(this.gameObject);
            }
        }
        else
            print("null object");

    }

}
