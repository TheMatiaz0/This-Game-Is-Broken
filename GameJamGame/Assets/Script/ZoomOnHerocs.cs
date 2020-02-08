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

public class ZoomOnHerocs : GlitchEffect
{
    private float before;
    public override string Description => "Missing reference exception \"Zoom.json\"";

    protected override void OnCancel()
    {
        PlayerController.Instance.PrefferedCameraZoom = before;
    }
    public override void WhenCollect()
    {
        before = PlayerController.Instance.PrefferedCameraZoom;
        PlayerController.Instance.PrefferedCameraZoom /=1.6f;
    }
}