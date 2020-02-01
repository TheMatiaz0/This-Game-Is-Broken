using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation180Screen : GlitchEffect
{
	public override string Description => "Screen_Out_Of_Index_Exception";

	protected override void OnCancel()
	{
		WhenCollect();
	}

	public override void WhenCollect()
	{
		
		LeanTween.rotate(Camera.main.gameObject, Camera.main.transform.rotation.eulerAngles + new Vector3(0, 180, 180), 2f);
	}
}
