Shader "Unlit/3D/WaterVoronoi"
{
    Properties
    {
		_BaseColor("Base Color",Color) = (1,1,1,1)
		_Alpha("Alpha",Range(0.0,1.0)) = 0.8
		_RipplesSpeed("Voronoi Speed",Float) = 1
		_RipplesDensity("Voronoi Density",Float) = 7.5
		_RipplesSlimness("Voronoi Slimness",Float) = 4.5
		[HDR]
		_RipplesColor("Voronoi Color",Color) = (1,1,1,1)
		[Space]
		_WaveSpeed("Wave Speed",Float) = 1
		[Space]
		_Edge("Edge",Range(0.0,1.0)) = 1
		_Offset("Offset",Range(1.0,10.0)) = 10

		_WaveFlow("Wave Flow",Range(0.0,1.0)) = 0
	}
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent-1"}
		Blend SrcAlpha OneMinusSrcAlpha
		Cull off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
			#include"../cginc/SmyMethod_Noise.cginc"
			#include"../cginc/SmyMethod.cginc"
			#include"../cginc/SmyMethod_Depth.cginc"


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal:NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float4 screenPos:TEXCOORD1;
            };

			fixed4 _BaseColor;
			float _Alpha;
			float _RipplesSpeed;
			float _RipplesDensity;
			float _RipplesSlimness;
			fixed4 _RipplesColor;
			float _WaveSpeed;
			fixed4 _LightColor0;

			float _Edge;
			float _Offset;

			float _WaveFlow;

            v2f vert (appdata v)
            {
                v2f o;

                o.uv = v.uv;
				float t = _Time.x * _WaveSpeed;
				float2 uv = o.uv * 1 + t;
				float Out;
				GradientNoise_Float(uv, 5, Out);

				float3 m = Out * v.normal;

				v.vertex.xyz += m * _WaveFlow;

                o.vertex = UnityObjectToClipPos(v.vertex);
				o.screenPos = GetScreenPosition(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				//デプスバッファと実際のオブジェクトの距離の差分をとる
				float4 nS = i.screenPos;
				half2 suv = FragTransformScreenPos(nS);
				float depth = SceneDepth_Eye(suv);

				//デプスバッファとオブジェクトとの距離を測る
				//スクリーン座標のwにはカメラとオブジェクトとの距離が格納されているらしい
				float sub = (depth - i.screenPos.w) * _Offset;

				sub -= _Edge;

				sub = smoothstep(0.7, 0.2, sub);


				float t = _Time.y * _RipplesSpeed;
				float Out;
				float cells;

				float2 uv;
				RadialShader_Float(i.uv, 0.5, 1, 0, uv);

				Voronoi_Float(uv, t, _RipplesDensity, Out, cells);

				Out = pow(Out, _RipplesSlimness);
				fixed4 col = _RipplesColor * Out;
				float grad;
				GradientNoise_Float(uv, 5, grad);
				grad = remap(grad, 0.0f, 1.0f, -0.01f, 0.01);
				col += sub / 2.0f - grad ;
				return fixed4((_BaseColor.xyz * _LightColor0.xyz) + col.xyz,_Alpha);
            }
            ENDCG
        }
    }
	FallBack"Transparent/Diffuse"
}
