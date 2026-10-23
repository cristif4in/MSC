Shader "Standard" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo", 2D) = "white" { }
		_Cutoff ("Alpha Cutoff", Range(0,1)) = 0.5
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		[Gamma] _Metallic ("Metallic", Range(0,1)) = 0
		_MetallicGlossMap ("Metallic", 2D) = "white" { }
		_BumpScale ("Scale", Float) = 1
		_BumpMap ("Normal Map", 2D) = "bump" { }
		_Parallax ("Height Scale", Range(0.005,0.08)) = 0.02
		_ParallaxMap ("Height Map", 2D) = "black" { }
		_OcclusionStrength ("Strength", Range(0,1)) = 1
		_OcclusionMap ("Occlusion", 2D) = "white" { }
		_EmissionColor ("Color", Color) = (0,0,0,1)
		_EmissionMap ("Emission", 2D) = "white" { }
		_DetailMask ("Detail Mask", 2D) = "white" { }
		_DetailAlbedoMap ("Detail Albedo x2", 2D) = "grey" { }
		_DetailNormalMapScale ("Scale", Float) = 1
		_DetailNormalMap ("Normal Map", 2D) = "bump" { }
		[Enum(UV0,0,UV1,1)] _UVSec ("UV Set for secondary textures", Float) = 0
		[HideInInspector] _EmissionScaleUI ("Scale", Float) = 0
		[HideInInspector] _EmissionColorUI ("Color", Color) = (1,1,1,1)
		[HideInInspector] _Mode ("__mode", Float) = 0
		[HideInInspector] _SrcBlend ("__src", Float) = 1
		[HideInInspector] _DstBlend ("__dst", Float) = 0
		[HideInInspector] _ZWrite ("__zw", Float) = 1
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MetallicGlossMap;
		sampler2D _BumpMap;
		sampler2D _OcclusionMap;
		sampler2D _EmissionMap;

		fixed4 _Color;
		fixed4 _EmissionColor;
		half _Glossiness;
		half _Metallic;
		half _BumpScale;
		half _OcclusionStrength;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MetallicGlossMap;
			float2 uv_BumpMap;
			float2 uv_OcclusionMap;
			float2 uv_EmissionMap;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed4 mg = tex2D(_MetallicGlossMap, IN.uv_MetallicGlossMap);
			fixed4 occ = tex2D(_OcclusionMap, IN.uv_OcclusionMap);

			o.Albedo = c.rgb;
			o.Metallic = mg.r * _Metallic;
			o.Smoothness = mg.a * _Glossiness;
			o.Occlusion = lerp(1.0, occ.g, _OcclusionStrength);
			o.Emission = tex2D(_EmissionMap, IN.uv_EmissionMap).rgb * _EmissionColor.rgb;

			float3 n = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			n.xy *= _BumpScale;
			o.Normal = normalize(n);
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Legacy Shaders/Diffuse"
}
