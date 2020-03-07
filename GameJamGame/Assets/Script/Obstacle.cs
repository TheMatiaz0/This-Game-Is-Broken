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

public class Obstacle : ActiveElement
{
    public override bool IsBad => false;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Bullet bullet;
        if((bullet= collision.GetComponent<Bullet>())!=null)
        {
            bullet.DestroyWithEffect();
        }
    }
    /*
    private void Update()
    {
       Rgb.velocity = Vector2.zero;
    }
    */
}