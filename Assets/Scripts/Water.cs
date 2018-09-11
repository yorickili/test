using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    public GameObject spray;
    private GameObject hero;
    private PlayerControl playerControl;
    private PlayerHealth playerHealth;

    private bool islastinghurt = false;          //是否持续掉血
    public float hurtingtime = 0.2f;               //持续掉血属性：每秒掉多少血

    // Use this for initialization
    void Start () {
        hero = GameObject.FindGameObjectWithTag("Player");
        playerControl = hero.GetComponent<PlayerControl>();
        playerHealth = hero.GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        if (islastinghurt)
        {
            playerHealth.ReduceHealth(hurtingtime);
            //playerControl.ReduceHealth(hurtingtime);
            print("touch water" + playerHealth.nowhealth);

        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        float colPositionX = collision.transform.position.x-2f;
        Vector3 colPosition = new Vector3(colPositionX, transform.position.y, hero.transform.position.z);
        GameObject sprayclone =  Instantiate(spray, colPosition, this.transform.rotation) as GameObject;
        Destroy(sprayclone, 1);

        if (collision.tag == "Player")
        {
            islastinghurt = true;
           
        }
        else if(collision.name == "prop_box"){
           // Destroy(collision);
        }
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        islastinghurt = false;
    }
}
