using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver.Unity;
using UnityEngine.SceneManagement;

public class GameOverObject : MonoBehaviour
{
	[SerializeField]
	private FreezeMenu ownFreezeMenu;

	protected void OnEnable()
	{
		ownFreezeMenu.EnableMenuWithPause(true);
	}


	public void Retry ()
	{
		// freeze or not freeze?
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Quit ()
	{
		Application.Quit(0);
	}
}
