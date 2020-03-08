using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteBug : GlitchEffect
{
	public static readonly Color32 defaultColor = new Color32(0, 0, 0, 128);

	public static void ResetValues ()
	{
		Vignette vignette = PlayerController.Instance.VignetteEffect;
		vignette.color.value = defaultColor;
		vignette.center.value.x = 0.5f;
		vignette.center.value.y = 0.5f;
		vignette.intensity.value = 0.55f;
		vignette.smoothness.value = 0.2f;
		vignette.roundness.value = 1f;
		vignette.rounded.value = false;
	}

	public override string Description => LeanLocalization.GetTranslationText("VignetteMessage");

	protected override void OnCancel()
	{
		ResetValues();
	}

	public override void WhenCollect()
	{
		Vignette vignette = PlayerController.Instance.VignetteEffect;
		vignette.color.value = Color.red;
		vignette.center.value.x = 0.26F;
		vignette.center.value.y = 0.5F;
		vignette.intensity.value = 1;
		vignette.smoothness.value = 1;
		vignette.roundness.value = 1;
		vignette.rounded.value = true;
	}
}
