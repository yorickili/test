using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWeather : MonoBehaviour 
{

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

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
        /*
        Camera LightCamera = GameObject.FindGameObjectWithTag("LightCamera").GetComponent<Camera>();
        RenderTexture renderTexture = LightCamera.targetTexture;//拿到目标渲染纹理
        RenderTexture.active = renderTexture;
        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height);//新建纹理存储渲染纹理
        tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);//把渲染纹理的像素给Texture2D,才能在项目里面使用
        tex.Apply();//记得应用一下，不然很蛋疼

        GetMaterial.SetTexture("_MainTex", source);
        GetMaterial.SetTexture("_LightMaskTex", tex);
        */
        Graphics.Blit(source, destination, GetMaterial);
	}

}

/*
public class FogWeather : MonoBehaviour {

	public float Intensity = 1;//使用它来调整可视区域的大小
    public float Radius = 20;

    public GameObject LightObject;
    public float LightIntensity = 1;//使用它来调整可视区域的大小
    public float LightRadius = 10;

    public GameObject EndObject;
    public float EndIntensity = 1;//使用它来调整可视区域的大小
    public float EndRadius = 10;

    //public Texture2D MaskTex;
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

    private Vector3 Center;
    private Vector3 LightCenter;
    private Vector3 EndCenter;

    private float ScreenWidth, ScreenHeight;

    void SetBasicValues()
    {
        float leftBorder;
        float rightBorder;
        float topBorder;
        float downBorder;
        //the up right corner
        Vector3 cornerPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Mathf.Abs(Camera.main.transform.position.z)));

        leftBorder = Camera.main.transform.position.x - (cornerPos.x - Camera.main.transform.position.x);
        rightBorder = cornerPos.x;
        topBorder = cornerPos.y;
        downBorder = Camera.main.transform.position.y - (cornerPos.y - Camera.main.transform.position.y);

        ScreenWidth = rightBorder - leftBorder;
        ScreenHeight = topBorder - downBorder;
    }

	private void Start()
	{
        SetBasicValues();
        //ScreenWidth = CameraFollow.GetScreenWidth();
        //ScreenHeight = CameraFollow.GetScreenHeight();
	}

    private void Update()
    {
        Transform PlayerTrans = CameraFollow.GetPlayerTransform();
        Center = PlayerTrans.position - Camera.main.transform.position;
        Center.x = Center.x / ScreenWidth + 0.5f;
        Center.y = Center.y / ScreenHeight + 0.5f;

        if (LightObject.activeSelf)
        {
            LightCenter = LightObject.transform.position - Camera.main.transform.position;
            LightCenter.x = LightCenter.x / ScreenWidth + 0.5f;
            LightCenter.y = LightCenter.y / ScreenHeight + 0.5f;
        }
        else
        {
            LightCenter = new Vector3(-1000f, -1000f, -1000f);
        }

        if (EndObject.activeSelf)
        {
            EndCenter = EndObject.transform.position - Camera.main.transform.position;
            EndCenter.x = EndCenter.x / ScreenWidth + 0.5f;
            EndCenter.y = EndCenter.y / ScreenHeight + 0.5f;
        }
        else
        {
            EndCenter = new Vector3(-1000f, -1000f, -1000f);
        }
        //Debug.Log(PlayerTrans.position);
        //Debug.Log(center);
	}

	//src是摄像机截取到的照片，dest是处理过的图片
	void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        GetMaterial.SetTexture("_MainTex", src);
        GetMaterial.SetFloat("_Ratio", ScreenWidth / ScreenHeight);

        GetMaterial.SetVector("_Center", new Vector4(Center.x, Center.y, Center.z, 0f));
        GetMaterial.SetFloat("_Radius", Radius);
        GetMaterial.SetFloat("_Intensity", Intensity);

        GetMaterial.SetVector("_CenterLight", new Vector4(LightCenter.x, LightCenter.y, 0f, 0f));
        GetMaterial.SetFloat("_RadiusLight", LightRadius);
        GetMaterial.SetFloat("_IntensityLight", LightIntensity);

        GetMaterial.SetVector("_CenterEnd", new Vector4(EndCenter.x, EndCenter.y, 0f, 0f));
        GetMaterial.SetFloat("_RadiusEnd", EndRadius);
        GetMaterial.SetFloat("_IntensityEnd", EndIntensity);

        Graphics.Blit(src, dest, GetMaterial);
    }
}
*/
