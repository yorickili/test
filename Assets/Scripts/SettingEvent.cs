using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingEvent : MonoBehaviour {

    public GameObject ButtonPrefab;
    private GameObject MusicSwitch;
    private GameObject SoundSwitch;

    private void Start()
    {
        CreateButton(new Vector3(1250, 660, 0), "Setting/exit", new Vector2(68, 59)).GetComponent<Button>().onClick.AddListener(Exit);
        CreateButton(new Vector3(866, 475, 0), "Setting/nocheck", new Vector2(60, 59)).GetComponent<Button>().onClick.AddListener(Music);
        CreateButton(new Vector3(866, 315, 0), "Setting/nocheck", new Vector2(60, 59)).GetComponent<Button>().onClick.AddListener(Sound);
        CreateButton(new Vector3(667, 150, 0), "Setting/back", new Vector2(286, 55)).GetComponent<Button>().onClick.AddListener(Back);
        if (PlayerPrefs.GetInt("MusicSwitch") == 0) CreateMusicSwitch();
        if (PlayerPrefs.GetInt("SoundSwitch") == 0) CreateSoundSwitch();
    }

    private GameObject CreateButton(Vector3 vector3, string path, Vector2 size)
    {
        GameObject obj = Instantiate(ButtonPrefab, vector3, this.transform.rotation, this.transform);
        obj.GetComponent<RectTransform>().sizeDelta = size;
        Texture2D texture = Resources.Load(path) as Texture2D;
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
        obj.GetComponent<Image>().sprite = sprite;
        return obj;
    }

    private void CreateMusicSwitch ()
    {
        MusicSwitch = CreateButton(new Vector3(866, 475, 0), "Setting/checked", new Vector2(60, 59));
        MusicSwitch.GetComponent<Button>().onClick.AddListener(Music);
    }

    private void CreateSoundSwitch ()
    {
        SoundSwitch = CreateButton(new Vector3(866, 315, 0), "Setting/checked", new Vector2(60, 59));
        SoundSwitch.GetComponent<Button>().onClick.AddListener(Sound);
    }

    private void Music()
    {
        if (PlayerPrefs.GetInt("MusicSwitch") == 0)
        {
            Destroy(MusicSwitch);
            PlayerPrefs.SetInt("MusicSwitch", 1);
        }
        else
        {
            CreateMusicSwitch();
            PlayerPrefs.SetInt("MusicSwitch", 0);
        }
    }

    private void Sound()
    {
        if (PlayerPrefs.GetInt("SoundSwitch") == 0)
        {
            Destroy(SoundSwitch);
            PlayerPrefs.SetInt("SoundSwitch", 1);
        }
        else
        {
            CreateSoundSwitch();
            PlayerPrefs.SetInt("SoundSwitch", 0);
        }
    }

    private void Exit ()
    {
        Destroy(this.gameObject);
    }

    private void Back()
    {
        if (SceneManager.GetActiveScene().name != "Welcome") SceneManager.LoadScene("WelCome");
        else Exit();
    }
}
