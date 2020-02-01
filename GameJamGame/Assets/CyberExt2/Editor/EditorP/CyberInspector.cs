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
namespace  Cyberevolver.EditorUnity
{
    [CustomEditor(typeof(UnityEngine.Object),true)]
    public class CyberInspector : Editor
    {
        private CyberEdit cyberEditor;
        private void OnEnable()
        {
            if (target == null)
                return;
            cyberEditor = new CyberEdit(serializedObject, target);
            cyberEditor.Active();
        }
        public override void OnInspectorGUI()
        {
            cyberEditor.DrawAll();
            cyberEditor.Save();
        }
    }
}
