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
    public interface  IConditionsDrawer : ICyberDrawer
    {
       bool CanDraw( CyberAttrribute cyberAttrribute);
       
    }
}

