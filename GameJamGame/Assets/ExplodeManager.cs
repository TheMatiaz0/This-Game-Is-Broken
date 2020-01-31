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
    [Min(0)]
    private int radius;

    public void Explode(Vector2 pos)
    {

        GameObject g = new GameObject();
        var colider= g.AddComponent<CircleCollider2D>();
        colider.radius = radius;
        colider.isTrigger = true;
        var rigi= colider.gameObject.AddComponent<Rigidbody2D>();
        rigi.bodyType = RigidbodyType2D.Kinematic;

        g.AddComponent<ExplosionElement>();

        
    }





}