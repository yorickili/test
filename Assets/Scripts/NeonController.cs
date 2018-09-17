using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonController : MonoBehaviour {

    public GameObject NeonLight;
    private float ShowSpeed = 0.04f, FadeSpeed = 0.009f;
    private int StayTime = 100;

    //bool isLighting = false;
    float Luminance = 0f, MaxLuminance = 1f;
    float Step = 0f;
    int Stay = 0;

    private void SetColor(Vector4 InColor)
    {
        if (InColor.w < 0.01f) this.GetComponent<SpriteRenderer>().color = new Vector4(0, 0f, 0f, InColor.x + 0.3f);
        else this.GetComponent<SpriteRenderer>().color = InColor;
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
            Luminance = MaxLuminance - ShowSpeed;
            Step = -1f;
            Stay = StayTime;
        }

        if (Stay == 0)
        {
            if (Step > 0)
                Luminance += Step * ShowSpeed;
            else
                Luminance += Step * FadeSpeed;
        }
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
