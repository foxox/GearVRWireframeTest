Shader "Unlit/CheckerClip"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 3.0

#include "UnityCG.cginc"

			// note: no SV_POSITION in this struct
			struct v2f
			{
				float2 uv : TEXCOORD0;
				fixed3 color : COLOR0;
			};

			v2f vert(appdata_base v,
				float4 vertex : POSITION, // vertex position input
				//float2 uv : TEXCOORD0, // texture coordinate input
				out float4 outpos : SV_POSITION // clip space position output
				)
			{
				v2f o;
				o.uv = v.texcoord;
				outpos = UnityObjectToClipPos(vertex);
				o.color = v.normal * 0.5 + 0.5;
				return o;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f i, UNITY_VPOS_TYPE screenPos : VPOS) : SV_Target
			{
				// screenPos.xy will contain pixel integer coordinates.
				// use them to implement a checkerboard pattern that skips rendering
				// 4x4 blocks of pixels

				// checker value will be negative for 4x4 blocks of pixels
				// in a checkerboard pattern

				//screenPos.x += sin(_Time * 20) * 10;
				screenPos.y += cos(_Time * 200 + screenPos.y * 0.05) * 2;
				screenPos.x += sin(_Time * 200 + screenPos.x * 0.05) * 2;

				screenPos.xy = floor(screenPos.xy * 0.25) * 0.5;
				float checker = -frac(screenPos.r + screenPos.g);

				// clip HLSL instruction stops rendering a pixel if value is negative
				clip(checker);

				// for pixels that were kept, read the texture and output it
				fixed4 c = fixed4(i.color,1);// tex2D(_MainTex, i.uv);
				return c;
			}

			ENDCG
		}
	}
}