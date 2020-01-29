using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEditor;
using UnityEngine;

namespace Cyberevolver.EditorUnity
{ 
    public abstract class CyberevolverPropertyDrawer:PropertyDrawer
    {
        protected float Height { get; private set; }
        protected float OneLine { get; private set; }
        protected Rect StartPos { get; private set; }
        protected Rect AddLine(Rect pos)
        {
            Height += OneLine;
            pos.y += OneLine;
            return pos;
        }
        public sealed override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + Height;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            OneLine = base.GetPropertyHeight(property, label);
            Height = 0;
        }
    }
}
