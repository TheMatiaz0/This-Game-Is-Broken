using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
namespace Cyberevolver.Unity
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DictioniarySettingsAttribute : Attribute
    {
        public DictioniarySettingsAttribute(bool tryDoReordable, bool expandable)
        {
            TryDoReordable = tryDoReordable;
            Exandable = expandable;
        }

        public bool TryDoReordable { get; }
        public bool Exandable { get; }
       

    }
}
