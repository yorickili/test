using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSetting : MonoBehaviour {

    public GameObject Setting;

	void Start ()
    {
        GetComponent<Button>().onClick.AddListener(Open);
    }

    void Open ()
    {
        Instantiate(Setting, new Vector3(0, 0, 0), this.transform.rotation);
    }
}
