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
        GameManager.Instance.SceneChangeManager.Next(sceneName);
        if(isUnload == true)
        {
            DebugLogger.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            GameManager.Instance.SceneChangeManager.Unload(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}
