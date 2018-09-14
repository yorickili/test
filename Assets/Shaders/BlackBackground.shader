Shader "Sprites/BlackBackground"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		//Cull Off ZWrite Off ZTest Always
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
 
            float noise(float2 uv)
            {
                float _Factor1 = 1234;
                float _Factor2 = 510;
                float _Factor3 = 7275;
                return frac(sin(dot(uv, float2(_Factor1, _Factor2))) * _Factor3);
            }
 
			fixed4 frag (v2f i) : SV_Target
			{
                fixed4 incol = tex2D(_MainTex, i.uv);
                
                float f = noise(i.uv);
                fixed4 col = fixed4(f, f, f, 1 - incol.r);
                return col;
                /*
				fixed4 col = tex2D(_MainTex, i.uv);
				// just invert the colors
				//col.rgb = 1 - col.rgb;
                //return col;
                if (col.r == 0)
                {
                    float temp = noise(float2(0.5,0.3));
                    return float4(temp, temp, temp, 0);
                }
                return float4(0,0,0,1-col.r);
                */
			}
			ENDCG
		}
	}
}
