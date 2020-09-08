using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    //読み込まれたシーンの名前
    private List<string> loadSceneNameList = new List<string>();
    //読み込んではいけないシーン名
    private string dontLoadSceneName = "";

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize(string dontLoadSceneName)
    {
        //一定時間は必ずロードする
        loadSceneNameList = new List<string>();
        this.dontLoadSceneName = dontLoadSceneName;
    }

    /// <summary>
    /// シーンの読み込み
    /// </summary>
    /// <param name="sceneName"></param>
    public void Next(string sceneName)
    {
        if(dontLoadSceneName == sceneName)
        {
            DebugLogger.LogError($"{sceneName}は読み込んではいけません");
            return;
        }
        StartCoroutine(LoadScene(sceneName));
    }

    public void Unload(string sceneName)
    {
        if(dontLoadSceneName == sceneName)
        {
            DebugLogger.LogError($"{sceneName}は解放できません");
            return;
        }
        StartCoroutine(UnloadScene(sceneName));
    }

    /// <summary>
    /// シーンを読み込み、
    /// 元のシーンをアンロードする
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="unloadSceneName"></param>
    public void NextToUnloadScene(string sceneName,string unloadSceneName)
    {
        if(dontLoadSceneName == sceneName ||
            dontLoadSceneName == unloadSceneName)
        {
            DebugLogger.LogError($"{dontLoadSceneName}は読み込み、解放できません");
            return;
        }
        StartCoroutine(LoadScene(sceneName, true, unloadSceneName));
    }

    /// <summary>
    /// シーンの読み込み(マルチシーン)
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator LoadScene(string sceneName,bool isUnload = false,string unloadSceneName = "")
    {
        //すでにhierarchyに存在しているなら
        if (loadSceneNameList.Contains(sceneName) == true)
        {
            DebugLogger.LogWarning($"{sceneName}はすでに読み込まれています。");
            yield break;
        }
        loadSceneNameList.Add(sceneName);
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        yield return async;

        if(isUnload == true)
        {
            Unload(unloadSceneName);
        }
    }

    /// <summary>
    /// 読み込まれているシーンの開放
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator UnloadScene(string sceneName)
    {
        //指定したシーンを読み込んでいないなら
        if(loadSceneNameList.Contains(sceneName) == false)
        {
            DebugLogger.LogWarning($"{sceneName}は読み込まれていません");
            yield break;
        }

        loadSceneNameList.Remove(sceneName);
        AsyncOperation async = SceneManager.UnloadSceneAsync(sceneName);

        yield return async;

        //リソースの開放
        yield return Resources.UnloadUnusedAssets();
    }
}
