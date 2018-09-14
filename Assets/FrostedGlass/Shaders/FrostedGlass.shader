Shader "Custom/FrostedGlass"
{
	Properties
	{
        _MainTex ("Base (RGB)", 2D) = "" {}
		//_FrostTex ("Fross Texture", 2D) = "white" {}
		//_FrostIntensity ("Frost Intensity", Range(0.0, 1.0)) = 0.5
	}
	SubShader
	{
		//Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		//LOD 100

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
				float2 uvfrost : TEXCOORD0;
				float4 uvgrab : TEXCOORD1;
                float2 uv : TEXCOORD2;
				float4 vertex : SV_POSITION;
			};

			sampler2D _FrostTex;
            sampler2D _MainTex;
			float4 _FrostTex_ST;// = float4(1, 1, 0, 0);

			float _FrostIntensity = 1;
            float _Ratio;
            float _Intensity;
            float _Radius;
            float4 _Center;

			sampler2D _GrabBlurTexture_0;
			sampler2D _GrabBlurTexture_1;
			sampler2D _GrabBlurTexture_2;
			sampler2D _GrabBlurTexture_3;
			
            sampler2D _LightMap;
            
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uvfrost = TRANSFORM_TEX(v.uv, _FrostTex);
				o.uvgrab = ComputeGrabScreenPos(o.vertex);
                o.uv = v.uv;
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
            
			
			fixed4 frag (v2f i) : SV_Target
			{
                _FrostIntensity = 1;
				float surfSmooth = 1-tex2D(_FrostTex, float2(i.uv.x, i.uv.y/_Ratio)) * _FrostIntensity;
				
				surfSmooth = clamp(0, 1, surfSmooth);

				float4 refraction;
                /*
				half4 ref00 = tex2Dproj(_GrabBlurTexture_0, i.uvgrab);
				half4 ref01 = tex2Dproj(_GrabBlurTexture_1, i.uvgrab);
				half4 ref02 = tex2Dproj(_GrabBlurTexture_2, i.uvgrab);
				half4 ref03 = tex2Dproj(_GrabBlurTexture_3, i.uvgrab);
                */
                float4 ref00 = tex2D(_GrabBlurTexture_0, i.uv);
                float4 ref01 = tex2D(_GrabBlurTexture_1, i.uv);
                float4 ref02 = tex2D(_GrabBlurTexture_2, i.uv);
                float4 ref03 = tex2D(_GrabBlurTexture_3, i.uv);
                
				float step00 = smoothstep(0.75, 1.00, surfSmooth);
				float step01 = smoothstep(0.5, 0.75, surfSmooth);
				float step02 = smoothstep(0.05, 0.5, surfSmooth);
				float step03 = smoothstep(0.00, 0.05, surfSmooth);

				refraction = lerp(ref03, lerp( lerp( lerp(ref03, ref02, step02), ref01, step01), ref00, step00), step03);
                //refraction = lerp( lerp( lerp(ref03, ref02, step02), ref01, step01), ref00, step00);
				
                float4 light = tex2D(_LightMap, i.uv);
                float4 col = tex2D(_MainTex, i.uv);
                
                //refraction /= dist(i.uv - float2(0.5,0.5)) * 10;
                
                float factor = getfactor(i.uv - _Center.xy, _Intensity, _Radius);
                float4 output = light.r * col + (1-light.r) * refraction * factor;
				return output;//tex2D(_GrabBlurTexture_3, i.uv);
                //return factor;
			}
			ENDCG
		}
	}
}
 