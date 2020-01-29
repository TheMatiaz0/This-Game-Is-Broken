using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Cyberevolver.Unity
{
    [Serializable]
    public struct SerializeVector2
    {
        private float X { get; }
        private float Y { get; }
        public Vector2 Vector2 => new Vector2(X, Y);
        public SerializeVector2(Vector2 vector)
        {
            X = vector.x;
            Y = vector.y;
        }
        public static implicit operator Vector2(SerializeVector2 serialize)
        {
            return serialize.Vector2;
        }
        public static implicit operator SerializeVector2(Vector2 vector)
            => new SerializeVector2(vector);


    }
}