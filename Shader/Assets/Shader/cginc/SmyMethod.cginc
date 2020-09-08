#define PI 3.14159265359
#define Gravity 9.8

//頂点のワールド座標を求める
float3 WORLD_POSITION(float4 vertex){
    return mul(unity_ObjectToWorld,vertex);
}

//グレースケール
float GrayScale(fixed3 color){
    return color.r*0.6f+color.g*0.1f+color.b*0.1f;
}

//t: 時間 a: 明るさ　b: ぼかし c: 色のスケーリング
//参照:http://iquilezles.org/www/articles/palettes/palettes.htm
float3 colorPalette( float t,  float3 a,  float3 b,  float3 c,float3 d) {
    return a + b * cos(PI*2*(c*t + d));
}

//画面比の取得
float ScreenRatio(){
    return _ScreenParams.x/_ScreenParams.y;
}

//0.0～yの値に収まる数値を返す
float mod(float x, float y) {
	return x - y * floor(x / y);
}


//Twirl:https://docs.unity3d.com/Packages/com.unity.shadergraph@6.9/manual/Twirl-Node.html
void Twirl(float2 UV, float2 Center, float Strength, float2 Offset, out float2 Out)
{
	//渦の中心を求める
	float2 delta = UV - Center;
	//渦の動きの強さ＊中心のベクトルの長さをかける
	float angle = Strength * length(delta);
	//アルキメデスの螺旋方程式x = a*cos()  y = a*sin()
	float x = cos(angle) * delta.x - sin(angle) * delta.y;
	float y = sin(angle) * delta.x + cos(angle) * delta.y;
	//uvとして出力する(Centerで真ん中に合わせ、Offsetで調節する)
	Out = float2(x + Center.x + Offset.x, y + Center.y + Offset.y);
}

//valの値がinMin～inMax内の場合、outMin～outMaxの指定された割合に変換して出力する
float remap(float val, float inMin, float inMax, float outMin, float outMax)
{
    return clamp(outMin + (val - inMin) * (outMax - outMin) / (inMax - inMin), outMin, outMax);
}

//ノーマルマップをブレンドする(簡易版)
//普通はUnityCG.cgincのBlendNormalsを使う
fixed3 BlendNormal_Simple(fixed3 normal1,fixed3 normal2){
    return normalize(fixed3(normal1.xy + normal2.xy,normal1.z * normal2.z));
}


//SurfaceShaderのUnpackScaleNormalをUnlitでも使用可能にしたもの。
//主にバンプマップの強度を手動で調整したいときに使用
half3 UnpackScaleNormal_Simple(half4 normal,half scale){
    #if defined(UNITY_NO_DXT5nm)
        return normal.xyz * 2 - 1;
    #else
        half3 n;
        n.xy = (normal.wy * 2 - 1);
        #if(SHADER_TARGET >= 30)
            nn.xy *= scale;
        #endif
        n.z = sqrt(1.0 - saturate(dot(n.xy,normal.xy)));
        return n;
    #endif
}

//スクリーン座標を取得する
float4 GetScreenPosition(float4 vertex){
    return ComputeScreenPos(vertex);
}

//GrabTextureで使用するスクリーン座標を取得
float4 GetGrabScreenPosition(float4 vertex){
    return ComputeGrabScreenPos(vertex);
}

//GrabTextureのUV座標を取得
half2 GrabTexture_UV(half4 screenPosition){
    return (screenPosition.xy/screenPosition.w) * float2(1, -1) + float2(0, 1);
}

//円を描画する
float DrawCircle(float2 uv,float3 pos,float r,float blur){
    //距離を計算
    float d = length(uv - pos);
    //ブラーをかける
    float c = smoothstep(r,r - blur,d);
    return c;
}

float Period(){
    return 2.0 * PI;
}

//波の振れ幅を求める 波高が欲しいときは二倍にする
float WaveHeight(float waveLength){
    return waveLength / 7.0;
}

//波を作成する
float CulcWave(float x,float waveLength){
    float A = WaveHeight(waveLength);
    float s = sin(Period()/waveLength*x-sqrt(Period()*Gravity/waveLength)*_Time.y);
    return A * s;
}

//波を作成する powNumで波頭を決める
float CulcWave(int powNum,float x,float waveLength){
    float A = WaveHeight(waveLength);
    float s = sin(Period()/waveLength*x-sqrt(Period()*Gravity/waveLength)*_Time.y);
    s = (s + 1) / 2;
    return A * pow(s,powNum);
}

//極座標変換
void PolarCoordinates(float2 uv,float2 center,float radialScale,float lengthScale,out float2 Out){
    float2 delta = uv - center;
    float radius = length(delta) * 2 * radialScale;
    float angle = atan2(delta.x , delta.y) * 1.0 / 6.28 * lengthScale;
    Out = float2(radius,angle);
}

fixed4 FlowMap(sampler2D flowMap,sampler2D mainTex,float2 uv,float flowSpeed,float intensity)
{
    float2 flow_Texture = tex2D(flowMap,uv).rg;
	flow_Texture *= 2;      
	flow_Texture -= 1;
	flow_Texture *= -1;

	float time = _Time.x * flowSpeed;
	float time_fmod = fmod(time, 1);

	float time_A = time + 0.5;
	time_A = fmod(time_A, 1);

	//-----------------------------------------------------

	float2 flow_Texture1 = flow_Texture * time_fmod;
	float2 flow_Texture2 = flow_Texture * time_A;

	flow_Texture1 *= intensity;
	flow_Texture2 *= intensity;
	time_fmod *= 2;

	float2 uv1 = uv + flow_Texture1;
	float2 uv2 = uv + flow_Texture2;
	time_fmod -= 1;

	time_fmod = abs(time_fmod);
	fixed4 col1 = tex2D(mainTex, uv1);
	fixed4 col2 = tex2D(mainTex, uv2);

	fixed4 color = lerp(col1, col2, time_fmod);
	color.a = color.r;
    return color;
}
