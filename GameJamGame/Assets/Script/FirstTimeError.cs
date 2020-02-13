using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTimeError : GlitchEffect
{
	private FreezeMenu freezeMenu;

	public override string Description => "FirstTime bool is undefined in XML file";

	public override void WhenCollect()
	{
		freezeMenu = GameObject.FindGameObjectWithTag("TutorialManager").GetComponent<FreezeMenu>();
		freezeMenu.FakePause(true);
	}

	protected override void OnCancel()
	{
		freezeMenu.FakePause(false);
	}
}
