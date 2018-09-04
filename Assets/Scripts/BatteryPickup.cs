using UnityEngine;
using System.Collections;

public class BatteryPickup : MonoBehaviour
{

    public float healthBonus=10f;               // How much health the crate gives the player.当前电量增加值
   // public AudioClip collect;               // The sound of the crate being collected.
    public float staytime=5f;                  //staytime


    private PickupSpawner pickupSpawner;    // Reference to the pickup spawner.
    //private Animator anim;                  // Reference to the animator component.
    private bool landed;					// Whether or not the crate has landed.

    private void Awake()
    {
        pickupSpawner = GameObject.Find("pickupManager").GetComponent<PickupSpawner>();
        //anim = transform.root.GetComponent<Animator>();
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
           // print("player");
            // Get a reference to the player health script.
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            playerHealth.health += healthBonus;
            playerHealth.health = Mathf.Clamp(playerHealth.health, 0f, playerHealth.maxhealth);

            print("add health:"+playerHealth.health);

            // Update the health bar.
            playerHealth.UpdateHealthBar();

            // Trigger a new delivery.
            // pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());

            // Play the collection sound.
           // AudioSource.PlayClipAtPoint(collect, transform.position);
            // Destroy the crate.
            Destroy(transform.root.gameObject);

        }
        else if (collision.tag == "Ground" && !landed)
        {
            // ... set the Land animator trigger parameter.
           // anim.SetTrigger("Land");

            transform.parent = null;
            gameObject.AddComponent<Rigidbody2D>();
            landed = true;

            Destroy(gameObject, staytime);
        }
    }

}
