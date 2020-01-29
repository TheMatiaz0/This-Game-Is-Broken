using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cyberevolver.EditorUnity
{ 
    public interface IPrefixDrawer:ICyberDrawer
    {
        void DrawPrefix(GUIContent content, GUIStyle style, CyberAttrribute cyberAttrribute);
    }
}
