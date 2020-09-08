Shader"SmyCustom/Graphics/Specular"{
	Properties{
		_AmbientColor("Ambient Color",Color) = (1,1,1,1)
		_AmbientIntensity("Ambient Intensity",Range(1.0,5.0)) = 1.0
		[Space]
		_DiffuseColor("Diffuse Color",Color) = (1,1,1,1)
		_DiffuseIntensity("Diffuse Intensity",Range(1.0,5.0)) = 1.0
		[Space]
		_SpecularColor("Specular Color",Color) = (1,1,1,1)
		_SpecularIntensity("Specular Intensity",Range(1.0,5.0)) = 1.0
		_SpecularPower("Specular Power",Range(1.0,100.0)) = 1.0
	}
	SubShader{
		Pass{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include"UnityCG.cginc"
			#include"../cginc/SmyMethod_Lighting.cginc"

			struct appdata {
				float4 vertex:POSITION;
				float3 normal:NORMAL;
			};

			struct v2f {
				float4 position:SV_POSITION;
				float3 normal:NORMAL;
				float4 worldPosition:TEXCOORD0;
				float3 viewDir:TEXCOORD1;
			};

			v2f vert(appdata v) {
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.worldPosition = mul(unity_ObjectToWorld, v.vertex);
				o.viewDir = WorldSpaceViewDir(v.vertex);
				return o;
			}

			fixed4 _AmbientColor;
			float _AmbientIntensity;
			fixed4 _DiffuseColor;
			float _DiffuseIntensity;
			fixed4 _SpecularColor;
			float _SpecularIntensity;
			float _SpecularPower;

			fixed4 frag(v2f i) :SV_Target{
				//Ambient(環境光)
				fixed4 ambient = saturate(_AmbientColor * _AmbientIntensity);

				fixed4 diffuse = HalfDiffuse(i.normal, _DiffuseIntensity, _DiffuseColor);

				fixed4 specular = Specular(i.normal, i.viewDir, _SpecularIntensity, _SpecularColor, _SpecularPower);
				fixed4 col = ambient + diffuse + specular ;

				return col;
			}
			ENDCG
		}
	}
}