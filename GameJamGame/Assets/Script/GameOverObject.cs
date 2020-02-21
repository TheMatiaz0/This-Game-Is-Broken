using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverObject : CanvasMenu
{
	[SerializeField]
	private FreezeMenu ownFreezeMenu;

	[SerializeField]
	private Text endScore = null;

	public Text EndScore => endScore;

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
