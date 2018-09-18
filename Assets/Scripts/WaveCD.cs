using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCD : MonoBehaviour {

    public float CD = 30f;

    bool isCDing = false;
    float now = 0;
    Image Icon;

	void Start () {
        Icon = GetComponent<Image>();
	}

    void Update()
    {
        if (isCDing) {
            if (now >= CD) {
                isCDing = false;
                now = 0;
            }
            else
            {
                now += Time.deltaTime;
                Icon.fillAmount = now / CD;
            }
        }
    }

    public bool Turn ()
    {
        if (!isCDing) {
            isCDing = true;
            return true;
        }
        else {
            return false;
        }
    }

}
