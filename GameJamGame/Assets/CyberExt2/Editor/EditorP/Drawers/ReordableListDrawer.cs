using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Reflection;
using UnityEditorInternal;

namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(ReordableListAttribute))]

    public class ReordableListDrawer : IDirectlyDrawer
    {
       
        public void DrawDirectly(SerializedProperty property, CyberAttrribute atribute, GUIContent content, GUIStyle style, FieldInfo field)
        {
            
            
            if (property.arraySize > 0 && property.GetArrayElementAtIndex(0).propertyType == SerializedPropertyType.Generic)
                throw new CyberAttributeException(typeof(ReordableListAttribute),"It can be use only with base type array");

            const string Reorderable = "Reorderable";
            var getted = CyberEdit.Current.GetGlobalValue(field, Reorderable);
            ReorderableList reorderable;
            if(getted==null)
            {
                reorderable = new ReorderableList(CyberEdit.Current.SerializedObject, property, true, true, true, true);
                CyberEdit.Current.SetGlobalValue(field, Reorderable, reorderable);
               
                reorderable.drawElementCallback =
                    (Rect rect, int index, bool isActive, bool isFocused) =>
                    {
                        EditorGUI.PropertyField(rect, property.GetArrayElementAtIndex(index));
                    };
                reorderable.drawHeaderCallback =
                    (Rect rect) =>
                    {
                        EditorGUI.LabelField(rect, content);
                    };
            }
            else
            {
                reorderable = getted as ReorderableList;
            }   

          
            TheEditor.DrawBeforeArraySize(field, property);

            TheEditor.DrawAfterArraySize(field, property);
            reorderable.DoLayoutList();
    
            TheEditor.DrawAfterArray(field, property);

        }
    }
}
