Shader "SmyCustom/3D/UnlitRimLight"
{
    Properties
    {
		_RimPower("Rim Power",Range(0.1,5.0)) = 2
		[HDR]
		_RimColor("Rim Color",Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
			#include "../cginc/SmyMethod.cginc"
			#include "../cginc/SmyMethod_Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
				float3 normal:NORMAL;
            };

            struct v2f
            {
				float3 normal:NORMAL;
				float3 worldPos:TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

			float _RimPower;
			float4 _RimColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = WORLD_POSITION(v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float c = UnlitRimLight(i.normal,i.worldPos,_RimPower);
				return _RimColor * c;
            }
            ENDCG
        }
    }
}
