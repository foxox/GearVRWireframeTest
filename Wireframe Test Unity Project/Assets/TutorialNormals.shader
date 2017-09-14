Shader "Tutorial/TutorialNormals"
{
	SubShader
	{
		Pass
		{

			CGPROGRAM

#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

			struct v2f
			{
				float4 pos : SV_POSITION;
				fixed3 color : COLOR0;
			};

			v2f vert(appdata_base v)
			{
				float4 normsq;
				float3 normal_normalized = v.normal * 1.5 + 0.5;
				normsq.x = normal_normalized.x*normal_normalized.x;
				normsq.y = normal_normalized.y*normal_normalized.y;
				normsq.z = normal_normalized.z*normal_normalized.z;

				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex + normsq * 0.1);
				o.color = v.normal * 0.5 + 0.5;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return fixed4(i.color, 1);
			}
		
				ENDCG

		}
	}
}
