using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation180Screen : GlitchEffect
{
	public override string Description => "ScreenOutOfIndexException";

	public override void WhenCollect()
	{
		Camera.main.transform.rotation = Camera.main.transform.rotation.Add(180);
	}
}
