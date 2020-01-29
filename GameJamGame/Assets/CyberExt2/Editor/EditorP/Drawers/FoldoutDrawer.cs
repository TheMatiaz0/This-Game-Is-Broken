using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Reflection;
using Cyberevolver.Unity;

namespace Cyberevolver.EditorUnity
{
    [Drawer(typeof(FoldoutAttribute))]
    public class FoldoutDrawer : IGroupDrawer
    {
        public void DrawGroup(IGrouping<string, MemberInfo> group )
        {
            bool clearZone = CyberEdit.Current.DeepWay.Count == 0;
            bool res;
            bool before = CyberEdit.Current.GetExpand(group.Key);
            if (clearZone)
            {
                res= EditorGUILayout.BeginFoldoutHeaderGroup(before, group.Key);
              
            }
            else
            {
              res=  EditorGUILayout.Foldout(before, group.Key);
            }
            CyberEdit.Current.SetExpand(group.Key,res
              );


            if (CyberEdit.Current.GetExpand(group.Key))
            {

                CustomBackgrounGroupAttribute atr=(CustomBackgrounGroupAttribute)TheEditor.DrawBeforeGroup<CustomBackgrounGroupDrawer>(group.Key);
            
                EditorGUILayout.BeginVertical();
                EditorGUI.indentLevel++;
                foreach (var element in group)
                {
                   CyberEdit.Current.DrawMember(element);
                }
                EditorGUI.indentLevel--;
                TheEditor.DrawAfteGroup<CustomBackgrounGroupDrawer>(group.Key);
                EditorGUILayout.EndVertical();
            }
            if (clearZone)
                EditorGUILayout.EndFoldoutHeaderGroup();

        }
    }
}
