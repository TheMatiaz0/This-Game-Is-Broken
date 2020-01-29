using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace Cyberevolver.EditorUnity
{
    public interface IArrayModiferDrawer:ICyberDrawer
    {
        void DrawAfterSize(SerializedProperty prop, CyberAttrribute cyberAttrribute);
        void DrawBeforeSize(SerializedProperty prop, CyberAttrribute cyberAttrribute);
        void DrawAfterAll(SerializedProperty prop, CyberAttrribute cyberAttrribute);
    }
}
