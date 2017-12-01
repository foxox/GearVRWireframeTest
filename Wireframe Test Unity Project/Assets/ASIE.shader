Shader "John/ASIE"
{
	Properties
	{
		//_BWBlend("Black & White blend", Range (0, 1)) = 0
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_DepthImage ("Depth Image", 2D) = "white" {}
		_PatternImage ("Pattern Image", 2D) = "white" {}
		_PatternScale ("Scale of Pattern Tiling", Float) = 1
	}

	SubShader
	{
		Pass
		{

			CGPROGRAM
			
#pragma vertex vert_img
#pragma fragment frag

#include "UnityCG.cginc"

			//uniform float _BWBlend;
			uniform sampler2D _MainTex;
			//uniform sampler2D _DepthImage;
			uniform sampler2D _OffsetImage;
			uniform sampler2D _PatternImage;
			uniform float _PatternScale;

			float4 frag(v2f_img i) : COLOR
			{
				float4 screenColor = tex2D(_MainTex, i.uv);


				half2 patternuv = i.uv;
				float offset = tex2D(_OffsetImage, i.uv).r;

				patternuv.x = offset * 10.0f;
				patternuv.y *= 4;

				float4 patternColor = tex2D(_PatternImage, patternuv);

				float4 result = patternColor;
				//result = offset;
				return result;
			}

			ENDCG
		}
	}
}


