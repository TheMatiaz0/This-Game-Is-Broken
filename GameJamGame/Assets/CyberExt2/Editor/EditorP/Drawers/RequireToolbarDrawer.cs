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
    [Drawer(typeof(RequireToolbarAttribute))]
    public class RequireToolbarDrawer : IConditionsDrawer
    {
        public  bool CanDraw(CyberAttrribute cyberAttrribute)
        {
            RequireToolbarAttribute attribute = cyberAttrribute as RequireToolbarAttribute;
            int select = CyberEdit.Current.GetToolbarSelect(attribute.ToolbarId);
            if (attribute.HasOnlyStringValue == false)
                return select == attribute.Index;
            else
                return select == CyberEdit.Current.ToolbarElements[attribute.ToolbarId].GetIndex(attribute.IndexAsName);

        }
    }
}
