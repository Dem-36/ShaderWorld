using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeRelay : MonoBehaviour
{
    [SerializeField, SceneName]
    private string sceneName = "";

    //public void Next()
    //{
    //    //SceneManagerがない場合は直接読み込む
    //    if (SceneChangeManager.Instance == null)
    //        SceneManager.LoadScene(sceneName);
    //    else
    //        SceneChangeManager.Instance.Next(sceneName);
    //}

}
