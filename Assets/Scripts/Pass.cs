using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pass : MonoBehaviour
{

    public bool haveKey = false;

    // Use this for initialization
    void Start()
    {
        haveKey = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (haveKey)
                PassThisLevel();
        }
    }

    private void PassThisLevel() 
    {
        //todo
        GameObject.FindGameObjectWithTag("BlackBG").GetComponent<SetBlackBGTransform>().PassLevelAnimation();
    }
}
