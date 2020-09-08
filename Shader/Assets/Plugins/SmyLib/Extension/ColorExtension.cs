using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Smy.Graphics
{
    /// <summary>
    /// 色情報の拡張メソッド
    /// </summary>
    public static class ColorExtension
    {
        public static Color R(this Color color, float r)
            => new Color(r, color.g, color.b, color.a);

        public static Color G(this Color color, float g)
            => new Color(color.r, g, color.b, color.a);

        public static Color B(this Color color, float b)
            => new Color(color.r, color.g, b, color.a);

        public static Color A(this Color color, float a)
            => new Color(color.r, color.g, color.b, a);

        public static Color Zero(this Color color)
            => new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }
}