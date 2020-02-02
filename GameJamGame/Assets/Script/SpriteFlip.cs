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

public class SpriteFlip : GlitchEffect
{
    public override string Description => throw new NotImplementedException();

    protected override void OnCancel()
    {
        
    }
    public override void WhenCollect()
    {
        base.WhenCollect();

    }
}