using UnityEngine;
using System.Collections;

public class Stab : MonoBehaviour
{
    private GameObject hero;
    private PlayerControl playerControl;
    private PlayerHealth playerHealth;
    public float damage;

    // Use this for initialization
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
        playerControl = hero.GetComponent<PlayerControl>();
        playerHealth = hero.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerHealth.ReduceHealth(damage);
        print("touch stab.nowhealth"+playerHealth.nowhealth);
        //playerControl.ReduceHealth(damage);
    }
}
