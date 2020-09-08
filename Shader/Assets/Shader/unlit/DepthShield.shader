Shader "SmyCustom/Graphics/Shield" {
	Properties{
		[HDR]
		_Color("Color",Color) = (1,1,1,1)
		_Edge("Edge",Range(0.0,1.0)) = 1
		_RimPower("RimPower",Range(1.00,5.00)) = 3
		_Offset("f",Range(1.0,10.0)) = 1
	}
	SubShader{
		Tags {"Queue" = "Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		ZWrite off
		Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include"../cginc/SmyMethod.cginc"
			#include"../cginc/SmyMethod_Depth.cginc"

			struct appdata {
				float4 vertex:POSITION;
				float2 uv:TEXCOORD0;
				float3 normal:NORMAL;
			};

			struct v2f {
				float4 position : SV_POSITION;
				float2 uv:TEXCOORD0;
				float4 screenPos:TEXCOORD1;
				float3 normal:NORMAL;
				float3 worldPos:TEXCOORD2;
			};

			float _Edge;
			fixed4 _Color;
			float _RimPower;
			float _Offset;

			v2f vert(appdata v) {
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.screenPos = GetScreenPosition(o.position);
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.worldPos = WORLD_POSITION(v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : COLOR {
				//デプスバッファと実際のオブジェクトの距離の差分をとる
				float4 nS = i.screenPos;
				half2 uv = FragTransformScreenPos(nS);
				float depth = SceneDepth_Eye(uv);

				//デプスバッファとオブジェクトとの距離を測る
				//スクリーン座標のwにはカメラとオブジェクトとの距離が格納されているらしい
				float sub = (depth - i.screenPos.w) * _Offset;

				sub -= _Edge;

				sub = smoothstep(0.7, 0.2, sub);

				fixed4 col = _Color;

				//リムライト計算
				float3 viewDir = _WorldSpaceCameraPos.xyz - i.worldPos;
				viewDir = normalize(viewDir);
				float3 n = normalize(i.normal);
				float d = dot(viewDir, n);
				d = abs(d);
				d = 1 - d;
				d = pow(d, _RimPower);
				sub += d;
				col.a = sub;
				return col;
			}
			ENDCG
		}
	}
}