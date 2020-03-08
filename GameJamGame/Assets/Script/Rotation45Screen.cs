using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation45Screen : GlitchEffect
{
	public override string Description => LeanLocalization.GetTranslationText("Rotation45Message");

    protected override void OnCancel()
    {
        PlayerController.Instance.Cam.transform.rotation = PlayerController.Instance.Cam.transform.rotation.Add(new Vector3(0, -45, 45));
    }

    public override void WhenCollect()
    {
        PlayerController.Instance.Cam.transform.rotation = PlayerController.Instance.Cam.transform.rotation.Add(new Vector3(0, 45, 45));
    }
}
