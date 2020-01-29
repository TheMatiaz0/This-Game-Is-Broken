using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cyberevolver.Unity
{
    public static class SpecialConvert
    {
        public static object Convert(Type type,UnityEngine.Object reference, string value)
        {
            if (type.IsSubclassOf(typeof(UnityEngine.Object)) || type == typeof(UnityEngine.Object))
                return reference;
            else if (type == typeof(AColor))
                return ColorExtension.Parse(value);
            else if (type == typeof(Vector2))
                return (object)Vector2Extension.Parse(value);
            else if (type == typeof(Vector2Int))
            {
                Vector2 vect = Vector2Extension.Parse(value);
                return new Vector2Int((int)vect.x, (int)vect.y);
            }
            else if (type.IsSubclassOf(typeof(BackgroundMode)))
                return BackgroundMode.Parse(type, value);
            else
                return System.Convert.ChangeType(value, type);
        }
        public static TArg Convert<TArg>(UnityEngine.Object reference, string value)
        {
            return (TArg)Convert(typeof(TArg), reference, value);
        }
    }
}
