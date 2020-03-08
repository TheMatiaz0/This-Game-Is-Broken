using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggingIn : MonoBehaviour
{
	protected void Start()
	{
#if GAME_JOLT

		GameJolt.UI.GameJoltUI.Instance.ShowSignIn();
#else
		ChangeScene();
#endif
	}

	public void ChangeScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
	}


}
