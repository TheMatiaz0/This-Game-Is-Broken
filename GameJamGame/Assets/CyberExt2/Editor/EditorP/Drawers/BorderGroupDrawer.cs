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
    [Drawer(typeof(BorderGroupAttribute))]
    public class BorderGroupDrawer : IGroupDrawer
    {
        public void DrawGroup(IGrouping<string, MemberInfo> groups)
        {
            CyberEdit.Current.DrawBasicGroup(groups, BackgroundMode.GroupBox);
        }
    }
}
