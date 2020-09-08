using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Smy.Math;

/// <summary>
/// 集中線制御
/// </summary>
[RequireComponent(typeof(ShaderController))]
public class ConcentratedLineController : MonoBehaviour
{
    private ShaderController controller = null;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ShaderController>();
        controller.Initialize(GetComponent<Image>());
    }

    /// <summary>
    /// ワールド座標を起点に位置を指定する
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="offset"></param>
    public void SetLineCenter(Vector3 worldPosition,Vector2 offset = new Vector2())
    {
        //ワールド座標をスクリーン座標に変換する
        Vector3 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPosition);
        //0～1に変換する
        pos.x /= Screen.width;
        pos.y /= Screen.height;
        //offset = offset.Clamp(-2.0f, 2.0f);
        controller.SetVector("_Center", pos + offset.Vec2to3());
    }

    /// <summary>
    /// uv座標を起点に位置を指定する
    /// </summary>
    /// <param name="uv"></param>
    /// <param name="offset"></param>
    public void SetLineCenter_UV(Vector2 uv, Vector2 offset = new Vector2())
        => controller.SetVector("_Center", uv + offset);

    /// <summary>
    /// ノイズの数を指定する
    /// (数値に依存はしない)
    /// </summary>
    /// <param name="scale"></param>
    public void SetLengthScale(float scale = 200.0f)
    {
        scale = Mathf.Clamp(scale, 0.0f, 500.0f);
        controller.SetFloat("_LengthScale", scale);
    }

    /// <summary>
    /// 線の長さを指定
    /// </summary>
    /// <param name="lineLength"></param>
    public void SetLineLength(float lineLength = 1.0f)
    {
        lineLength = Mathf.Clamp(lineLength, 0.0f, 10.0f);
        controller.SetFloat("_RadialScale", lineLength);
    }

    /// <summary>
    /// 線の回転スピード調整
    /// </summary>
    /// <param name="rotateSpeed"></param>
    public void SetRotateSpeed(float rotateSpeed = 0.0f)
    {
        rotateSpeed = Mathf.Clamp(rotateSpeed, 0.0f, 200.0f);
        controller.SetFloat("_RotateSpeed", rotateSpeed);
    }

    /// <summary>
    /// ノイズの強さ指定
    /// </summary>
    /// <param name="noisePower"></param>
    public void SetNoisePower(float noisePower = 0.0f)
    {
        noisePower = Mathf.Clamp(noisePower, 0.0f, 100.0f);
        controller.SetFloat("_NoisePower", noisePower);
    }

    private void OnDestroy()
    {
        SetLineCenter_UV(new Vector2(0.5f, 0.5f));
        SetLineLength(0.0f);
        SetRotateSpeed();
        SetNoisePower();
    }
}
