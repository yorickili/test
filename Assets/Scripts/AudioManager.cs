using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip jumpAudio;
    public AudioClip downAudio;
    public AudioClip pickupAudio;
    public AudioClip pickkeyAudio;
    public AudioClip waterAudio;
    public AudioClip waveAudio;
    public AudioClip neonAudio;
    public AudioClip deadAudio;
    public AudioClip takedamageAudio;
    public AudioClip changepartAudio;
    public float tauntDelay = 1f;

    private int sound = 1;

	// Use this for initialization
	void Start () 
    {
        SetBGM();
        SetSound();
	}
	
	// Update is called once per frame
	void Update () 
    {
        SetSound();
        if (sound == 0)
        {
            StopNeonAudio();
        }
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

    public void PlaySoundAudio(AudioClip audio, Vector3 position)
    {
        SetSound();
        print("sound: " + sound);
        if (sound > 0)
            AudioSource.PlayClipAtPoint(audio, position);
    }

    public void OpenSound()
    {
        sound = 1;
        PlayerPrefs.SetInt("SoundSwitch", 1);
        PlayerPrefs.Save();
    }

    public void CloseSound()
    {
        sound = 0;
        PlayerPrefs.SetInt("SoundSwitch", -1);
        PlayerPrefs.Save();
    }

    public void SetSound()
    {
        if (PlayerPrefs.GetInt("SoundSwitch") == -1)
        {
            sound = 0;
        }
        else 
        {
            sound = 1;
        }
    }

    public void OpenBGM()
    {
        PlayerPrefs.SetInt("SoundSwitch", 1);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().enabled = true;
        PlayerPrefs.Save();
    }

    public void CloseBGM()
    {
        PlayerPrefs.SetInt("SoundSwitch", -1);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().enabled = false;
        PlayerPrefs.Save();
    }

    public void SetBGM()
    {
        if (PlayerPrefs.GetInt("MusicSwitch") == -1)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().enabled = false;
        }
        else 
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().enabled = true;
        }
    }
}
