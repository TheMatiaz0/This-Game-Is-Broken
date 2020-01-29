using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberevolver
{

    /// <summary>
    /// <see cref="CollectionManager{TLimit}"/> using this in events.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CollectionActionArgs<T> : EventArgs
        where T : class
    {
        /// <summary>
        /// Collection manager, which call event.
        /// </summary>
        public CollectionManager<T> CollectionManager { get; }
        /// <summary>
        /// Element, which was change(add, remove or clear).
        /// </summary>
        public T Element { get; }
        /// <summary>
        /// Action type
        /// </summary>
        public CManagerAction ActionType { get; }
        public CollectionActionArgs(CManagerAction action, T element, CollectionManager<T> collectionManager)
        {
            Element = element;
            CollectionManager = collectionManager;
            ActionType = action;
        }
    }


}
