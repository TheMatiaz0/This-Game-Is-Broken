using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver.Unity;
using UnityEngine.SceneManagement;

public class GameOverObject : CanvasMenu
{
	[SerializeField]
	private FreezeMenu ownFreezeMenu;

	protected new void OnEnable()
	{
		base.OnEnable();
		ownFreezeMenu.EnableMenuWithPause(true);
	
	}



	public void Retry()
	{		
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	
}
