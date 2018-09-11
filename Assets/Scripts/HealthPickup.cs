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

            // Increasse the player's health by the health bonus but clamp it at 100.
            //playerHealth.nowhealth += healthBonus;
            //playerHealth.nowhealth = Mathf.Clamp(playerHealth.nowhealth, 0f, playerHealth.maxhealth);
            playerHealth.IncreaseHealth(healthBonus);
            print("add now health"+playerHealth.nowhealth);

			// Update the health bar.
			//playerHealth.UpdateHealthBar();

			// Trigger a new delivery.
			//pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());

			// Play the collection sound.
			//AudioSource.PlayClipAtPoint(collect,transform.position);

			// Destroy the crate.
			Destroy(transform.root.gameObject);
		}
		// Otherwise if the crate hits the ground...
		else if(other.tag == "Ground" && !landed)
		{
			// ... set the Land animator trigger parameter.
			//anim.SetTrigger("Land");

			transform.parent = null;
			gameObject.AddComponent<Rigidbody2D>();
			landed = true;

           // Destroy(gameObject, staytime);
		}
	}
}
