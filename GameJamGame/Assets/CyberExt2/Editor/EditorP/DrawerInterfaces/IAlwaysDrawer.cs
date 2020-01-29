using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberevolver.EditorUnity
{
    public interface IAlwaysDrawer:ICyberDrawer
    {
        void DrawBefore(CyberAttrribute cyberAttrribute);
        void DrawAfter(CyberAttrribute cyberAttrribute);
    }
}
