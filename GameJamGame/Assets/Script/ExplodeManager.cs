using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;




public class ExplodeManager : AutoInstanceBehaviour<ExplodeManager>
{


    [SerializeField]
    [Min(0)]
    private float finalSize = 10;
    [SerializeField]
    private SerializeTimeSpan time;


    [SerializeField]
    private ExplosionElement explodePrefab;


    public void Explode(Vector2 pos)
    {
        GameObject g = Instantiate(explodePrefab).gameObject;
        g.transform.position = pos;

        LeanTween.scale(g, new Vector2(finalSize, finalSize), (float)time.TimeSpan.TotalSeconds)
            .setOnComplete(() => Destroy(g));




    }





}