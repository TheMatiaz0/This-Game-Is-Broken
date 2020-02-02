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

public class _2DErrorcs : GlitchEffect
{
    public override string Description => "2D not responding";



    protected override void OnCancel()
    {
        PlayerController.Instance.PrefferedCameraRotate -= new Vector3(0, 30, 0);
    }

   
    public override void WhenCollect()
    {
        PlayerController.Instance.PrefferedCameraRotate += new Vector3(0, 30, 0);
    }
}