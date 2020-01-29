using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberevolver.EditorUnity
{
    public interface IClassDrawer:ICyberDrawer
    {
        void Draw(CyberAttrribute attribute);
    }
}
