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

namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(AutoLoadAttribute))]
    public class AutoLoadDrawer : IEnableInspectorDrawer,IMetaDrawer
    {
        public void DrawBefore(CyberAttrribute cyberAttribute)
        {
            EditorGUILayout.BeginHorizontal();
        }
        public void DrawAfter(CyberAttrribute cyberAttribute)
        {
          
            if(GUILayout.Button("↺",GUILayout.Width(20)))
            {
                Restore(CyberEdit.Current.CurrentProp, CyberEdit.Current.CurrentField, cyberAttribute);
            }
            EditorGUILayout.EndHorizontal();
            
        }

      


        public void DrawOnEnable(CyberAttrribute cyberAttribute, SerializedProperty property, FieldInfo field)
        {
            AutoLoadAttribute attribute = cyberAttribute as AutoLoadAttribute;
            if (CyberEdit.Current.Target is SceneAsset)
                return;
            if ((CyberEdit.Current.Target is Component
                &&(field.FieldType.IsInterface||field.FieldType.IsSubclassOf(typeof(UnityEngine.Component)))))
            {
                if (attribute.AngryPutIfNull && property.objectReferenceValue == null)
                {
                    Restore( property,field,cyberAttribute);
                }
            }
            else
                throw new CyberAttributeException(typeof(AutoLoadAttribute), "It can be attached only to component and can only needs a component");
        }
        private void Restore(SerializedProperty property, FieldInfo field,CyberAttrribute cyberAttribute)
        {
            AutoLoadAttribute attribute = cyberAttribute as AutoLoadAttribute;
            var component = (CyberEdit.Current.Target as Component);
            if (attribute.WithChildren == false)
                property.objectReferenceValue = component.gameObject.GetComponent(field.FieldType);
            else
                property.objectReferenceValue = component.gameObject.GetComponentInChildren(field.FieldType);
        }
    }
}
