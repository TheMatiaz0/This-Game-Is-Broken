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

public class Hammer : Collectable
{
    public override bool IsBad => false;
    [SerializeField]
    private Gradient gradient;
    [Auto]
    public SpriteRenderer Sprite { get; protected set; }
    [SerializeField]
    [Range(0,100)]
    private float speed = 2;
    protected override void OnCollect()
    {
        ExplodeManager.Instance.Explode(this.transform.position);
        PlayerController.Instance.HammerUsage();
    }
    private void Update()
    {
        Sprite.color = gradient.Evaluate(((PlayerController.Instance.SceneTime * 100)%100) /100);
    }
}