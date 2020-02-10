using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
public class DevHelper : MonoBehaviourPlus
{
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    private bool showInfo = true;

    protected virtual void OnGUI()
    {
     




        GUILayout.Label("DevelopmentBulid", new GUIStyle("label") { fontSize = 25 });
        GUIStyle btStyle = new GUIStyle("button");
        btStyle.fontSize = 35;
        GUILayout.BeginVertical("Box");
        if (showInfo)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 40;
            style.normal.textColor = Color.white;
            GUILayout.Label($"SpawnRate:{Generator.Instance.GetFinalChanceForAnyActiveItems()}\n" +
                $"WaweSpeed:{System.Math.Round( Wave.Instance.GetSpeedForTime(PlayerController.Instance.SceneTime),3).ToString("0.000")}", style);
            if (GUILayout.Button("Kill wave",btStyle))
                Destroy(Wave.Instance.gameObject);
            if (GUILayout.Button("Explose", btStyle))
                ExplodeManager.Instance.Explode(PlayerController.Instance.transform.position);
            if (GUILayout.Button("Kill",btStyle))
                PlayerController.Instance.Kill();
            if (GUILayout.Button("RemoveAllGlith", btStyle))
                PlayerController.Instance.ClearAllGlith();


        }
       
        if (GUILayout.Button(showInfo ? "Hide" : "Show", btStyle))
            showInfo = !showInfo;
        GUILayout.EndVertical();


    }
#endif
}