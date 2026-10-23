Shader "Windshield/windshield" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap ("Normal", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Rain ("Rain", 2D) = "white" {}
		_RainOpacity ("Rain Opacity", Range(0,1)) = 0.5
		_Raining ("Raining (On/Off)", Range(0,1)) = 0
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		Cull Off

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _BumpMap;
			sampler2D _Rain;
			fixed4 _Color;
			half _Glossiness;
			half _RainOpacity;
			half _Raining;

			struct appdata {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldNormal : TEXCOORD1;
				float3 worldView : TEXCOORD2;
			};

			v2f vert (appdata v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.worldView = _WorldSpaceCameraPos.xyz - worldPos;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target {
				fixed4 tex = tex2D(_MainTex, i.uv) * _Color;
				float3 n = normalize(i.worldNormal);
				float3 v = normalize(i.worldView);
				float fresnel = pow(1.0 - saturate(dot(n, v)), 2.0);

				float alpha = saturate(lerp(0.10, 0.70, fresnel) + tex.a * 0.20);

				if (_Raining > 0.5) {
					fixed4 rain = tex2D(_Rain, i.uv) * _RainOpacity;
					tex.rgb = lerp(tex.rgb, rain.rgb, rain.a);
					alpha = saturate(alpha + rain.a * _RainOpacity);
				}

				return fixed4(tex.rgb, alpha);
			}
			ENDCG
		}
	}
	Fallback Off
}
