Shader "Legacy Shaders/Self-Illumin/Diffuse" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" { }
		_Illum ("Illumin (A)", 2D) = "white" { }
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Illum;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Illum;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed4 i = tex2D(_Illum, IN.uv_Illum);

			o.Albedo = c.rgb;
			o.Emission = c.rgb * i.rgb * i.a;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Legacy Shaders/Diffuse"
}
