using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Smy
{
    public static class TransformExtension
    {
        public static void SetX(this Transform t, float x)
            => t.position = new Vector3(x, t.position.y, t.position.z);

        public static void SetY(this Transform t, float y)
            => t.position = new Vector3(t.position.x, y, t.position.z);

        public static void SetZ(this Transform t, float z)
            => t.position = new Vector3(t.position.x, t.position.y, z);

        public static void AddX(this Transform t, float x)
            => t.SetX(t.position.x + x);

        public static void AddY(this Transform t, float y)
            => t.SetY(t.position.y + y);

        public static void AddZ(this Transform t, float z)
            => t.SetZ(t.position.z + z);

        public static Vector3 XZ(this Transform t)
            => new Vector3(t.position.x, 0.0f, t.position.z);

        public static void SetParent(this Transform t, Transform parent)
            => t.parent = parent;

    }

    public static class GameObjectExtension
    {

    }

    public static class ComponentExtension
    {

        /// <summary>
        /// コンポーネントがアタッチされているかどうかを調べる
        /// </summary>
        /// <typeparam name="T">Component</typeparam>
        /// <param name="obj">Object</param>
        /// <returns>Tがnullの場合true</returns>
        public static bool IsComponent<T>(this GameObject obj) where T : class
            => obj.GetComponent<T>() == null;

        /// <summary>
        /// コンポーネントがアタッチされているかどうかを調べる
        /// </summary>
        /// <typeparam name="T">Component</typeparam>
        /// <param name="obj">Object</param>
        /// <returns>Tがnullの場合true</returns>
        public static bool IsComponentInParent<T>(this GameObject obj) where T : class
            => obj.GetComponentInParent<T>() == null;

        /// <summary>
        /// コンポーネントがアタッチされているかどうかを調べる
        /// </summary>
        /// <typeparam name="T">Component</typeparam>
        /// <param name="obj">Object</param>
        /// <returns>Tがnullの場合true</returns>
        public static bool IsComponentInChildren<T>(this GameObject obj) where T : class
            => obj.GetComponentInChildren<T>() == null;

        /// <summary>
        /// コンポーネントの取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetComponentToNullCheck<T>(this GameObject obj) where T : class
        {
            T type = obj.GetComponent<T>();

            if (type.Equals(null))
                return ComponentErrorLog<T>(obj.name);

            return type;
        }

        /// <summary>
        /// 親のコンポーネント取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetComponentInParentToNullCheck<T>(this GameObject obj) where T : class
        {
            T type = obj.GetComponentInParent<T>();

            if (type.Equals(null))
                return ComponentErrorLog<T>(obj.name);

            return type;
        }

        /// <summary>
        /// 子供のコンポーネント取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetComponentInChildrenToNullCheck<T>(this GameObject obj) where T : class
        {
            T type = obj.GetComponentInChildren<T>();

            if (type.Equals(null))
                return ComponentErrorLog<T>(obj.name);

            return type;
        }

        /// <summary>
        /// 子オブジェクトの指定したComponentを取得する
        /// 自身は省く
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T[] GetComponentsInChildrenNoIncSelf<T>(this GameObject obj) where T : Component
            => obj.GetComponentsInChildren<T>().Where(c => obj != c.gameObject).ToArray();

        /// <summary>
        /// 親オブジェクトの指定したComponentを取得
        /// 自身は省く
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T[] GetComponentsInParentNoIncSelf<T>(this GameObject obj) where T : Component
            => obj.GetComponentsInParent<T>().Where(c => obj != c.gameObject).ToArray();


        /// <summary>
        /// エラーログの表示
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="name">オブジェクトの型</param>
        /// <returns></returns>
        private static T ComponentErrorLog<T>(string name)
        {
            DebugLogger.LogError($"Component:{typeof(T).ToString()}はGameObject:" +
                $"{name}にアタッチされていません");

            return default(T);
        }
    }
}