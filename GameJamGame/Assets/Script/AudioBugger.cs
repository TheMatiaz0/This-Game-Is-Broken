using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioBugger : GlitchEffect
{
	public override string Description => "AUDIO_DRIVER_CRASH";

	protected override void OnCancel()
	{
		PlayerController.Instance.StartingSnapshot.TransitionTo(.01f);
	}

	public override void WhenCollect()
	{
		PlayerController.Instance.GlitchedSnapshot.TransitionTo(.8f);
	}
}
