Shader "Hidden/LightCamera"
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
            
            //float4 _Center;
            float _Radius;
            float4 _FlashColor;
            float _MinDegree;
            float _MaxDegree;

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

            fixed4 frag (v2f i) : SV_Target
            {
                float4 color = tex2D(_MainTex, i.uv);
				//return float4(1-color.a, 1-color.a, 1-color.a, 1);
                return color;
                //return float4(0.0, 0.0, 0.0, 1 - color.r);
                //if (color.r != 0.0 || color.g != 0.0 || color.b != 0.0)
                //{
                    //return float4(1.0, 1.0, 1.0, color.r);
                //}
                //else return float4(0.0, 0.0, 0.0, 1.0);
            }
            ENDCG
        }
    }
}
