using OpenTK;
using System;

namespace MaximovInk.FlatinyEngine.Core
{
    public static class Mathf
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static Vector2 Clamp(this Vector2 vec, float min, float max)
        {
            return new Vector2(Clamp(vec.X, min, max), Clamp(vec.Y, min, max));
        }

        public static Vector3 Clamp(this Vector3 vec, float min, float max)
        {
            return new Vector3(Clamp(vec.X, min, max), Clamp(vec.Y, min, max), Clamp(vec.Z,min,max));
        }

        public static Vector4 Clamp(this Vector4 vec, float min, float max)
        {
            return new Vector4(Clamp(vec.X, min, max), Clamp(vec.Y, min, max), Clamp(vec.Z,min,max),Clamp(vec.W,min,max));
        }
    }
}
