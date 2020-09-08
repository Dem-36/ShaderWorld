using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Smy.Math;

[RequireComponent(typeof(ShaderController))]
public class CircleEffectController : MonoBehaviour
{
    private ShaderController controller = null;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ShaderController>();
        controller.Initialize(GetComponent<Image>());
    }

    /// <summary>
    /// 円の位置を指定する
    /// </summary>
    /// <param name="worldPosition">注目したいオブジェクトのワールド座標</param>
    /// <param name="offset">オフセット値(-2～2)が目安</param>
    public void CircleEffectPosition(Vector3 worldPosition ,Vector2 offset = new Vector2())
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
    /// (左下が原点,真ん中は(0.5,0.5))
    /// </summary>
    /// <param name="uvPosition"></param>
    /// <param name="offset"></param>
    public void CircleEffectPosition_UV(Vector2 uvPosition, Vector2 offset = new Vector2())
        => controller.SetVector("_Center", uvPosition + offset);

    /// <summary>
    /// 円の大きさを指定する
    /// </summary>
    /// <param name="distance"></param>
    public void SetCircleDist(float distance)
        => controller.SetFloat("_Distance", distance);

    /// <summary>
    /// アルファ値の指定
    /// </summary>
    /// <param name="alpha"></param>
    public void SetAlpha(float alpha)
    {
        alpha = Mathf.Clamp(alpha, 0.0f, 1.0f);
        controller.SetFloat("_Alpha", alpha);
    }

    private void OnDestroy()
    {
        CircleEffectPosition_UV(new Vector2(0.5f, 0.5f));
        SetCircleDist(1.5f);
        SetAlpha(0.0f);
    }
}
