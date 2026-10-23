Shader "Skybox/Procedural" {
Properties {
 _SunSize ("Sun size", Range(0,1)) = 0.04
 _AtmosphereThickness ("Atmosphere Thickness", Range(0,5)) = 1
 _SkyTint ("Sky Tint", Color) = (0.5,0.5,0.5,1)
 _GroundColor ("Ground", Color) = (0.369,0.349,0.341,1)
 _Exposure ("Exposure", Range(0,8)) = 1.3
}
	SubShader {
		Tags { "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
		Cull Off
		ZWrite Off

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			fixed4 _SkyTint;
			fixed4 _GroundColor;
			half _Exposure;
			half _AtmosphereThickness;

			struct appdata {
				float4 vertex : POSITION;
			};
			struct v2f {
				float4 pos : SV_POSITION;
				float3 dir : TEXCOORD0;
			};

			v2f vert (appdata v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.dir = v.vertex.xyz;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target {
				float3 dir = normalize(i.dir);
				float h = dir.y;

				float sky = smoothstep(-0.12, 0.25, h);
				float3 col = lerp(_GroundColor.rgb, _SkyTint.rgb, sky);

				// brighten the band just above the horizon a little
				col += _SkyTint.rgb * 0.25 * exp(-abs(h) * 6.0) * _AtmosphereThickness;

				col = 1.0 - exp(-col * _Exposure * 1.5);
				return fixed4(col, 1.0);
			}
			ENDCG
		}
	}
	Fallback Off
}
