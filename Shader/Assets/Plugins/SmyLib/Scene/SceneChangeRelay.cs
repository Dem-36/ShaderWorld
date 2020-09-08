using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeRelay : MonoBehaviour
{
    [SerializeField, SceneName]
    private string sceneName = "";
    [SerializeField, Header("読み込んだ際、シーンをアンロードするか")]
    private bool isUnload = true;

    /// <summary>
    /// シーンの読み込み
    /// </summary>
    public void Next()
    {
        if (isUnload == true)
        {
            GameManager.Instance.SceneChangeManager.NextToUnloadScene(sceneName, gameObject.scene.name);
        }
        else
        {
            GameManager.Instance.SceneChangeManager.Next(sceneName);
        }
    }

    /// <summary>
    /// シーンのアンロード
    /// </summary>
    public void Unload()
    {
        GameManager.Instance.SceneChangeManager.Unload(gameObject.scene.name);
    }
}
