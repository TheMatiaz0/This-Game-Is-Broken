using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Cyberevolver.Unity
{
    [Serializable]
    public class BinarySprite
    {
        private byte[] ByteArray { get; }
        public Sprite Sprite => SpriteLoad.LoadSprite(ByteArray);
        public BinarySprite(Sprite sprite)
        {
            if (sprite == null)
                ByteArray = null;
            else
                ByteArray = sprite.texture.EncodeToPNG();
        }
        public static implicit operator Sprite(BinarySprite serialize)
        {
            return serialize.Sprite;
        }
        public static implicit operator BinarySprite(Sprite sprite)
            => new BinarySprite(sprite);
    }

}