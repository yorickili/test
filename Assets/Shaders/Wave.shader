Shader "Sprites/Wave"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _FlashColor ("FlashColor", Color) = (1,1,1,1)
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

            float dist(float2 p) {
                return sqrt(p.x * p.x + p.y * p.y);
            }

            int check(float2 p) {
                float d = atan2(p.y, p.x);
                if (d < _MinDegree || d > _MaxDegree)
                    return 0;
                else 
                    return 1;
            }

			fixed4 frag (v2f i) : SV_Target
			{
                if (!check(i.uv - float2(0.5, 0.5)))
                    return float4(0.0, 0.0, 0.0, 0.0);

                float d = dist(i.uv - float2(0.5, 0.5));
				//fixed4 col = tex2D(_MainTex, i.uv);
                //if (col.a == 0.0) return float4(0.0, 0.0, 0.0, 0.0); 
                // just invert the colors
				//col.rgb = col.rgb;
                float factor = abs(d - _Radius);
                if (factor < 0.002) 
                {
                    return _FlashColor;
                }
                else return float4(0.0, 0.0, 0.0, 0.0);
			}
			ENDCG
		}
	}
}
