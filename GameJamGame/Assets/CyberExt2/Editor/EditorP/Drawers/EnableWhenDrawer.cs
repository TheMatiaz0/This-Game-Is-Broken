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
    [Drawer(typeof(EnableWhenAttribute))]
    public class EnableWhenDrawer : IMetaDrawer
    {

     
        public void DrawBefore(CyberAttrribute atr)
        {
          
            var fatr = (atr as EnableWhenAttribute);
            GUI.enabled= TheEditor.CheckEquals(CyberEdit.Current.SerializedObject.FindProperty(fatr.Prop), fatr.Value, fatr.Equaler);

        }
        public void DrawAfter(CyberAttrribute atr)
        {
            
        }
    }
}
