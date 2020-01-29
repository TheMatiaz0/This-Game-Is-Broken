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
    [Drawer(typeof(RequiresAnyAttribute))]
    public class RequiresAnyDrawer : IMetaDrawer
    {

        
        public void DrawBefore(CyberAttrribute atr)
        {

            if (CyberEdit.Current.CurrentProp == null)
                return;
            if (CyberEdit.Current.CurrentProp.objectReferenceValue == null)
            {
                TheEditor.HelpBox("This value is requier", MessageType.Warning, new GUIStyle() { fixedHeight = 30 });
                if(Application.isPlaying)
                {
                    throw new CyberAttributeException(typeof(RequiresAnyAttribute), $"Assign {CyberEdit.Current.CurrentProp.name} before you will start play");
                }
            }
                
        }
        public void DrawAfter(CyberAttrribute atr)
        {
           
        }
    }
}
