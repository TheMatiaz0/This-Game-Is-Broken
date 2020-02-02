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

public class Bullet : Collectable
{
    public override bool IsBad => true;
    public Direction Dir { get; set; }
    public float Speed { get; set; }
    private void Update()
    {
        this.transform.position += (Vector3)Dir.ToVector2() * Speed * Time.deltaTime;

        if (Vector2.Distance(this.transform.position, PlayerController.Instance.transform.position) > 10)
        {
            Destroy(this.gameObject);
        }

    }
    protected override void OnCollect()
    {
        PlayerController.Instance.PushBugs(GlitchEffect.GetRandomGlitchEffect()); 
    }

}