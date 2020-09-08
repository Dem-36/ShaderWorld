using System.Diagnostics;
using UnityEngine;

public class DebugLogger
{
    [Conditional("UNITY_EDITOR")]
    public static void Log(object obj)
        => UnityEngine.Debug.Log(obj);

    [Conditional("UNITY_EDITOR")]
    public static void LogWarning(object obj)
        => UnityEngine.Debug.LogWarning(obj);

    [Conditional("UNITY_EDITOR")]
    public static void LogError(object obj)
        => UnityEngine.Debug.LogError(obj);

    [Conditional("UNITY_EDITOR")]
    public static void Break()
        => UnityEngine.Debug.Break();

    [Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end)
        => UnityEngine.Debug.DrawLine(start, end);

    [Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end, Color color)
        => UnityEngine.Debug.DrawLine(start, end, color);

    [Conditional("UNITY_EDITOR")]
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration, bool depthTest = true)
        => UnityEngine.Debug.DrawLine(start, end, color, duration);

    [Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 dir)
        => UnityEngine.Debug.DrawRay(start, dir);

    [Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 dir, Color color)
        => UnityEngine.Debug.DrawRay(start, dir, color);

    [Conditional("UNITY_EDITOR")]
    public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration, bool depthTest = true)
        => UnityEngine.Debug.DrawRay(start, dir, color, duration, depthTest);
}
