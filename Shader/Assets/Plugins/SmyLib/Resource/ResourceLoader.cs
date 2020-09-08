using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ResourceLoader
{
    private static Dictionary<string, AssetBundle> bundles;

    public T[] LoadResources<T>(string filePath) where T : class
    {
        if (bundles == null)
            bundles = new Dictionary<string, AssetBundle>();

        if (!bundles.ContainsKey(filePath))
            bundles.Add(filePath, AssetBundle.LoadFromFile(filePath));


        string[] assetNames = bundles[filePath].GetAllAssetNames();
        T[] assets = new T[assetNames.Length];
        for (int i = 0; i < assetNames.Length; i++)
        {
            assets[i] = bundles[filePath].LoadAsset(assetNames[i]) as T;
            Debug.Log(assets[i]);
        }

        return assets;
    }
}
