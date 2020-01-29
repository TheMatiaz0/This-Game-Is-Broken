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

    [Drawer(typeof(MinSizeAttribute))]
    public class MinSizeDrawer : IMetaDrawer,IArrayModiferDrawer
    {

        public void DrawBefore(CyberAttrribute atr)
        {


        }
        public void DrawAfter(CyberAttrribute cyberAttribute)
        {
            MinSizeAttribute atribute = cyberAttribute as MinSizeAttribute;
            if (CyberEdit.Current.CurrentProp == null)
                return;

            if (CyberEdit.Current.CurrentProp.arraySize < atribute.Min)
                CyberEdit.Current.CurrentProp.arraySize = (int)atribute.Min;

        }

        public void DrawAfterSize(SerializedProperty prop, CyberAttrribute cyberAttrribute)
        {
           
        }

        public void DrawBeforeSize(SerializedProperty prop, CyberAttrribute cyberAttrribute)
        {
            TheEditor.ShortLabelField(new GUIContent($"Min:{(cyberAttrribute as MinSizeAttribute).Min}"), new GUIStyle { fontStyle = FontStyle.Italic });
        }

        public void DrawAfterAll(SerializedProperty prop, CyberAttrribute cyberAttrribute)
        {
            
        }
    }
}
