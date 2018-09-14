using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightCamera : MonoBehaviour {

    public Shader ScreanShader;
    public Material GetMaterial
    {
        get
        {
            if (_material == null) _material = new Material(ScreanShader);
            return _material;
        }
    }
    private Material _material = null;

    private RenderTexture renderTexture;
    private RenderTexture target;

    private void Start()
    {
        renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        this.GetComponent<Camera>().targetTexture = renderTexture;

        //RawImage rawImage = GameObject.FindGameObjectWithTag("BlackBG").GetComponent<RawImage>();
        //rawImage.texture = renderTexture;

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FogWeather>().LightCameraOut = renderTexture;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    /*
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        //Graphics.Blit(src, dest, GetMaterial);

        //RenderTexture.active = dest;
        //Texture2D tex = new Texture2D(dest.width, dest.height);//新建纹理存储渲染纹理
        //tex.ReadPixels(new Rect(0, 0, dest.width, dest.height), 0, 0);//把渲染纹理的像素给Texture2D,才能在项目里面使用
        //tex.Apply();//记得应用一下，不然很蛋疼
        
        //Sprite sprite = Sprite.Create(src, new Rect(0, 0, src.width, src.height), Vector2.zero);
        RawImage rawImage = GameObject.FindGameObjectWithTag("BlackBG").GetComponent<RawImage>();
        rawImage.texture = src;
    }
    */
}
