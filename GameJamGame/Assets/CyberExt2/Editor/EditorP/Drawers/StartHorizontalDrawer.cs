using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using UnityEditor;
using System.Collections;
namespace  Cyberevolver.EditorUnity
{
    [Drawer(typeof(StartHorizontalAttribute))]
    public class StartHorizontalDrawer : IAlwaysDrawer,IEnderDrawer
    {
        public void DrawAfter(CyberAttrribute cyberAttribute)
        {
            
        }

        public void DrawBefore(CyberAttrribute cyberAttribute)
        {
            bool before;
            before = GUI.enabled;
            GUI.enabled = true;
            var attribute = cyberAttribute as StartHorizontalAttribute;
            EditorGUILayout.BeginHorizontal();
            
         
            if (attribute.Name != null)
                EditorGUILayout.PrefixLabel(new GUIContent((attribute.Name)),new GUIStyle(),new GUIStyle() { fixedWidth=20});
            if (attribute.RightPush != 0)
                GUILayout.Label("", GUILayout.Width(attribute.RightPush));
            TheEditor.BeginHorizontal(attribute.BackgroundMode);
            CyberEdit.Current.PushHorizontalStack();
            GUI.enabled = before;
        }

        public void DrawEnd(CyberAttrribute atr)
        {
          
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndHorizontal();

            CyberEdit.Current.PopHorizontalStack();
        }
    }
}
