using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cyberevolver;
using Cyberevolver.Unity;




public class ExplodeManager : AutoInstanceBehaviour<ExplodeManager>
{

    [SerializeField]
    private GameObject explodePrefab;
    [SerializeField]
    private SerializeTimeSpan growTime;
    [SerializeField]
    [Range(2,100)]
    private float scalePower;

    public void Explode(Vector2 pos)
    {

        
        GameObject g= Instantiate(explodePrefab.gameObject);
        g.transform.position = pos;
        g.AddComponent<ExplosionElement>();
        LeanTween.scale(g, new Vector2(scalePower, scalePower), (float)growTime.TimeSpan.TotalSeconds)
            .setOnComplete(() => LeanTween.alpha(g, 0, 1));
        
    }





}