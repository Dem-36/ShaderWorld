using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

/// <summary>
/// シェーダー管理用スクリプト
/// 使用するマテリアルがあるオブジェクトの場所につける
/// </summary>
public class ShaderController : MonoBehaviour
{
    private Material shaderMaterial = null;
    private Dictionary<string, int> shaderPropertyDic;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize(Renderer renderer)
    {
        if (renderer == null)
            DebugLogger.LogError("Renderer is null");
        shaderMaterial = renderer.material;
        GetShaderProperties();
    }

    public void Initialize(Image image)
    {
        if (image == null)
            DebugLogger.LogError("Image is null");
        shaderMaterial = image.material;
        GetShaderProperties();
    }

    private void GetShaderProperties()
    {
        shaderPropertyDic = new Dictionary<string, int>();
        //Shaderのプロパティの数を取得し、名前とIDを登録する
        for (int i = 0; i < shaderMaterial.shader.GetPropertyCount(); i++)
        {
            string name = shaderMaterial.shader.GetPropertyName(i);
            int id = shaderMaterial.shader.GetPropertyNameId(i);
            shaderPropertyDic.Add(name, id);
        }
    }

    /// <summary>
    /// シェーダーのプロパティ名が存在するかどうか
    /// 存在するならそのIDを渡し、ないなら-1を返す
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    private bool CheckPropertyName(string propertyName, out int id)
    {
        //すでに登録してあるかどうか
        if (shaderPropertyDic.ContainsKey(propertyName))
        {
            id = shaderPropertyDic[propertyName];
            return true;
        }

        DebugLogger.LogError("そのプロパティ名は存在しません。プロパティ名:" + propertyName);
        id = -1;
        return false;
    }

    #region Setter

    /// <summary>
    /// ShaderにFloat型の値を渡す
    /// </summary>
    /// <param name="propertyName">プロパティ名</param>
    /// <param name="value">渡す値</param>
    public void SetFloat(string propertyName, float value)
    {
        int id;
        if (!CheckPropertyName(propertyName, out id))
            return;
        shaderMaterial.SetFloat(id, value);
    }

    /// <summary>
    /// ShaderにInt型の値を渡す
    /// </summary>
    /// <param name="propertyName">プロパティ名</param>
    /// <param name="value">渡す値</param>
    public void SetInt(string propertyName, int value)
    {
        int id;
        if (!CheckPropertyName(propertyName, out id))
            return;
        shaderMaterial.SetInt(id, value);
    }

    /// <summary>
    /// ShaderにColor型の値を渡す
    /// </summary>
    /// <param name="propertyName">プロパティ名</param>
    /// <param name="value">渡す値</param>
    public void SetColor(string propertyName, Color value)
    {
        int id;
        if (!CheckPropertyName(propertyName, out id))
            return;
        shaderMaterial.SetColor(id, value);
    }

    /// <summary>
    /// ShaderにMatrix4×4型の値を渡す
    /// </summary>
    /// <param name="propertyName">プロパティ名</param>
    /// <param name="value">渡す値</param>
    public void SetMatrix(string propertyName, Matrix4x4 value)
    {
        int id;
        if (!CheckPropertyName(propertyName, out id))
            return;
        shaderMaterial.SetMatrix(id, value);
    }

    /// <summary>
    /// ShaderにTexture型の値を渡す
    /// </summary>
    /// <param name="propertyName">プロパティ名</param>
    /// <param name="value">渡す値</param>
    public void SetTexture(string propertyName, Texture2D value)
    {
        int id;
        if (!CheckPropertyName(propertyName, out id))
            return;
        shaderMaterial.SetTexture(id, value);
    }

    /// <summary>
    /// ShaderにTextureのoffsetを指定する
    /// </summary>
    /// <param name="propertyName">プロパティ名</param>
    /// <param name="value">渡す値</param>
    public void SetTextureOffset(string propertyName, Vector2 value)
    {
        int id;
        if (!CheckPropertyName(propertyName, out id))
            return;
        shaderMaterial.SetTextureOffset(id, value);
    }

    /// <summary>
    /// ShaderにTextureのScaleを指定する
    /// </summary>
    /// <param name="propertyName">プロパティ名</param>
    /// <param name="value">渡す値</param>
    public void SetTextureScale(string propertyName, Vector2 value)
    {
        int id;
        if (!CheckPropertyName(propertyName, out id))
            return;
        shaderMaterial.SetTextureScale(id, value);
    }

    /// <summary>
    /// ShaderにVector4型の値を渡す
    /// </summary>
    /// <param name="propertyName">プロパティ名</param>
    /// <param name="value">渡す値</param>
    public void SetVector(string propertyName, Vector4 value)
    {
        int id;
        if (!CheckPropertyName(propertyName, out id))
            return;
        shaderMaterial.SetVector(id, value);
    }
    #endregion

    #region Getter

    /// <summary>
    /// プロパティ名を指定し、Float型の値を取得
    /// </summary>
    /// <param name="propertyName">プロパティ名</param>
    /// <returns></returns>
    public float GetFloat(string propertyName)
    {
        int id;
        if (!CheckPropertyName(propertyName, out id))
            return 0.0f;
        return shaderMaterial.GetFloat(id);
    }

    /// <summary>
    /// プロパティ名を指定し、int型の値を取得
    /// </summary>
    /// <param name="propertyName">プロパティ名</param>
    /// <returns></returns>
    public int GetInt(string propertyName)
    {
        int id;
        if (!CheckPropertyName(propertyName, out id))
            return 0;
        return shaderMaterial.GetInt(id);
    }

    /// <summary>
    /// プロパティ名を指定し、Color型の値を取得
    /// </summary>
    /// <param name="propertyName">プロパティ名</param>
    /// <returns></returns>
    public Color GetColor(string propertyName)
    {
        int id;
        if (!CheckPropertyName(propertyName, out id))
            return Color.white;
        return shaderMaterial.GetColor(id);
    }

    /// <summary>
    /// プロパティ名を指定し、Matrix4×4型の値を取得
    /// </summary>
    /// <param name="propertyName">プロパティ名</param>
    /// <returns></returns>
    public Matrix4x4 GetMatrix(string propertyName)
    {
        int id;
        if (!CheckPropertyName(propertyName, out id))
            return Matrix4x4.identity;
        return shaderMaterial.GetMatrix(id);
    }

    /// <summary>
    /// プロパティ名を指定し、Texture型の値を取得
    /// </summary>
    /// <param name="propertyName">プロパティ名</param>
    /// <returns></returns>
    public Texture GetTexture(string propertyName)
    {
        int id;
        if (!CheckPropertyName(propertyName, out id))
            return null;
        return shaderMaterial.GetTexture(id);
    }

    /// <summary>
    /// プロパティ名を指定し、Textureのoffset値を取得する
    /// </summary>
    /// <param name="propertyName">プロパティ名</param>
    /// <returns></returns>
    public Vector2 GetTextureOffset(string propertyName)
    {
        int id;
        if (!CheckPropertyName(propertyName, out id))
            return Vector2.zero;
        return shaderMaterial.GetTextureOffset(id);
    }

    /// <summary>
    /// プロパティ名を指定し、TextureのScale値を取得
    /// </summary>
    /// <param name="propertyName">プロパティ名</param>
    /// <returns></returns>
    public Vector2 GetTextureScale(string propertyName)
    {
        int id;
        if (!CheckPropertyName(propertyName, out id))
            return Vector2.zero;
        return shaderMaterial.GetTextureScale(id);
    }

    /// <summary>
    /// プロパティ名を指定し、Vector4型の値を取得
    /// </summary>
    /// <param name="propertyName">プロパティ名</param>
    /// <returns></returns>
    public Vector4 GetVector(string propertyName)
    {
        int id;
        if (!CheckPropertyName(propertyName, out id))
            return Vector4.zero;
        return shaderMaterial.GetVector(id);
    }
    #endregion

    /// <summary>
    /// プロパティ名一覧をログ表示
    /// </summary>
    public void LogPropertyName()
    {
        foreach (string key in shaderPropertyDic.Keys)
            Debug.Log(key);
    }
}
