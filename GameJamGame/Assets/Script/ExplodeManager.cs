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


    [SerializeField, Min(0)]
    private float             finalSize     = 10;
    [SerializeField]
    private SerializeTimeSpan time          = new SerializeTimeSpan(TimeSpan.FromSeconds(1));
    [SerializeField,RequiresAny]
    private ExplosionElement  explodePrefab = null;


    public void Explode(Vector2 pos)
    {
        ExplosionElement g = Instantiate(explodePrefab);
        g.transform.position = pos;
        g.Init(new Vector2(finalSize, finalSize), time.TimeSpan);
         
       
    }





}