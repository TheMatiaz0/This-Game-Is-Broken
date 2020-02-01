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

public class Bullet : ActiveElement
{
    public override bool IsBad => false;
    public Direction Dir { get; set; }
    public float Speed { get; set; }
    private void Update()
    {
        this.transform.position += (Vector3)Dir.ToVector2() * Speed;

    }
    protected override void OnColidWithPlayer(PlayerController player)
    {

        PlayerController.Instance.PushBugs(GlitchEffect.GetRandomGlitchEffect());
        DestroyWithEffect();
    }
   
}