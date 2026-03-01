Shader "Custom/VisionMask" {
    SubShader {
        // Wir rendern das nach der normalen Welt, aber vor der Dunkelheit
        Tags { "RenderType"="Opaque" "Queue"="Geometry+10" }
        ColorMask 0 // Macht das Objekt komplett unsichtbar
        ZWrite Off  // Verhindert Clipping-Fehler
        
        // Das ist die Magie: Wir schreiben eine "1" in den Stencil Buffer (die Maske)
        Stencil {
            Ref 1
            Comp Always
            Pass Replace 
        }
        
        Pass {}
    }
}