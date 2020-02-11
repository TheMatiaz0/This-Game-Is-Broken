using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleExpansion : MonoBehaviour
{
	[SerializeField] private Graphic graphicForDisabled = null;
	[SerializeField] private Graphic graphicForEnabled = null;

	public void OnValueChange(bool isOn)
	{
		graphicForEnabled.gameObject.SetActive(isOn);
		graphicForDisabled.gameObject.SetActive(!isOn);
	}
}
