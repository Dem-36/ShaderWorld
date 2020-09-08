using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
#endif

class SceneNameAttribute : PropertyAttribute { }

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(SceneNameAttribute))]
public class SceneNameDrawer : PropertyDrawer
{
    private List<string> sceneNames;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (sceneNames == null || EditorBuildSettings.scenes.Length!=sceneNames.Count)
            RegistrationSceneName();
        if(sceneNames.Count <=0)
            return;
        var selectIndex = sceneNames.FindIndex(n => n.Equals(property.stringValue));
        if (selectIndex == -1)
            selectIndex = sceneNames.FindIndex(n => n.Equals(sceneNames[0]));
        selectIndex = EditorGUI.Popup(position, label.text, selectIndex, sceneNames.ToArray());
        property.stringValue = sceneNames[selectIndex];
    }

    private void RegistrationSceneName()
    {
        //中身のクリア
        sceneNames = new List<string>();
        //BuildSettingsからSceneのPathを読み込む
        List<string> allSceneNames = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToList();
        //シーンの登録
        foreach (string s in allSceneNames)
        {
            int slash = s.LastIndexOf("/");
            int dot = s.LastIndexOf(".");
            sceneNames.Add(s.Substring(slash + 1, dot - slash - 1));
        }
    }
}
#endif