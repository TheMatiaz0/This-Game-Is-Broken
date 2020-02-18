using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemplateTextSingleton : AutoInstanceBehaviour<TemplateTextSingleton>
{
	[SerializeField]
	private Text templateText = null;

	public Text TemplateText => templateText;
}
