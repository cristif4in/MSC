Shader "Hidden/FXAA III (Console)" {
Properties {
 _MainTex ("-", 2D) = "white" { }
 _EdgeThresholdMin ("Edge threshold min", Float) = 0.125
 _EdgeThreshold ("Edge Threshold", Float) = 0.25
 _EdgeSharpness ("Edge sharpness", Float) = 4
}
	
	SubShader {
		Tags { "RenderType"="Opaque" }
    Pass {
        ZTest Always
        Cull Off
        ZWrite Off
        Fog { Mode Off }
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        sampler2D _MainTex;
        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };
        v2f vert(appdata_img v) {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;
            return o;
        }
        fixed4 frag(v2f i) : SV_Target {
            return tex2D(_MainTex, i.uv);
        }
        ENDCG
    }
    Pass {
        ZTest Always
        Cull Off
        ZWrite Off
        Fog { Mode Off }
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        sampler2D _MainTex;
        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };
        v2f vert(appdata_img v) {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;
            return o;
        }
        fixed4 frag(v2f i) : SV_Target {
            return tex2D(_MainTex, i.uv);
        }
        ENDCG
    }
    Pass {
        ZTest Always
        Cull Off
        ZWrite Off
        Fog { Mode Off }
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        sampler2D _MainTex;
        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };
        v2f vert(appdata_img v) {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;
            return o;
        }
        fixed4 frag(v2f i) : SV_Target {
            return tex2D(_MainTex, i.uv);
        }
        ENDCG
    }
    Pass {
        ZTest Always
        Cull Off
        ZWrite Off
        Fog { Mode Off }
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        sampler2D _MainTex;
        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };
        v2f vert(appdata_img v) {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;
            return o;
        }
        fixed4 frag(v2f i) : SV_Target {
            return tex2D(_MainTex, i.uv);
        }
        ENDCG
    }
    Pass {
        ZTest Always
        Cull Off
        ZWrite Off
        Fog { Mode Off }
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        sampler2D _MainTex;
        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };
        v2f vert(appdata_img v) {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;
            return o;
        }
        fixed4 frag(v2f i) : SV_Target {
            return tex2D(_MainTex, i.uv);
        }
        ENDCG
    }
    Pass {
        ZTest Always
        Cull Off
        ZWrite Off
        Fog { Mode Off }
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        sampler2D _MainTex;
        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };
        v2f vert(appdata_img v) {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;
            return o;
        }
        fixed4 frag(v2f i) : SV_Target {
            return tex2D(_MainTex, i.uv);
        }
        ENDCG
    }
    Pass {
        ZTest Always
        Cull Off
        ZWrite Off
        Fog { Mode Off }
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        sampler2D _MainTex;
        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };
        v2f vert(appdata_img v) {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;
            return o;
        }
        fixed4 frag(v2f i) : SV_Target {
            return tex2D(_MainTex, i.uv);
        }
        ENDCG
    }
    Pass {
        ZTest Always
        Cull Off
        ZWrite Off
        Fog { Mode Off }
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        sampler2D _MainTex;
        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };
        v2f vert(appdata_img v) {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;
            return o;
        }
        fixed4 frag(v2f i) : SV_Target {
            return tex2D(_MainTex, i.uv);
        }
        ENDCG
    }
    Pass {
        ZTest Always
        Cull Off
        ZWrite Off
        Fog { Mode Off }
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        sampler2D _MainTex;
        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };
        v2f vert(appdata_img v) {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;
            return o;
        }
        fixed4 frag(v2f i) : SV_Target {
            return tex2D(_MainTex, i.uv);
        }
        ENDCG
    }
    Pass {
        ZTest Always
        Cull Off
        ZWrite Off
        Fog { Mode Off }
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        sampler2D _MainTex;
        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };
        v2f vert(appdata_img v) {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;
            return o;
        }
        fixed4 frag(v2f i) : SV_Target {
            return tex2D(_MainTex, i.uv);
        }
        ENDCG
    }
    Pass {
        ZTest Always
        Cull Off
        ZWrite Off
        Fog { Mode Off }
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        sampler2D _MainTex;
        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };
        v2f vert(appdata_img v) {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;
            return o;
        }
        fixed4 frag(v2f i) : SV_Target {
            return tex2D(_MainTex, i.uv);
        }
        ENDCG
    }
    Pass {
        ZTest Always
        Cull Off
        ZWrite Off
        Fog { Mode Off }
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        sampler2D _MainTex;
        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };
        v2f vert(appdata_img v) {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;
            return o;
        }
        fixed4 frag(v2f i) : SV_Target {
            return tex2D(_MainTex, i.uv);
        }
        ENDCG
    }	}
}
