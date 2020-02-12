using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cyberevolver.Unity;

public class UIChanger : AutoInstanceBehaviour<UIChanger>
{
	public bool IsHidden { get; private set; } = false;

	public void Activate (bool isTrue)
	{
		foreach (var item in GetComponentsInChildren<Graphic>())
		{
			item.enabled = isTrue;
			IsHidden = isTrue;
		}
	}

	public void ReswitchUI ()
	{
		Activate(!IsHidden);
	}
}
