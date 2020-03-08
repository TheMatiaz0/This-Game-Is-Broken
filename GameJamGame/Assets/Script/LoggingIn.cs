using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggingIn : MonoBehaviour
{
#if GAME_JOLT
	protected void Start()
	{
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
