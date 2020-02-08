using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation180Screen : GlitchEffect
{
	public override string Description => "Screen_Out_Of_Index_Exception";

	protected override void OnCancel()
	{
        PlayerController.Instance.Cam.transform.rotation = PlayerController.Instance.Cam.transform.rotation.Add(new Vector3(0, -180, 180));
    }

    public override void WhenCollect()
    {
        PlayerController.Instance.Cam.transform.rotation = PlayerController.Instance.Cam.transform.rotation.Add(new Vector3(0, 180, 180));
    }
    
}
