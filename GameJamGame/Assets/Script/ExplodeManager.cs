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
    private int radius = 10;

    public void Explode(Vector2 pos)
    {

        GameObject g = new GameObject();
        g.transform.position = pos;
        var colider = g.AddComponent<CircleCollider2D>();
        colider.radius = radius;
        colider.isTrigger = true;
        var rigi = colider.gameObject.AddComponent<Rigidbody2D>();
        rigi.bodyType = RigidbodyType2D.Kinematic;

        g.AddComponent<ExplosionElement>();


    }





}