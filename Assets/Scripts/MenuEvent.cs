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
        CreateButton(new Vector3(667, 525, 0), "Welcome/startbutton", new Vector2(134, 70)).onClick.AddListener(StartGame);
        CreateButton(new Vector3(667, 375, 0), "Welcome/settingbutton", new Vector2(134, 70)).onClick.AddListener(ShowSetting);
        CreateButton(new Vector3(667, 225, 0), "Welcome/exitbutton", new Vector2(134, 70)).onClick.AddListener(ExitGame);
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
            Instantiate(SelectLevelPrefab, this.transform.position, this.transform.rotation);
            //SceneManager.LoadScene("level0");
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
}
