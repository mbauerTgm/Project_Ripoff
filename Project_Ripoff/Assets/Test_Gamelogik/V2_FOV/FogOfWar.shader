Shader "Custom/FogOfWar" {
    Properties {
        _Color ("Fog Color", Color) = (0,0,0,0.95) // Schwarz und fast ganz blickdicht
    }
    SubShader {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        
        // Zeichne diese Farbe ‹BERALL, auﬂer da wo die Maske eine "1" hinterlassen hat
        Stencil {
            Ref 1
            Comp NotEqual 
        }
        
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata { float4 vertex : POSITION; };
            struct v2f { float4 pos : SV_POSITION; };
            
            float4 _Color;
            
            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            
            half4 frag(v2f i) : SV_Target {
                return _Color;
            }
            ENDCG
        }
    }
}