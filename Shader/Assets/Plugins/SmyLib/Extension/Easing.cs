using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Smy.Math
{
    public class Easing
    {
        /// <summary>
        /// 二乗
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static float Quad(float t) => t * t;
        /// <summary>
        /// 三乗
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static float Cubic(float t) => t * t * t;
        /// <summary>
        /// 四乗
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static float Quart(float t) => t * t * t * t;
        /// <summary>
        /// 五乗
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static float Quint(float t) => t * t * t * t * t;

        #region Linear
        public static float Linear(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;
            end -= start;
            return end * ct / et + start;
        }
        #endregion

        #region Quadratic
        public static float QuadIn(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;
            end -= start;
            ct /= et;
            return end * Quad(ct) + start;
        }

        public static float QuadOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;
            end -= start;
            ct /= et;
            return -end * ct * (ct - 2.0f) + start;
        }

        public static float QuadInOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;
            end -= start;
            ct /= et / 2.0f;
            if (ct < 1.0f)
                return end / 2.0f * Quad(ct) + start;
            ct--;
            return -end / 2.0f * (ct * (ct - 2.0f) - 1.0f) + start;
        }
        #endregion

        #region Cubic
        public static float CubicIn(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;
            end -= start;
            ct /= et;
            return end * Cubic(ct) + start;
        }

        public static float CubicOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;
            end -= start;
            ct /= et;
            ct--;
            return end * (Cubic(ct) + 1.0f) + start;
        }

        public static float CubicInOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;
            end -= start;
            ct /= et / 2.0f;
            if (ct < 1.0f)
                return end / 2.0f * Cubic(ct) + start;
            ct -= 2.0f;
            return end / 2.0f * (Cubic(ct) + 2.0f) + start;
        }
        #endregion

        #region Quartic
        public static float QuartIn(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;
            end -= start;
            ct /= et;
            return end * Quart(ct) + start;
        }

        public static float QuartOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;
            end -= start;
            ct /= et;
            ct--;
            return -end * (Quart(ct) - 1.0f) + start;
        }

        public static float QuartInOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;
            end -= start;
            ct /= et / 2.0f;
            if (ct < 1.0f)
                return end / 2.0f * Quart(ct) + start;
            ct -= 2.0f;
            return -end / 2.0f * (Quart(ct) - 2.0f) + start;
        }
        #endregion

        #region Quintic
        public static float QuintIn(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;

            end -= start;
            ct /= et;
            return end * Quint(ct) + start;
        }

        public static float QuintOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;
            end -= start;
            ct /= et;
            ct--;
            return end * (Quint(ct) + 1.0f) + start;
        }

        public static float QuintInOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;

            end -= start;
            ct /= et / 2.0f;
            if (ct < 1.0f)
                return end / 2.0f * Quint(ct) + start;
            ct -= 2.0f;
            return end / 2.0f * (Quint(ct) + 2.0f) + start;
        }
        #endregion

        #region Sinusoidal
        public static float SinIn(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;

            end -= start;
            return -end * Mathf.Cos(ct / et * (Mathf.PI / 2.0f)) + end + start;
        }

        public static float SinOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;

            end -= start;
            return end * Mathf.Sin(ct / et * (Mathf.PI / 2.0f)) + start;
        }

        public static float SinInOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;

            end -= start;
            return -end / 2.0f * (Mathf.Cos(Mathf.PI * ct / et) - 1.0f) + start;
        }
        #endregion

        #region Exponential
        public static float ExpIn(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;

            end -= start;
            return end * Mathf.Pow(2.0f, 10.0f * (ct / et - 1.0f)) + start;
        }

        public static float ExpOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;

            end -= start;
            return end * (-Mathf.Pow(2.0f, -10.0f * ct / et) + 1.0f) + start;
        }

        public static float ExpInOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;

            end -= start;
            ct /= et / 2.0f;
            if (ct < 1.0f)
                return end / 2.0f * Mathf.Pow(2.0f, 10.0f * (ct - 1.0f)) + start;
            ct--;
            return end / 2.0f * (-Mathf.Pow(2.0f, -10.0f * ct) + 2.0f) + start;
        }
        #endregion

        #region Circular
        public static float CircularIn(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;

            end -= start;
            ct /= et;
            return -end * (Mathf.Sqrt(1.0f - Quad(ct)) - 1.0f) + start;
        }

        public static float CircularOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;

            end -= start;
            ct /= et;
            ct--;
            return end * Mathf.Sqrt(1.0f - Quad(ct)) + start;
        }

        public static float CircularInOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;

            end -= start;
            ct /= et / 2.0f;
            if (ct < 1.0f)
                return -end / 2.0f * (Mathf.Sqrt(1.0f - Quad(ct)) - 1.0f) + start;
            ct -= 2.0f;
            return end / 2.0f * (Mathf.Sqrt(1.0f - Quad(ct)) + 1.0f) + start;
        }
        #endregion

        #region Elastic
        public static float ElasticIn(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;
            end -= start;
            ct /= et;

            float s = 1.70158f;
            float p = et * 0.3f;
            float a = end;
            if (ct == 0.0f)
                return start;
            if (ct == 1.0f)
                return end;

            if (a < Mathf.Abs(end))
            {
                a = end;
                s = p / 4.0f;
            }
            else
            {
                s = p / (2.0f * Mathf.PI) * Mathf.Asin(end / a);
            }
            ct -= 1.0f;
            return -(a * Mathf.Pow(2.0f, 10.0f * ct) * Mathf.Sin((ct * et - 5.0f) * (2.0f * Mathf.PI) / p)) + start;
        }

        public static float ElasticOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;

            end -= start;
            ct /= et;

            float s = 1.70158f;
            float p = et * 0.3f;
            float a = end;
            if (ct == 0.0f)
                return start;
            if (ct == 1.0f)
                return start + end;

            if (a < Mathf.Abs(end))
            {
                a = end;
                s = p / 4.0f;
            }
            else
            {
                s = p / (2.0f * Mathf.PI) * Mathf.Asin(end / a);
            }
            return a * Mathf.Pow(2.0f, -10.0f * ct) * Mathf.Sin((ct * et - 5.0f) * (2.0f * Mathf.PI) / p) + end + start;
        }

        public static float ElasticInOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;

            end -= start;
            ct /= et / 2.0f;

            float s = 1.70158f;
            float p = et * (0.3f * 1.5f);
            float a = end;
            if (ct == 0.0f)
                return start;
            if (ct == 1.0f)
                return start + end;

            if (a < Mathf.Abs(end))
            {
                a = end;
                s = p / 4.0f;
            }
            else
            {
                s = p / (2.0f * Mathf.PI) * Mathf.Asin(end / a);
            }

            if (ct < 1.0f)
            {
                return -0.5f * (a * Mathf.Pow(2.0f, 10.0f * (ct - 1.0f)) * Mathf.Sin((ct * et - 5.0f) * (2.0f * Mathf.PI) / p)) + start;
            }

            ct -= 1.0f;
            return a * Mathf.Pow(2.0f, -10.0f * ct) * Mathf.Sin((ct * et - 5.0f) * (2.0f * Mathf.PI) / p) * 0.5f + end + start;
        }
        #endregion

        #region Bounce
        public static float BounceIn(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;

            end -= start;
            return end - BounceOut(et - ct, et, 0.0f, end) + start;
        }

        public static float BounceOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;

            end -= start;
            ct /= et;

            const float d1 = 2.75f;
            const float n1 = 7.5625f;

            if (ct < 1.0f / d1)
            {
                return end * (n1 * Quad(ct)) + start;
            }
            else if (ct < 2.0f / d1)
            {
                ct -= 1.5f / d1;
                return end * (n1 * Quad(ct) + 0.75f) + start;
            }
            else if (ct < 2.5f / d1)
            {
                ct -= 2.25f / d1;
                return end * (n1 * Quad(ct) + 0.9375f) + start;
            }
            else
            {
                ct -= 2.625f / d1;
                return end * (n1 * Quad(ct) + 0.984375f) + start;
            }
        }

        public static float BounceInOut(float ct, float et, float start, float end)
        {
            if (ct > et)
                return end;

            if (ct < et / 2.0f)
                return BounceIn(ct * 2.0f, et, 0.0f, end - start) * 0.5f + start;
            else
                return BounceOut(ct * 2.0f - et, et, 0.0f, end - start) * 0.5f + start + (end - start) * 0.5f;
        }
        #endregion
    }
}