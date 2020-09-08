Shader "SmyCustom/2D/2DOutLine"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		[HDR]
		_OutLineColor("OutLine",Color) = (0,0,0,1)
		_Offset("Offset",Range(0.00,0.05)) = 0.0017
    }
    SubShader
    {
        Tags{"RenderType" = "Transparent" "Queue" = "Transparent"}
        Cull Off
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			fixed4 _OutLineColor;
			float _Offset;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 textureCol = tex2D(_MainTex, i.uv);

                fixed4 left = tex2D(_MainTex, i.uv + float2(_Offset,0.0));  //左
                fixed4 right = tex2D(_MainTex, i.uv + float2(-_Offset,0.0)); //右
				fixed4 up = tex2D(_MainTex, i.uv + float2(0.0, - _Offset));//上
				fixed4 bottom = tex2D(_MainTex, i.uv + float2(0.0, _Offset));//下

				fixed outline_rl = left.a + right.a;
				fixed outline_ub = up.a + bottom.a;

				fixed outline_A = outline_rl + outline_ub;
				outline_A = clamp(outline_A, 0, 1);

				fixed outline = outline_A - textureCol.a; //ここでoutlineの差分を出す
				fixed4 col = _OutLineColor * outline;

				fixed4 tex = textureCol * textureCol.a;
				//combine
				textureCol = fixed4(tex.r, tex.g, tex.b, textureCol.a);

                return textureCol + col;
            }
            ENDCG
        }
    }
}
