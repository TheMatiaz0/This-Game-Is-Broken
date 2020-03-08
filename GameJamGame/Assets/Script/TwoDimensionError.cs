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
using Lean.Localization;

public class TwoDimensionError : GlitchEffect
{
    public override string Description => LeanLocalization.GetTranslationText("2DMessage");
    protected override void OnCancel()
    {
        PlayerController.Instance.Cam.transform.rotation = PlayerController.Instance.Cam.transform.rotation.Add(new Vector3(0, -40, 0));
    }

    public override void WhenCollect()
    {
        PlayerController.Instance.Cam.transform.rotation= PlayerController.Instance.Cam.transform.rotation.Add(new Vector3(0, 40, 0));
    }
}