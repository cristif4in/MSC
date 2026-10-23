Shader "Car/GlassReflect2DNew" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
		_MainTex ("Base (RGB) RefStrength (A)", 2D) = "white" {}
		_Cube ("Reflection Cubemap", CUBE) = "_Skybox" {}
		_BumpMap ("Bumpmap (RGB Trans)", 2D) = "bump" {}
		_2DReflection ("Reflection (RGB)", CUBE) = "grey" {}
		_FresnelPower ("_FresnelPower", Range(0.05,5)) = 0.75
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
			samplerCUBE _Cube;
			sampler2D _BumpMap;
			fixed4 _Color;
			fixed4 _ReflectColor;
			half _FresnelPower;

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
				fixed4 tex = tex2D(_MainTex, i.uv);
				float3 n = normalize(i.worldNormal);
				float3 v = normalize(i.worldView);

				float fresnel = pow(1.0 - saturate(dot(n, v)), max(_FresnelPower, 0.05));
				float3 reflected = texCUBE(_Cube, reflect(-v, n)).rgb;

				float3 col = tex.rgb * _Color.rgb + reflected * _ReflectColor.rgb * _ReflectColor.a;
				float alpha = saturate(lerp(0.12, 0.85, fresnel) + tex.a * 0.15);

				return fixed4(col, alpha);
			}
			ENDCG
		}
	}
	Fallback Off
}
