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
    [Drawer(typeof(ToolbarAttribute))]
    public class ToolbarDrawer : IAlwaysDrawer
    {

        public void DrawBefore(CyberAttrribute atr)
        {
            ToolbarAttribute attribute = atr as ToolbarAttribute;
            CyberEdit.Current.SetToolbarSelect
                (attribute.ToolbarId,
                GUILayout.Toolbar(CyberEdit.Current.GetToolbarSelect(attribute.ToolbarId),
                attribute.Names));
            CyberEdit.Current.SetToolbarElementCollection(attribute.ToolbarId,attribute.Names);

        }
        public void DrawAfter(CyberAttrribute atr)
        {

        }
    }
}
