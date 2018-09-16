using UnityEngine;
using System.Collections;
//血量包1 加当前血量nowhealth
public class HealthPickup : MonoBehaviour
{
	public float healthBonus=10f;				// How much health the crate gives the player.
	//public AudioClip collect;               // The sound of the crate being collected.
    public float staytime = 2f;                  //staytime


   // private PickupSpawner pickupSpawner;	// Reference to the pickup spawner.
	//private Animator anim;					// Reference to the animator component.
	private bool landed;					// Whether or not the crate has landed.


	void Awake ()
	{
		// Setting up the references.
		//pickupSpawner = GameObject.Find("pickupManager").GetComponent<PickupSpawner>();
		//anim = transform.root.GetComponent<Animator>();
	}


	void OnTriggerEnter2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		if(other.tag == "Player")
		{
			// Get a reference to the player health script.
			PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.IncreaseHealth(healthBonus);

            // Destroy the crate.
            //Destroy(transform.root.gameObject);
            this.gameObject.SetActive(false);
        }
		// Otherwise if the crate hits the ground...
		/*else if(other.tag == "Ground" && !landed)
		{
			// ... set the Land animator trigger parameter.
			//anim.SetTrigger("Land");

			transform.parent = null;
			gameObject.AddComponent<Rigidbody2D>();
			landed = true;

           // Destroy(gameObject, staytime);
		}*/
	}
}
