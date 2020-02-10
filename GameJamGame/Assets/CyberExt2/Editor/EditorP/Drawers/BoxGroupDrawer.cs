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
    [Drawer(typeof(BoxGroupAttribute))]
    public class BoxGroupDrawer : IGroupDrawer
    {

       
        public void DrawGroup(IGrouping<string, MemberInfo> groups)
        {

            BackgroundMode mode = BackgroundMode.Box;
            if (TheEditor.DrawBeforeGroup<CustomBackgrounGroupDrawer>(groups.Key) != null)
                mode = BackgroundMode.None;
            CyberEdit.Current.DrawBasicGroup(groups, mode);
            TheEditor.DrawAfteGroup<CustomBackgrounGroupDrawer>(groups.Key);
        }
    }
}
