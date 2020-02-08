using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Collectable
{
    public override bool IsBad =>true;
    protected override void OnCollect()
    {
        PlayerController.Instance.Kill();
    }

}
