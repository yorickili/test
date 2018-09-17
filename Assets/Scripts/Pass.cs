using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pass : MonoBehaviour
{
    public int nextPart = -1;
    public GameObject nextEnter;
    private PlayerControl playerControl;

    // Use this for initialization
    void Start()
    {
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (playerControl.haveKey)
                PassThisPart();
        }
    }

    private void PassThisPart()
    {
        if (nextPart < 0)
        {
            PassThisLevel();
        }

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().ChangePart(nextPart);
        GameObject.FindGameObjectWithTag("Player").transform.position = nextEnter.transform.position;
        playerControl.haveKey = false;
    }

    private void PassThisLevel() 
    {
        //todo
        PlayerPrefs.SetInt("Level", -nextPart);
    }
}
