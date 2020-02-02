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
        PlayerController.Instance.Cam.transform.rotation = PlayerController.Instance.Cam.transform.rotation.Add(new Vector3(0, -40, 0));
    }

   
    public override void WhenCollect()
    {
        PlayerController.Instance.Cam.transform.rotation= PlayerController.Instance.Cam.transform.rotation.Add(new Vector3(0, 40, 0));
    }
}