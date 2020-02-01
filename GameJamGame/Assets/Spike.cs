using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Collectable
{
	protected override void OnCollect()
	{
		PlayerController.Instance.Death();
	}
}
