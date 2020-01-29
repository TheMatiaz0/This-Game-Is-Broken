using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine.UIElements;

namespace Cyberevolver.EditorUnity
{
    [CustomPropertyDrawer(typeof(Direction))]
    public class DirectionDrawer:PropertyDrawer
    {

        private Vector2? nValue=null;
        public override bool CanCacheInspectorGUI(SerializedProperty property)
        {
            return false;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position,label,property);
            int n = 0;
            SerializedProperty valueProp = property.FindPropertyRelative("_Value");
            if (nValue == null)
                nValue = valueProp.vector2Value;
            Vector2? last = nValue;
            Vector2 value = (Vector2)nValue;
            position=  EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive),label);
            DrawOneField("X",position,n++,ref value.x );
            DrawOneField("Y",position,n++,ref value.y );
            if (value.x > 1)
                value.x = 0;
            if (value.y > 1)
                value.y = 0;               
            var (namePos, fieldPos) = GetNameAndValuePos(position, n++);
            GenericMenu genericMenu = new GenericMenu();
            foreach (string item in Enum.GetNames(typeof(SimpleDirection)))
                genericMenu.AddItem(new GUIContent(item), false, OnItemSelect, Enum.Parse(typeof(SimpleDirection), item));
            string dirText = null;
            var dir = new Direction(value).TryToSimpleDirection();
            if (dir != null)
                dirText = dir.ToString();
            else
                dirText = "Custom";
                
            if (EditorGUI.DropdownButton(fieldPos, new GUIContent(dirText), FocusType.Passive))
                genericMenu.ShowAsContext();
            if (nValue==last)
                nValue = value;
            valueProp.vector2Value = (Vector2)nValue;
            EditorGUI.EndProperty();
        }
        
        private void OnItemSelect(object sender)
        {
            SimpleDirection s = (SimpleDirection)sender;
            Direction  dir= s.ToDirection();
            nValue = new Vector2(dir.X, dir.Y);
        }
        private void DrawOneField(string name,Rect mainPosition,int n,ref float value)
        {
            (Rect namePos, Rect fieldPos) = GetNameAndValuePos( mainPosition, n);
            EditorGUI.LabelField(namePos, new GUIContent(name));
            value= EditorGUI.FloatField(fieldPos, value);
        }
        private (Rect namePos,Rect fieldPos) GetNameAndValuePos(Rect position,int n)
        {
            float degresse = 3.4f;
            Rect namePos = new Rect(position.x + n * (position.width / degresse + 10), position.y, 10, position.height);
            Rect fieldPos = new Rect(namePos.x+namePos.width, position.y, position.width / degresse, position.height);
            return (namePos, fieldPos);

        }
    }
}
