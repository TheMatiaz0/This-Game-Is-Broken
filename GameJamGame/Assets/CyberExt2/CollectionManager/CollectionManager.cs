using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyberevolver
{
    /// <summary>
    /// Abstract construct intendet to fast create collection, which has limited adding and removing method and public
    /// information about elements.
    /// It also implements <see cref="IEnumerable{T}"/> and events.
    /// </summary>
    /// <typeparam name="TLimit">Type of collection element</typeparam>
    public abstract class CollectionManager<TLimit> : IEnumerable<TLimit>, ICollectionManager<TLimit>
        where TLimit : class
    {
        protected List<TLimit> List { get; set; } = new List<TLimit>();
        /// <summary>
        /// Only creator of specific manager can change this value (In constructor).
        /// If it is true, you can add null as collection element
        /// </summary>
        public bool CanAddNull { get; }
        public event EventHandler<CollectionActionArgs<TLimit>> OnAddElement = delegate { };
        public event EventHandler<CollectionActionArgs<TLimit>> OnRemoveElement =delegate{};
        /// <summary>
        /// Get all collection elements.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<TLimit> GetAll() => List;
        /// <summary>
        /// Adding element. Element must comply with the rules, which creator of specific manager created.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public T1 Add<T1>(T1 value)
            where T1 : class, TLimit
        {
            return (Add(val: value)) as T1;
        }
        /// <summary>
        /// Adding element. Element must comply with the rules, which creator of specific manager created.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public TLimit Add(TLimit val)
        {
            if (CanAddNull == false && val is null)
                throw new ArgumentNullException(nameof(val));
            if (CanAdd(val))
            {
                List.Add(val);
                OnAddElement.Invoke(this, new CollectionActionArgs<TLimit>(CManagerAction.Add, val, this));
                return val;
            }
            return null;
        }
        /// <summary>
        /// Removing element.It do not always remove. Creator of specific manager define the rules.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Remove<T1>(T1 value)
            where T1 : class, TLimit
        {
            if (CanAddNull == false && value is null)
                throw new ArgumentNullException(nameof(value));
            int index;
            if ((index = List.FindIndex((item => item == value))) != -1)
                if (CanRemove(value))
                    if (List.Remove(value))
                    {
                        OnRemoveElement(this, new CollectionActionArgs<TLimit>(CManagerAction.Add, value, this));
                        return true;
                    }

            return false;
        }
        /// <summary>
        /// Removing element by index.It do not always remove. Creator of specific manager define the rules.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Remove(int index)
        {
            if (CanRemove(List[index]))
                if (List.Remove(List[index]))
                {
                    OnRemoveElement.Invoke(this, new CollectionActionArgs<TLimit>(CManagerAction.Remove, List[index], this));
                    return true;
                }
            return false;
        }
       
        /// <param name="canAddNull">If it is true, you can add null value to collection</param>
        public CollectionManager(bool canAddNull)
        {
            CanAddNull = canAddNull;
        }
        /// <summary>
        /// Write here logic, which will be testing which element can be added
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract bool CanAdd(TLimit value);
        /// <summary>
        /// Write here logic, which will be testing which element can be removed
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract bool CanRemove(TLimit value);
        CollectionManager<TLimit> Clone()
            => base.MemberwiseClone() as CollectionManager<TLimit>;
       
        
        IReadOnlyList<TLimit> ICollectionManager<TLimit>.GetAllElement()
        {
            return List;
        }
        public IEnumerator<TLimit> GetEnumerator()
        {
            return List.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


}
