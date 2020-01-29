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
using UObj = UnityEngine.Object;
namespace Cyberevolver.EditorUnity
{
    public interface IMetaDrawer:ICyberDrawer
    {
        void DrawBefore(CyberAttrribute cyberAttributer);
        void DrawAfter(CyberAttrribute cyberAttributer);
    }
}

