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


        Camera.main.transform.rotation = Camera.main.transform.rotation.Add(new Vector3(0,180,180));
     
    ;

	}
}
