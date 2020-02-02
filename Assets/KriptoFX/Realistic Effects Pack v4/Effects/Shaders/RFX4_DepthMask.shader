Shader "KriptoFX/RFX4/Decal/DepthMask" {
	Properties{
		_Cutoff("Cutoff", Range(0,1)) = 0
		_Mask("Mask", 2D) = "white" {}
	}
	SubShader{
			Tags{ "Queue" = "Geometry-1" }
			ColorMask 0
			ZWrite On
 Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
           
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
               return 0;
            }
        ENDCG
    }
	}

}
