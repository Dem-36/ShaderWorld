//Lightingのメソッドをまとめたもの

//RimLightの計算
float UnlitRimLight(float3 normal, float3 worldPos, float rimPower) {
	//頂点のワールド座標からカメラの位置のベクトルを求める
	float3 L = normalize(worldPos.xyz - _WorldSpaceCameraPos.xyz);
    //dotで値が0だと垂直で、1だと平行 1-して値を逆にする
	float d = 1 - saturate(abs(dot(normal, L)));
	d = pow(d, rimPower);
	return d;
}

//拡散反射光
//法線は正規化せずに渡す intensity = 光の強さ
fixed4 Diffuse(float3 normal,float intensity,fixed4 color){
    float3 L = mul(UNITY_MATRIX_M,_WorldSpaceLightPos0.xyz);
    float d = dot(normalize(normal),L);
    fixed4 col = max(0.0,d) * intensity * color;
    return col;
}

//拡散反射光 Half
fixed4 HalfDiffuse(float3 normal,float intensity,fixed4 color,float power = 1){
    float3 L = normalize(_WorldSpaceLightPos0.xyz);
    float d = dot(normalize(normal),L);
    fixed4 col = pow(d * 0.5 + 0.5,power) * intensity * color;
    return col;
}

//鏡面反射光の計算
//viewDirはvertでWorldSpaceViewDir(v.vertex)で求める
fixed4 Specular(float3 normal,float3 viewDir,float intensity,fixed4 color,float power){
    float3 L = normalize(_WorldSpaceLightPos0.xyz);
    float3 E = normalize(viewDir);
    float3 N = normal;

    //反射ベクトルを求める
    float3 R = normalize(-E + 2.0 * dot(N,E) * N);
    fixed4 col = intensity * color * pow(max(0.0, dot(L,R)),power);
    return col;
}

//フレネル
float FresnelEffect(float3 normal,float3 viewDir,float power){
    float3 n = normalize(normal);
    float3 v = normalize(viewDir);
    return pow((1.0f - saturate(dot(n,v))),power);
}
