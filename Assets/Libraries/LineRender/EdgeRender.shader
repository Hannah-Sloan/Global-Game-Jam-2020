Shader "Custom/EdgeRender"
{
	//You have to include _MainTex in properties for it to get set by blit.
	//Detailed explanation of _MainTex below.
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Threshold("Threshold", Range(0.001,1)) = 0.001
	}

	SubShader
	{
		//This is important when using a replacement shader. Currently this is a blit shader so this is here just in case.
		Tags { "RenderType" = "Opaque" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			//Remember if you don't know what something does from here, you can just look at the source code:
			//	C:\Program Files\Unity\Hub\Editor\2019.1.10f1\Editor\Data\CGIncludes
			//	https://catlikecoding.com/unity/tutorials/rendering/part-2/from-object-to-image/include-files.png
			#include "UnityCG.cginc"

			//The screen render before being blit with this shader. Holds RGB of pixels and is screen width/height.
			sampler2D _MainTex;
			float _Threshold;

			/*
			Make sure to ask the camera to generate this if it's coming in empty.
			Packed texture including normal xyz and normalized depth between camera and far clip plane.
			https://docs.unity3d.com/Manual/SL-CameraDepthTexture.html
			*/
			sampler2D _CameraDepthNormalsTexture;
			//Texture_Name_TexelSize provides texel size of a texture courtesy of Unity. 
			//Aids in getting new uv co-ordinates after translation in pixel units.
			float4 _CameraDepthNormalsTexture_TexelSize;

            struct appdata
            {
                float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
            };

            struct v2f
            {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 screenPos : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.screenPos = ComputeScreenPos(o.vertex);
				o.uv = v.uv;
                return o;
            }

			float logWithBase(float base, float x) 
			{
				return log(x) / log(base);
			}

			//Function allows me to quickly change how each pixel is colored before comparing colors.
			//Function is fun to play with to change when lines are drawn.
			float4 getColor(float3 normalValues, float depth)
			{
				depth = 1/(depth);
				//depth = log10(depth);
				depth = logWithBase(1.2, depth);
				return float4(normalValues.x, normalValues.y, depth, 1);
			}

			fixed4 frag(v2f i) : COLOR
			{
				//First step is to:

				/* 
					Generate Pixel Cross:
					 _______________
					|				|
					|	b	-	d	|
					|	-	a	-	|
					|	e	-	c	|
					|_______________|
					
					Where a is at i.screenPos.xy.
				*/

				/*
				DecodeDepthNormal is provided by UnityCG.cginc:

					inline void DecodeDepthNormal( float4 enc, out float depth, out float3 normal)
					{
						depth = DecodeFloatRG (enc.zw);
						normal = DecodeViewNormalStereo (enc);
					}
				*/

				float3 tempNormalValues;
				float tempDepth;

				//A
				DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, i.screenPos.xy), tempDepth, tempNormalValues);
				float4 aColor = getColor(tempNormalValues, tempDepth);
				//Since a is the pixel in question it's useful to obtain all of it's info, even if getColor does not find it pertinent.
				//I save this for later here.
				float4 a = float4(tempNormalValues.x, tempNormalValues.y, tempNormalValues.z, tempDepth);

				float2 tempOffset;
				float2 texelSize = _CameraDepthNormalsTexture_TexelSize.xy;

				//B
				tempOffset = float2(-1, 1);
				DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, float2(i.screenPos.xy + (tempOffset * texelSize))), tempDepth, tempNormalValues);
				float4 bColor = getColor(tempNormalValues, tempDepth);

				//C
				tempOffset = float2(1, -1);
				DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, float2(i.screenPos.xy + (tempOffset * texelSize))), tempDepth, tempNormalValues);
				float4 cColor = getColor(tempNormalValues, tempDepth);

				//D
				tempOffset = float2(1, 1);
				DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, float2(i.screenPos.xy + (tempOffset * texelSize))), tempDepth, tempNormalValues);
				float4 dColor = getColor(tempNormalValues, tempDepth);

				//E
				tempOffset = float2(-1, -1);
				DecodeDepthNormal(tex2D(_CameraDepthNormalsTexture, float2(i.screenPos.xy + (tempOffset * texelSize))), tempDepth, tempNormalValues);
				float4 eColor = getColor(tempNormalValues, tempDepth);


				//Second Step is to if pixel a is an edge:

				float4 averagePixelCol = ((bColor + cColor + dColor + eColor) / (float)4);
				float pixelDifference = length(aColor - averagePixelCol);

				//This line is fun to play with different equations.
				float threshold = _Threshold; //Set small value
				//float threshold = max(0.001, a.w * 1); //Less sensitive over distance

				if (pixelDifference > threshold) 
				{
					//Define line color:

					//return float4(a.w * 1.1, a.w * 1.1, a.w * 1.1, 1); //Changes line color based on distance.
					/*float4(.015, .015, .015, 1) **/  //Dark grey
					//return tex2D(_MainTex, i.screenPos.xy) * .25; //25% of pixels original color
					return tex2D(_MainTex, i.screenPos.xy) * a.w; //Color becomes closer to objects original color based on distance.
					//float4(aColor.zzz, 1);
					//return 0; //Black Lines
				}

				//Define non-line color:
				//return a; //This lets you see the normals. Helps debug.
				//return aColor; //This lets you see what the shader sees, helps you debug.
				//return float4(aColor.zzz, 1); //Shader Depth Value.
				//return float4(a.w, 1-a.w, a.w, 1); //Can also use other screen space effects when not drawing lines. 
				return tex2D(_MainTex, i.screenPos.xy); //Replaces this pixel with pixel previously there.
            }
            ENDCG
        }
    }

	//FallBack "Diffuse"
}
