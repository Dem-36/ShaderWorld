using System.Collections;
using System.Collections.Generic;
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

    /// <summary>
    /// シーンの読み込み(マルチシーン)
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator LoadScene(string sceneName)
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
    }

    ///// <summary>
    ///// シーンの読み込み
    ///// </summary>
    ///// <param name="sceneName">シーン名</param>
    //public void Next(string sceneName)
    //{
    //    //連続読み込み防止
    //    if (sceneChangeCoroutine != null)
    //        return;
    //    sceneChangeCoroutine = SceneChange(sceneName);
    //    StartCoroutine(sceneChangeCoroutine);
    //}

    ///// <summary>
    ///// シーンの読み込み
    ///// </summary>
    ///// <param name="sceneName"></param>
    ///// <returns></returns>
    //private IEnumerator SceneChange(string sceneName)
    //{
    //    yield return new WaitWhile(() => !FadeManager.Instance.FadeIn());
    //    async = SceneManager.LoadSceneAsync(sceneName);
    //    //シーン読み込みを自動化させない
    //    async.allowSceneActivation = false;
    //    FadeManager.Instance.fadeLoading.Initialize(true);
    //    while (!async.isDone)
    //    {
    //        FadeManager.Instance.fadeLoading.Load();
    //        timer.Update();
    //        if (timer.IsTime() && !async.allowSceneActivation)
    //        {
    //            async.allowSceneActivation = true;
    //            timer.Initialize();
    //        }
    //        yield return new WaitForSeconds(Time.deltaTime);
    //    }
    //    FadeManager.Instance.fadeLoading.Initialize(false);
    //    yield return new WaitWhile(() => !FadeManager.Instance.FadeOut());
    //    //Fade out後にシーン遷移可能にする
    //    sceneChangeCoroutine = null;
    //}
}
