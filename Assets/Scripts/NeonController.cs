using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonController : MonoBehaviour {

    public GameObject NeonLight;
    public float FadeSpeed = 0.005f;
    public int StayTime = 500;

    //bool isLighting = false;
    float Luminance = 0f, MaxLuminance = 1f;
    float Step = 0f;
    int Stay = 0;

    private void SetColor(Vector4 InColor)
    {
        this.GetComponent<SpriteRenderer>().color = InColor;
        NeonLight.GetComponent<SpriteRenderer>().color = InColor;

        //Vector3 position = this.transform.position;
        //this.transform.position = new Vector3(position.x+Speed, position.y, position.z);
        //position = NeonLight.transform.position;
        //NeonLight.transform.position = new Vector3(position.x + Speed, position.y, position.z);
    }

    // Use this for initialization
    void Start () {
        SetColor(new Vector4(0f, 0f, 0f, 0f));
        //SetColor(new Vector4(1f, 1f, 1f, 1f));
	}
	
	// Update is called once per frame
	void Update () 
    {
        SetColor(new Vector4(Luminance, Luminance, Luminance, Luminance));

        if (Luminance <= 0f && Mathf.Abs(Step + 1) < 0.00001)
        {
            Step = 0f;
            Luminance = 0f;
        }
        else if (Luminance >= MaxLuminance)
        {
            Luminance = MaxLuminance - FadeSpeed;
            Step = -1f;
            Stay = 100;
        }

        if (Stay == 0)
            Luminance += Step * FadeSpeed;
        else
            Stay -= 1;
	}

    public bool LightUp()
    {
        //SetColor(new Vector4(1, 1, 1, 1));
        Step = 1f;

        return true;
    }
}
