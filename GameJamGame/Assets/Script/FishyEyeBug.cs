using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishyEyeBug : GlitchEffect
{
	public override string Description => "You_Are_Now_A_Fish Exception";

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
