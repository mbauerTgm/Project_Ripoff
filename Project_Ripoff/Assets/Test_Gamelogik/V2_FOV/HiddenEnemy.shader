Shader "Custom/HiddenEnemy" {
    Properties {
        // Standard-Eigenschaften für Farbe, Textur und Glanz
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 20

        // DIE MAGIE: Zeichne dieses Objekt NUR, wenn die Maske eine "1" sagt (also der Sichtkegel darauf trifft)
        Stencil {
            Ref 1
            Comp Equal
        }

        CGPROGRAM
        // Physikbasiertes Standard-Lichtmodell
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o) {
            // Textur und Farbe anwenden
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}