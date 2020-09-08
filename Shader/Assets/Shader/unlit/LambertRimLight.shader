Shader"SmyCustom/Graphics/LambertRimLight"{
	Properties{
		_MainColor("Color",Color) = (1,1,1,1)
		_Intensity("Intensity",Range(0.0,5.0)) = 1.0
		[HDR]_RimColor("RimColor",Color) = (1,1,1,1)
		_RimPower("Rim Power",Range(1.0,5.0)) = 3.0
	}
	SubShader{
		Pass{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include"UnityCG.cginc"

			struct appdata {
				float4 vertex:POSITION;
				float3 normal:NORMAL;
			};

			struct v2f {
				float4 position:SV_POSITION;
				float3 normal:NORMAL;
				float4 worldPosition:TEXCOORD0;
			};

			v2f vert(appdata v) {
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.worldPosition = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}

			fixed4 _MainColor;
			float _Intensity;
			fixed4 _RimColor;
			float _RimPower;

			fixed4 frag(v2f i) :SV_Target{
				//Lambert
				float3 L = _WorldSpaceLightPos0.xyz;
				float N = normalize(i.normal);
				float d = dot(N, L);
				fixed4 col = saturate((float4(d, d, d, 1) * _MainColor) * _Intensity);

				//RimLight
				//頂点のワールド座標からカメラの位置のベクトルを求める
				//※正規化は視線ベクトルのみ
				float3 E = normalize(i.worldPosition.xyz - _WorldSpaceCameraPos.xyz);
				d = 1 - saturate(abs(dot(i.normal,E)));
				d = pow(d, _RimPower);
				fixed4 rim = _RimColor * d;
				return col + rim;
			}
			ENDCG
		}
	}
}