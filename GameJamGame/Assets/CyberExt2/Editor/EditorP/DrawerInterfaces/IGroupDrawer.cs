using Cyberevolver.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cyberevolver.EditorUnity
{ 
   public interface IGroupDrawer:ICyberDrawer
    {
        void DrawGroup(IGrouping<string, MemberInfo> groups );
    }
}
