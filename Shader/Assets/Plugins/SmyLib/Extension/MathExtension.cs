using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//計算系をまとめる
namespace Smy.Math
{
    public class MathExtension
    {
        /// <summary>
        /// 3次元のベジェ曲線
        /// </summary>
        /// <param name="ct">現在時間</param>
        /// <param name="et">終了時間</param>
        /// <param name="points">辿る位置</param>
        /// <returns></returns>
        public static Vector3 BezierCurve(float ct, float et, params Vector3[] points)
        {
            //時間になったら一番最後の位置を返す
            if (ct > et)
                return points[points.Length - 1];
            //数値を0 ～ 1に納める
            float t = ct / et;
            float oneMinusT = 1.0f - t;
            Vector3 point = Vector3.zero;
            //最初と最後でかける数値を変更するため
            int d = 0;
            //ベジェ曲線の計算
            for (int i = 0; i < points.Length; i++)
            {
                d = (i == 0 || i == points.Length - 1) ? 0 : 1;
                point += Mathf.Pow(3.0f, d) *
                    Mathf.Pow(oneMinusT, points.Length - i - 1) *
                    Mathf.Pow(t, i) *
                    points[i];
            }
            return point;
        }
    }

    public static class VectorExtension
    {

        /// <summary>
        /// 距離計算
        /// </summary>
        /// <param name="pos1">自身の位置</param>
        /// <param name="pos2">相手の位置</param>
        /// <returns></returns>
        public static float Distance(this Vector3 pos1, Vector3 pos2)
            => (pos1 - pos2).sqrMagnitude;

        /// <summary>
        /// Vector2をVector3に変換
        /// 計算時に使用
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Vector3 Vec2to3(this Vector2 vec)
            => new Vector3(vec.x, vec.y,0.0f);

        /// <summary>
        /// Vector3をVector2に変換
        /// 計算時に使用
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Vector2 Vec3to2(this Vector3 vec)
            => new Vector2(vec.x, vec.y);

        public static Vector2 Clamp(this Vector2 vec, float min, float max)
            => new Vector2(Mathf.Clamp(vec.x, min, max), Mathf.Clamp(vec.y, min, max));
    }
}
