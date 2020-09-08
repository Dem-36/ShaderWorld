Shader "SmyCustom/UI/CircleEffect"
{
	Properties
	{
		_Distance("Distance",Float) = 1
		_Center("Center",Vector) = (0.5,0.5,0,0)
		_Alpha("Alpha",Range(0.0,1.0)) = 1.0
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" "Queue" = "Transparent"}

		Blend SrcAlpha OneMinusSrcAlpha
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
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			float _Distance;
			float2 _Center;
			float _Alpha;

			fixed4 frag(v2f i) : SV_Target
			{
				float2 uv = i.uv - _Center;
				uv.x *= 16.0f / 9.0f;
				float dist = distance(uv, fixed2(0, 0));
				fixed4 col = 0;
				col.a = _Alpha * step(_Distance, dist);
				return col;
			}
			ENDCG
		}
	}
}
