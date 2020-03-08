using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishyEyeBug : GlitchEffect
{
	public override string Description => LeanLocalization.GetTranslationText("FishyEyeMessage");

	public static void ResetValues ()
	{
		PlayerController.Instance.LensDistortion.enabled.value = false;
	}

	public override void WhenCollect()
	{
		PlayerController.Instance.LensDistortion.enabled.value = true;
	}

	protected override void OnCancel()
	{
		ResetValues();
	}
}
