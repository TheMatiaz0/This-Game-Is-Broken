using System;
using UnityEngine;
namespace Cyberevolver.Unity
{
    [Serializable]
    public struct BinaryColor
    {
        public float R { get; }
        public float G { get; }
        public float B { get; }
        public float A { get; }
        public Color Color => new Color(R, G, B, A);
        public BinaryColor(Color color)
        {
            R = color.r;
            G = color.g;
            B = color.b;
            A = color.a;
        }
        public static implicit operator Color(BinaryColor color)
        {
            return color.Color;
        }
        public static implicit operator BinaryColor(Color color)
        {
            return new BinaryColor(color);
        }
    }

}
