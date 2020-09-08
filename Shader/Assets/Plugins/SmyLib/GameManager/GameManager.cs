using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField, SceneName]
    private string dontLoadSceneName = "";
    [SerializeField, SceneName]
    private string initLoadSceneName = "";
    public SceneChangeManager SceneChangeManager { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if (this == null)
            return;
        CloneSceneManager();
        SceneChangeManager.Initialize(dontLoadSceneName);
        SceneChangeManager.Next(initLoadSceneName);
    }

    /// <summary>
    /// シーンマネージャーの生成
    /// </summary>
    private void CloneSceneManager()
    {
        //すでに存在するなら
        if (SceneChangeManager != null)
        {
            return;
        }
        SceneChangeManager = Instantiate(Resources.Load<SceneChangeManager>("Manager/SceneManager"));
    }
}
