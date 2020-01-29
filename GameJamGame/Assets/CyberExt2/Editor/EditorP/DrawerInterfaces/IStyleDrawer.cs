using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace  Cyberevolver.EditorUnity
{ 
   public interface IStyleDrawer:ICyberDrawer
    {
        void ChangeGuiStyle(CyberAttrribute attrribute,ref GUIStyle style,ref string customName);
    }
}
