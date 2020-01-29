using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cyberevolver.EditorUnity
{
    public interface IMethodDrawer:ICyberDrawer
    {
        void DrawMethod(MethodInfo method,CyberAttrribute cyberAttrribute);
    }
}
