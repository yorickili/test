Shader "Hidden/FogWeather"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha

        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            sampler2D _MainTex;
            sampler2D _LightMaskTex;

            fixed4 frag (v2f i) : SV_Target
            {
                float4 mask = tex2D(_LightMaskTex, i.uv);
				//return mask;               
                if (mask.r != 0.0)
                {
                    float4 color = tex2D(_MainTex, i.uv);
                    return float4(color.r, color.g, color.b, mask.r);
                }
                else return float4(0.0, 0.0, 0.0, 1.0);
            }
            ENDCG
        }
    }



/*
	Properties{
        _MainTex("MainTex",2D) = "white"{}
        //_Center("Center",Vector)
        //_MaskTex("MaskTex",2D) = "white"{}
        _Radius("Radius",Range(0,100)) = 20
        _Intensity("Intensity",Range(0,100)) = 1
    }
    SubShader{
        Pass{
            Tags{ "LightMode" = "ForwardBase" }
 
            CGPROGRAM
            #include "Lighting.cginc"
            #pragma vertex vert
            #pragma fragment frag
            //sampler2D _MaskTex;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _AlphaBase;
            float _Ratio;

            float4 _Center;
            float _Intensity;
            float _Radius;

            float4 _CenterEnd;
            float _IntensityEnd;
            float _RadiusEnd;

            float4 _CenterLight;
            float _IntensityLight;
            float _RadiusLight;


            struct a2v {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };
            struct v2f {
                float4 pos : SV_POSITION;
                fixed2 uv : TEXCOORD0;
            };
            v2f vert(a2v i) {
                v2f o;
                o.pos = UnityObjectToClipPos(i.vertex);
                o.uv = TRANSFORM_TEX(i.texcoord, _MainTex);
                return o;
            }
            float dist(float2 d) {
                d.x *= _Ratio;
                return sqrt(d.x * d.x + d.y * d.y);
            }

            float getfactor(float2 c, float i, float r)
            {
                float d = dist(c) / (0.5 * _Ratio);
                float factor = 1.0;
                if (d > r / 100.0) factor = 0.0;
                else if (i > 0) {
                    factor = 1.0 - d / (r / 100.0);
                    factor = log(1.0+i*factor) / log(1+i);
                    //factor *= _Intensity;
                    if (factor > 1.0) factor = 1.0;
                    if (factor < 0.0) factor = 0.0;
                }
                else {
                    i = -i;
                    factor = d / (r / 100.0);
                    factor = 1.0 - log(1.0+i*factor) / log(1+i);
                    //factor *= _Intensity;
                    if (factor > 1.0) factor = 1.0;
                    if (factor < 0.0) factor = 0.0;
                }
                return factor;
            }

            fixed4 frag(v2f o) :SV_TARGET{
                //fixed4 color = float4(1.0, 0.0, 0.0, 1.0);//tex2D(_MaskTex, o.uv);
                //color.r = 1 - color.r;
                //color.g = 1 - color.g;
                //color.b = 1 - color.b;

                float factor1 = getfactor(o.uv - _Center, _Intensity, _Radius);
                float factor2 = getfactor(o.uv - _CenterLight, _IntensityLight, _RadiusLight);
                float factor3 = getfactor(o.uv - _CenterEnd, _IntensityEnd, _RadiusEnd);

                float factor = min(1.0, 1.0 - (1.0-factor1) * (1.0-factor2) * (1.0-factor3));

                float4 color = tex2D(_MainTex, o.uv);
                color.r = color.r * factor;
                color.g = color.g * factor;
                color.b = color.b * factor;
                color.a = 1.0;

                return color;
            }
            ENDCG
        }
    }
*/
}
