using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip jumpAudio;
    public AudioClip pickupAudio;
    public AudioClip pickkeyAudio;
    public AudioClip waterAudio;
    public AudioClip waveAudio;
    public AudioClip neonAudio;
    public AudioClip deadAudio;
    public AudioClip takedamageAudio;
    public float tauntDelay = 1f;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    public void PlayNeonAudio()
    {
        //yield return new WaitForSeconds(tauntDelay);
        if (!GetComponent<AudioSource>().isPlaying)
        {
            // Play the new taunt.
            //GetComponent<AudioSource>().clip = neonAudio;
            GetComponent<AudioSource>().Play();
        }
    }

    public void StopNeonAudio()
    {
        if(GetComponent<AudioSource>().isPlaying)
        {
            // Play the new taunt.
            //GetComponent<AudioSource>().clip = neonAudio;
            GetComponent<AudioSource>().Stop();
        }
    }
}
