Shader "Car Paint Reflective Bumped Specular (Paint only, no mask)" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_Tint ("Tint Color", Color) = (1,1,1,1)
		_SpecColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
		_Shininess ("Shininess", Range(0.03,1)) = 0.078125
		_Reflection ("Reflection", Range(0.03,1)) = 0.25
		_ReflectColor ("Reflection Color (RGB)", Color) = (1,1,1,0.5)
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" { }
		_Skybox ("Reflection Cubemap", CUBE) = "_Skybox" { }
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf BlinnPhong
		#pragma target 3.0

		sampler2D _MainTex;
		samplerCUBE _Skybox;
		fixed4 _Color;
		fixed4 _Tint;
		fixed4 _SpecColor;
		fixed4 _ReflectColor;
		half _Shininess;
		half _Reflection;

		struct Input {
			float2 uv_MainTex;
			float3 worldRefl;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);

			o.Albedo = c.rgb * _Color.rgb * _Tint.rgb;
			o.Specular = _SpecColor.rgb;
			o.Gloss = saturate(_Shininess * c.a);
			o.Emission = texCUBE(_Skybox, IN.worldRefl).rgb * _ReflectColor.rgb * _ReflectColor.a * _Reflection;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Legacy Shaders/Specular"
}
