using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    public GameObject spray;
    private GameObject hero;
    private PlayerControl playerControl;
    private PlayerHealth playerHealth;

    private bool isPlayerInwater = false;          //是否持续掉血
    public float hurtingtime = 0.2f;               //持续掉血属性：每秒掉多少血
    private bool isBoxInWater = false;

    // Use this for initialization
    void Start () {
        hero = GameObject.FindGameObjectWithTag("Player");
        playerControl = hero.GetComponent<PlayerControl>();
        playerHealth = hero.GetComponent<PlayerHealth>();


	}
	
	// Update is called once per frame
	void Update () {
        if (isPlayerInwater)
        {
              playerHealth.ReduceHealth(hurtingtime);
            // print("touch water" + playerHealth.nowhealth);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerControl.TakeDamage(hurtingtime, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
   
       // float sparyWidth = spray.GetComponent<Renderer>().bounds.size.x*spray.transform.localScale.x/2;
        float waterWidth = GetComponent<Renderer>().bounds.size.x;
        float waterLeftbbounds = transform.position.x-waterWidth/2;
        float waterRightbbounds = transform.position.x + waterWidth/2 ;
       // print(waterLeftbbounds);
       // print(waterRightbbounds);
        float colPositionX = collision.collider.transform.position.x;

        //print(sparyWidth);
        float sparyWidth = 2.5f;
        if (collision.collider.tag == "Player") {
            if (!isPlayerInwater)
            {
                isPlayerInwater = true;
                float leftOff = 0;
                print(colPositionX);
                if (colPositionX<(waterLeftbbounds+sparyWidth))
                {
                   // print("1");
                   leftOff = waterLeftbbounds ;
                   Vector3 colPosition = new Vector3(leftOff, transform.position.y, hero.transform.position.z);
                    GameObject sprayclone = Instantiate(spray, colPosition, this.transform.rotation) as GameObject;
                    Destroy(sprayclone, 0.5f);
                }
                else if (colPositionX > (waterRightbbounds - sparyWidth))
                {
                   // print("2");
                    leftOff = waterRightbbounds-sparyWidth;
                    Vector3 colPosition = new Vector3(leftOff, transform.position.y, hero.transform.position.z);
                    GameObject sprayclone = Instantiate(spray, colPosition, this.transform.rotation) as GameObject;
                    Destroy(sprayclone, 0.5f);
                }
                else
               {
                    // print("3");
                     leftOff = sparyWidth/2 ;
                     Vector3 colPosition = new Vector3(colPositionX - leftOff, transform.position.y, hero.transform.position.z);
                     GameObject sprayclone = Instantiate(spray, colPosition, this.transform.rotation) as GameObject;

                     Destroy(sprayclone, 0.5f);
                }
               
               
               
            }
         }
        else if(collision.collider.name == "prop_box")
        {
            if (!isBoxInWater)
            {
                isBoxInWater = true;

                float leftOff = sparyWidth/2;
                Vector3 colPosition = new Vector3(collision.collider.transform.position.x -leftOff, transform.position.y, hero.transform.position.z);
                GameObject sprayclone = Instantiate(spray, colPosition, this.transform.rotation) as GameObject;
                Destroy(sprayclone, 0.50f);
            }
        }          
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        isPlayerInwater = false;

    }
}
