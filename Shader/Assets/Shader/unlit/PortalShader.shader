//ワープホール:https://www.youtube.com/watch?v=w0znZIuvQ2I
Shader "SmyCustom/Graphics/PortalShader"
{
    Properties
    {
		_MaskTex("Mask Texture",2D) = "white"{}
		_TwirlAmount("Twirl Amount",Float) = 30
		_TwirlSpeed("Twirl Speed",Float) = 0.5
		_VolonoiScale("Scale",Float) = 22
		_VolonoiDistance("Distance",Range(0,1)) = 1
		[HDR]_EmissionColor("Emission Color",Color)=(1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Opaque"="Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
			#include"../cginc/SmyMethod.cginc"
			#include"../cginc/SmyMethod_Noise.cginc"

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

			sampler2D _MaskTex;
			float4 _MaskTex_ST;
			float _TwirlAmount;
			float _TwirlSpeed;
			float _VolonoiScale;
			float _VolonoiDistance;
			float4 _EmissionColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv =TRANSFORM_TEX(v.uv,_MaskTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed2 s = _Time.w * _TwirlSpeed;
				fixed4 twirl;
				Twirl(i.uv, float2(0.5, 0.5), _TwirlAmount, s, twirl.xy);
				//Twirlの結果をUV座標として手渡す(ShaderGraph内ではEmission扱い)
				float vol;
				float cell;
				Voronoi_Float(i.uv + twirl.xy, _VolonoiScale, _VolonoiDistance, vol, cell);
				//Emission
				fixed4 emissiveCol = _EmissionColor * vol;
				fixed4 tex = tex2D(_MaskTex, i.uv);
				emissiveCol.a = emissiveCol.r;
                return emissiveCol * tex;
            }
            ENDCG
        }
    }
}
