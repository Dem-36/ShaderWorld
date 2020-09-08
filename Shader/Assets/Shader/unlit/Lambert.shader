Shader"SmyCustom/Graphics/Lambert"{
	Properties{
		_MainColor("Color",Color) = (1,1,1,1)
		_Intensity("Intensity",Range(0.0,5.0)) = 1.0
		[KeywordEnum(NORMAL,HALF,HALFD)]
		_TYPE("TYPE",Float) = 0
	}
	SubShader{
		Pass{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include"UnityCG.cginc"
			#include"../cginc/SmyMethod.cginc"
			#include"../cginc/SmyMethod_Lighting.cginc"
			#pragma multi_compile _TYPE_NORMAL _TYPE_HALF _TYPE_HALFD

			struct appdata {
				float4 vertex:POSITION;
				float3 normal:NORMAL;
			};

			struct v2f {
				float4 position:SV_POSITION;
				float3 normal:NORMAL;
			};

			v2f vert(appdata v) {
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				return o;
			}

			fixed4 _MainColor;
			float _Intensity;

			fixed4 frag(v2f i) :SV_Target{
				float3 lambert;
				#ifdef _TYPE_NORMAL
					lambert = Diffuse(i.normal,_Intensity, _MainColor);
				#elif _TYPE_HALF
					lambert = HalfDiffuse(i.normal,_Intensity, _MainColor);
				#elif _TYPE_HALFD
					lambert = HalfDiffuse(i.normal,_Intensity, _MainColor, 2.0);
				#endif
				return saturate(float4(lambert, 1) * _Intensity);
			}
			ENDCG
		}
	}
}