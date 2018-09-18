using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuEvent : MonoBehaviour {

    public GameObject ButtonPrefab;
    public GameObject SettingPrefab;
    public GameObject SelectLevelPrefab;

    void Start ()
    {
        CreateButton(new Vector3(Screen.width * 0.5f, Screen.height * 0.7f, 0), "Welcome/startbutton", new Vector2(134, 70)).onClick.AddListener(StartGame);
        CreateButton(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0), "Welcome/settingbutton", new Vector2(134, 70)).onClick.AddListener(ShowSetting);
        CreateButton(new Vector3(Screen.width * 0.5f, Screen.height * 0.3f, 0), "Welcome/exitbutton", new Vector2(134, 70)).onClick.AddListener(ExitGame);

        SetBGM();
    }

    private Button CreateButton(Vector3 vector3, string path, Vector2 size)
    {
        GameObject obj = Instantiate(ButtonPrefab, vector3, this.transform.rotation, this.transform);
        obj.GetComponent<RectTransform>().sizeDelta = size;
        Texture2D texture = Resources.Load(path) as Texture2D;
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
        obj.GetComponent<Image>().sprite = sprite;
        return obj.GetComponent<Button>();
    }

    private void StartGame()
    {
        if (PlayerPrefs.GetInt("level") >= 1) {
            Instantiate(SelectLevelPrefab, this.transform.position, this.transform.rotation);
        }
        else {
            SceneManager.LoadScene("Level0");
        }
    }

    private void ShowSetting()
    {
        Instantiate(SettingPrefab, this.transform.position, this.transform.rotation);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void SetBGM()
    {
        if (PlayerPrefs.GetInt("MusicSwitch") == 0)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().enabled = false;
        }
        else
        {
            PlayerPrefs.SetInt("SoundSwitch", 1);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().enabled = true;
        }
    }
}
