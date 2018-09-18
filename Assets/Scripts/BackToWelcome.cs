using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToWelcome : MonoBehaviour
{

    public GameObject ButtonPrefab;

    void Start()
    {
        CreateButton(new Vector3(Screen.width * 0.75f, Screen.height * 0.45f, 0), "Setting/back", new Vector2(275, 60)).onClick.AddListener(Back);

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

    private void Back()
    {
        SceneManager.LoadScene("Welcome");
    }

}
