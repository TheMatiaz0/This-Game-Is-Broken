using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cyberevolver.Unity;

public class Tutorial : MonoBehaviour
{
	// Handles Tutorial 1-6:
	[SerializeField]
	private SpecificGameObjectFromGameObjects gameObjectScript;

	private InputActions inputActions;

	private int lastSectionNumber = 0;

	protected void Awake()
	{
		inputActions = new InputActions();
	}

	protected void OnEnable()
	{
		inputActions.Enable();
	}

	protected void OnDisable()
	{
		inputActions.Disable();
	}

	protected void Update()
	{
		if (inputActions.TutorialControls.SkipFull.triggered)
		{
			GameManager.Instance.Back();
		}

		if (inputActions.TutorialControls.SkipNext.triggered)
		{
			if (gameObjectScript.AllSections.Length > lastSectionNumber + 1)
			{
				gameObjectScript.ButtonClick(gameObjectScript.AllSections[lastSectionNumber += 1]);
			}

			else
			{
				GameManager.Instance.Back();
			}

		}
	}


}