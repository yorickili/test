using UnityEngine;
using System.Collections;

public class Stab : MonoBehaviour
{
    private GameObject hero;
    private PlayerControl playerControl;
    //private PlayerHealth playerHealth;
    public float damage;
    private bool isInDamage = false;

    // Use this for initialization
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
        playerControl = hero.GetComponent<PlayerControl>();
        //playerHealth = hero.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInDamage)
        {
            playerControl.TakeDamage(damage);
            //print("touch stab.nowhealth" + playerHealth.nowhealth);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInDamage = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            isInDamage = false;
    }
}
