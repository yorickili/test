using UnityEngine;
using System.Collections;
//随机生成障碍物
public class Spawner : MonoBehaviour
{
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public GameObject[] enemies;		// Array of enemy prefabs.


	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
	    InvokeRepeating("Spawn", spawnDelay, spawnTime);  //随机生成敌人函数
	}


	void Spawn ()
	{
		// Instantiate a random enemy.
		int enemyIndex = Random.Range(0, enemies.Length);
		Instantiate(enemies[enemyIndex], transform.position, transform.rotation);

		// Play the spawning effect from all of the particle systems.
		//foreach(ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		//{
			//p.Play();
		//}
	}
}
