Shader "John/ViewWarpIE"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_BWBlend("Black & White blend", Range (0, 1)) = 0
	}

	SubShader
	{
		Pass
		{

			CGPROGRAM
			
#pragma vertex vert_img
#pragma fragment frag

#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _BWBlend;

			float4 frag(v2f_img i) : COLOR
			{
				half2 uv = i.uv;
				uv.x = i.uv.x + sin(i.uv.y * 10 + _Time * 10) * 0.1;
				//uv.y = cos(i.uv.y + _Time);

				float4 c = tex2D(_MainTex, uv);

				//float lum = c.r*.3 + c.g*.59 + c.b*.11;
				//float3 bw = float3(lum, lum, lum);

				float4 result = c;
				//result.rgb = lerp(c.rgb, bw, _BWBlend);
				return result;
			}

			ENDCG
		}
	}
}


