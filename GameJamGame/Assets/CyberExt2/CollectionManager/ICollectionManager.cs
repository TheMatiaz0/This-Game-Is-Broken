using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberevolver
{
    interface ICollectionManager<out T>
    {
   
        IReadOnlyList<T> GetAllElement();
    }
}
