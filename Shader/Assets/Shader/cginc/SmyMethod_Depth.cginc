//Unityで使用するDepthメソッド

sampler2D _CameraDepthTexture;

//vertで求めたスクリーン座標をビューポート変換する
float2 FragTransformScreenPos(float4 screenPosition)
{
    return screenPosition.xy / screenPosition.w;
}

//深度値をカメラからのワールド空間における距離として取得
float SceneDepth_Eye(float2 screenPos)
{
    float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,screenPos);
    depth = LinearEyeDepth(depth);
    return depth;
}

//深度値を0～1の線形な値に変換
float SceneDepth_Default(float2 screenPos)
{
    float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture,screenPos);
    depth = Linear01Depth(depth);
    return depth;
}