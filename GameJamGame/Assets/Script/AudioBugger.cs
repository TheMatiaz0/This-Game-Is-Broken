using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioBugger : GlitchEffect
{
	public override string Description => LeanLocalization.GetTranslationText("AudioBuggerMessage");

	protected override void OnCancel()
	{
		PlayerController.Instance.StartingSnapshot.TransitionTo(.01f);
	}

	public override void WhenCollect()
	{
		PlayerController.Instance.GlitchedSnapshot.TransitionTo(.8f);
	}
}
