using System;
using UnityEngine;
namespace Cyberevolver.Unity
{
    [Serializable]
    public struct BinaryColor32
    {
        public byte R { get; }
        public byte G { get; }
        public byte B { get; }
        public byte A { get; }
        public Color32 Color => new Color32(R, G, B, A);
        public BinaryColor32(Color32 color)
        {
            R = color.r;
            G = color.g;
            B = color.b;
            A = color.a;
        }
        public static implicit operator Color32(BinaryColor32 color)
        {
            return color.Color;
        }
        public static implicit operator BinaryColor32(Color32 color)
        {
            return new BinaryColor32(color);
        }
    }
}

