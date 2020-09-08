using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : SingletonMonoBehaviour<FadeManager>
{
    [SerializeField]
    private Image fadeImage = null;
    [SerializeField]
    private Texture2D dissolveTexture = null;
    [SerializeField]
    private ShaderController controller = null;

    public FadeLoading fadeLoading = null;

    private const string SMOOTH = "_Smooth";
    private const string DISSOLVE_TEXTURE = "_DissolveTex";

    protected override void Awake()
    {
        base.Awake();
        if (this == null)
            return;
        controller.Initialize(fadeImage);
        SetDissolveTexture(dissolveTexture);
    }

    /// <summary>
    /// fadeで使用するDissolveTextureを変更する
    /// </summary>
    /// <param name="tex"></param>
    public void SetDissolveTexture(Texture2D tex)
    {
        controller.SetTexture(DISSOLVE_TEXTURE, tex);
    }

    /// <summary>
    /// Fade in
    /// </summary>
    /// <returns></returns>
    public bool FadeIn()
    {
        float alpha = controller.GetFloat(SMOOTH) + Time.deltaTime;
        controller.SetFloat(SMOOTH, Mathf.Clamp01(alpha));
        if (alpha >= 1.0f)
            return true;
        return false;
    }

    /// <summary>
    /// Fade out
    /// </summary>
    /// <returns></returns>
    public bool FadeOut()
    {
        float alpha = controller.GetFloat(SMOOTH) - Time.deltaTime;
        controller.SetFloat(SMOOTH, Mathf.Clamp01(alpha));
        if (alpha <= 0.0f)
            return true;
        return false;
    }

    private void OnDestroy()
    {
        controller.SetFloat(SMOOTH, 0.0f);
        controller.SetTexture(DISSOLVE_TEXTURE, null);
    }
}
