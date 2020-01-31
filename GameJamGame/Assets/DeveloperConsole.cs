using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeveloperConsole : MonoBehaviour
{
	[SerializeField]
	private InputField inputField;

	[SerializeField]
	private Text outputField;

	protected void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{

			outputField.text += $">> {inputField.text}\n";
			inputField.text = string.Empty;
		}
	}
}
