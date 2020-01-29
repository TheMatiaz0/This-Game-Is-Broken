using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Cyberevolver
{
    [Serializable]

    public abstract class SaveObject<T>
    where T : SaveObject<T>, new()
    {
        protected virtual string FileName { get; }
        public SaveObject(string fileName)
        {
            FileName = fileName;
        }
        public SaveObject()
        {
            FileName = typeof(T).ToString();
        }
        public void Save()
        {
            SaveControl.SaveObject<T>(this as T, FileName);
        }
        public static T Load()
        {
            return SaveControl.LoadObject<T>(new T().FileName);
        }
        public static T TryLoad()
        {
            try
            {
                return Load();
            }
            catch
            {
                return null;
            }
        }
        public static T TryLoadElseCreateNewFile()
        {
            T moment = TryLoad();
            if (moment == null)
            {
                moment = new T();
                moment.Save();

            }
            return moment;
        }


    }

}

