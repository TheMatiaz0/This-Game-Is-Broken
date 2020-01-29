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
namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(SeparatorAttribute))]
    public class SeparatorDrawer : IMetaDrawer
    {

        public void DrawBefore(CyberAttrribute cyberAttribute)
        {
            SeparatorAttribute attribute = cyberAttribute as SeparatorAttribute;
            EditorGUILayout.BeginHorizontal();

            var content = new GUIContent(attribute.Label);
            float width = EditorStyles.label.CalcSize(content).x;
            if (string.IsNullOrEmpty(attribute.Label) == false)
            {
                DrawMinBox();
                EditorGUILayout.LabelField(attribute.Label, attribute.GUIStyle, GUILayout.Width(width));
                DrawMinBox();
                
            }
            else
                DrawMinBox();
            EditorGUILayout.EndHorizontal();

            void DrawMinBox()
            {
                
                EditorGUILayout.BeginVertical();              
                GUILayout.Box(GUIContent.none,"GroupBox",GUILayout.Height(1),GUILayout.MaxWidth(int.MaxValue));
                EditorGUILayout.EndVertical();
                
            }
        }
        public void DrawAfter(CyberAttrribute atr)
        {

        }
    }
}
