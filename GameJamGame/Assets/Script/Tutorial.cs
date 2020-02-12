using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cyberevolver.Unity;
using System;

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
    public static bool TutorialIsActive { get; private set; } = false;

	protected void OnEnable()
	{
        TutorialIsActive=true;
		inputActions.Enable();
	}

	protected void OnDisable()
	{
        TutorialIsActive = false;
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
                GameObject slower = new GameObject();
                slower.name = "slower";
                slower.AddComponent<MonoBehaviourPlus>()
                    .StartCoroutine(RestoringScaling());
				GameManager.Instance.Back();
			}

		}
	}

    private IEnumerator RestoringScaling()
    {
        yield return Async.NextFrame;
        Time.timeScale = 0.45f;
        while(Time.timeScale<1)
        {
            yield return Async.NextFrame;
            Time.timeScale += Time.deltaTime/5f;
        }
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }
}