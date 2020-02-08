using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : ActiveElement
{
    public override bool IsBad =>true;
    protected override void OnColidWithPlayer(PlayerController player)
    {
        PlayerController.Instance.Kill();
        
    }

}
