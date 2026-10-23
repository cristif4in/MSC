Shader "Skybox/6 Sided" {
Properties {
 _Tint ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
[Gamma]  _Exposure ("Exposure", Range(0,8)) = 1
 _Rotation ("Rotation", Range(0,360)) = 0
[NoScaleOffset]  _FrontTex ("Front [+Z]   (HDR)", 2D) = "grey" { }
[NoScaleOffset]  _BackTex ("Back [-Z]   (HDR)", 2D) = "grey" { }
[NoScaleOffset]  _LeftTex ("Left [+X]   (HDR)", 2D) = "grey" { }
[NoScaleOffset]  _RightTex ("Right [-X]   (HDR)", 2D) = "grey" { }
[NoScaleOffset]  _UpTex ("Up [+Y]   (HDR)", 2D) = "grey" { }
[NoScaleOffset]  _DownTex ("Down [-Y]   (HDR)", 2D) = "grey" { }
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

			sampler2D _FrontTex;
			sampler2D _BackTex;
			sampler2D _LeftTex;
			sampler2D _RightTex;
			sampler2D _UpTex;
			sampler2D _DownTex;
			fixed4 _Tint;
			half _Exposure;
			float _Rotation;

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
				float3 d = normalize(i.dir);

				float rot = _Rotation * 0.0174532924;
				float s = sin(rot);
				float c = cos(rot);
				d = float3(d.x * c - d.z * s, d.y, d.x * s + d.z * c);

				float3 a = abs(d);
				float2 uv;
				float4 col;

				if (a.x >= a.y && a.x >= a.z) {
					uv = float2(d.z * (d.x >= 0.0 ? 1.0 : -1.0), d.y) * 0.5 + 0.5;
					col = (d.x >= 0.0) ? tex2D(_LeftTex, uv) : tex2D(_RightTex, uv);
				} else if (a.y >= a.z) {
					uv = float2(d.x, d.z * (d.y >= 0.0 ? 1.0 : -1.0)) * 0.5 + 0.5;
					col = (d.y >= 0.0) ? tex2D(_UpTex, uv) : tex2D(_DownTex, uv);
				} else {
					uv = float2(d.x * (d.z >= 0.0 ? 1.0 : -1.0), d.y) * 0.5 + 0.5;
					col = (d.z >= 0.0) ? tex2D(_FrontTex, uv) : tex2D(_BackTex, uv);
				}

				col.rgb *= _Tint.rgb * 2.0;
				col.rgb = 1.0 - exp(-col.rgb * _Exposure * 2.0);
				return fixed4(col.rgb, 1.0);
			}
			ENDCG
		}
	}
	Fallback Off
}
