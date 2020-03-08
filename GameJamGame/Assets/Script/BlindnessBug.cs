using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindnessBug : GlitchEffect
{
	public override string Description => LeanLocalization.GetTranslationText("BlindessMessage");

	public static void ResetValues ()
	{
		PlayerController.Instance.DepthOfField.enabled.value = false;
	}

	public override void WhenCollect()
	{
		PlayerController.Instance.DepthOfField.enabled.value = true;
	}

	protected override void OnCancel()
	{
		ResetValues();
	}

}
