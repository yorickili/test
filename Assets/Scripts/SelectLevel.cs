using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour {

    public GameObject ButtonPrefab;
    public GameObject MaskPrefab;

    void Start () {
        CreateButton(new Vector3(Screen.width * 0.3f, Screen.height * 0.5f, 0), "SelectLevel/1", new Vector2(120, 120)).onClick.AddListener(() => EnterLevel(0));

        CreateButton(new Vector3(Screen.width * 0.25f, Screen.height * 0.3f, 0), "SelectLevel/1", new Vector2(60, 60)).onClick.AddListener(() => EnterLevel(0));
        CreateButton(new Vector3(Screen.width * 0.3f, Screen.height * 0.3f, 0), "SelectLevel/2", new Vector2(60, 60)).onClick.AddListener(() => EnterLevel(1));
        CreateButton(new Vector3(Screen.width * 0.35f, Screen.height * 0.3f, 0), "SelectLevel/3", new Vector2(60, 60)).onClick.AddListener(() => EnterLevel(2));


        CreateButton(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0), "SelectLevel/2", new Vector2(120, 120)).onClick.AddListener(() => EnterLevel(3));

        CreateButton(new Vector3(Screen.width * 0.45f, Screen.height * 0.7f, 0), "SelectLevel/1", new Vector2(60, 60)).onClick.AddListener(() => EnterLevel(3));
        CreateButton(new Vector3(Screen.width * 0.5f, Screen.height * 0.7f, 0), "SelectLevel/2", new Vector2(60, 60)).onClick.AddListener(() => EnterLevel(4));
        CreateButton(new Vector3(Screen.width * 0.55f, Screen.height * 0.7f, 0), "SelectLevel/3", new Vector2(60, 60)).onClick.AddListener(() => EnterLevel(5));


        CreateButton(new Vector3(Screen.width * 0.7f, Screen.height * 0.5f, 0), "SelectLevel/3", new Vector2(120, 120)).onClick.AddListener(() => EnterLevel(6));

        CreateButton(new Vector3(Screen.width * 0.65f, Screen.height * 0.3f, 0), "SelectLevel/1", new Vector2(60, 60)).onClick.AddListener(() => EnterLevel(6));
        CreateButton(new Vector3(Screen.width * 0.7f, Screen.height * 0.3f, 0), "SelectLevel/2", new Vector2(60, 60)).onClick.AddListener(() => EnterLevel(7));
        CreateButton(new Vector3(Screen.width * 0.75f, Screen.height * 0.3f, 0), "SelectLevel/3", new Vector2(60, 60)).onClick.AddListener(() => EnterLevel(8));


        CreateButton(new Vector3(Screen.width * 0.93f, Screen.height * 0.93f, 0), "SelectLevel/back", new Vector2(62, 45)).onClick.AddListener(Back);

        CreateMask(new Vector3(Screen.width * 0.7f, Screen.height * 0.3f, 0));
        CreateMask(new Vector3(Screen.width * 0.75f, Screen.height * 0.3f, 0));

        /*
        if (PlayerPrefs.GetInt("level") == 1)
        {
            CreateMask(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0));
            CreateMask(new Vector3(Screen.width * 0.7f, Screen.height * 0.5f, 0));
        }
        else if (PlayerPrefs.GetInt("level") == 2)
        {
            CreateMask(new Vector3(Screen.width * 0.7f, Screen.height * 0.5f, 0));
        }
        */
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

    private void CreateMask(Vector3 vector3)
    {
        Instantiate(MaskPrefab, vector3, this.transform.rotation, this.transform);
    }

    private void EnterLevel (int level)
    {
        int biglevel = level / 3 + 1;
        int smalllevel = level - (biglevel-1) * 3;

        print("small: " + smalllevel);
        PlayerControl.nowPart = smalllevel;
        SceneManager.LoadScene("Level" + biglevel);
    }

    private void Back()
    {
        Destroy(this.gameObject);
    }
}
