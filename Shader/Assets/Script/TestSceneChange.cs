using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneChange : MonoBehaviour
{
    public void OnClick()
    {
        var a = GetComponents<SceneChangeRelay>();
        foreach(var aa in a)
        {
            aa.Next();
        }
    }
}
