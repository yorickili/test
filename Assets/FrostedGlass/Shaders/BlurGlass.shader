﻿Shader "Hidden/BlurGlass"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        //_GrabBlurTexture_0 ("Texture0", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		//Cull Off ZWrite Off ZTest Always

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
            
            sampler2D _GrabBlurTexture_0;
            sampler2D _GrabBlurTexture_1;
            sampler2D _GrabBlurTexture_2;
            sampler2D _GrabBlurTexture_3;
			

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_GrabBlurTexture_3, i.uv);
				//if (col.b > 0.2)
                    //return fixed4(1, 0, 0, 1);
                // just invert the colors
				//col.rgb = 1 - col.rgb;
				return col;
			}
			ENDCG
		}
	}
}
