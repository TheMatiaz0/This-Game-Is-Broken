using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cyberevolver.Unity;

public class UIChanger : AutoInstanceBehaviour<UIChanger>
{
	public void Activate (bool isTrue)
	{
		foreach (var item in GetComponentsInChildren<Graphic>())
		{
			item.enabled = isTrue;
		}
	}
}
