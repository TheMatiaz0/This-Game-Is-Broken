using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cyberevolver.Unity;

public class Console : AutoInstanceBehaviour<Console>
{
	[SerializeField] private Text outputText = null;

	public void UpdateConsole (string addText)
	{
		outputText.text += $"{addText}\n";
	}

	public void ReplaceTextConsole (string newText)
	{
		outputText.text = newText;
	}

}
