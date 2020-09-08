Shader "SmyCustom/UI/ConcentratedLine"
{
	Properties
	{
		_Color("Color",Color) = (1,1,1,1)
		_Center("Center",Vector) = (0.5,0.5,0,0)
		//線の数
		_Scale("Noise Scale",Float) = 1
		//線の長さ
		_RadialScale("Radial Scale",Float) = 1
		_LengthScale("Length Scale",Float) = 200
		//回転速度
		_RotateSpeed("RotateSpeed",Float) = 0
		//線の挙動
		_NoisePower("Noise Power",Range(0.0,100.0)) = 0
    }
    SubShader
    {
		Tags{"RenderType" = "Transparent" "Queue" = "Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
			#include "../cginc/SmyMethod_Noise.cginc"
			#include "../cginc/SmyMethod.cginc"

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

			float _Scale;
			fixed3 _Color;
			fixed2 _Center;
			float _RadialScale;
			float _LengthScale;
			float _RotateSpeed;
			float _NoisePower;

            fixed4 frag (v2f i) : SV_Target
            {
				float2 polar;
				//uvを極座標に変換する
				PolarCoordinates(i.uv, _Center, _RadialScale, _LengthScale, polar);
				float x = polar.x;
				float y = polar.y + _Time.y * _RotateSpeed;
				float n;
				//ノイズを作成する
				GradientNoise_Float(float2(y,_Time.y*_NoisePower), _Scale, n);
				//下部分が黒くなるように調整
				//第５引数で白の割合が変化する
				float re = remap(x, 0.1, 0.85, -0.06, 0.68);
				re = 1 - re;
				n += re;
				//0.4 <= nの部分は0.0
				//n <= 0.86の部分は1.0
				// 0.4 < n < 0.86なら滑らかに補完する
				n = smoothstep(0.4, 0.86, n);
				return fixed4(n,n,n,1 - n) + fixed4(_Color,0);
            }
            ENDCG
        }
    }
}
