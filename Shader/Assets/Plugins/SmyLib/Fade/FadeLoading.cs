using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Smy.Math;

public class FadeLoading : MonoBehaviour
{
    [SerializeField]
    private Text loadingText = null;
    [SerializeField]
    private Image loadingImage = null;
    private Timer timer;

    private void Start()
    {
        timer = new Timer(1.5f);
        Initialize(false);
    }

    public void Initialize(bool enable)
    {
        loadingText.enabled = enable;
        loadingImage.enabled = enable;
        timer.Initialize();
    }

    public void Load()
    {
        timer.Update();
        if (timer.IsTime())
        {
            timer.Initialize();
        }
        loadingImage.rectTransform.localEulerAngles =
            new Vector3(0.0f, 0.0f, -Easing.QuartInOut(timer.currentTime, timer.limitTime, 0.0f, 360.0f));
    }
}
