using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseObject : MonoBehaviour
{
	
	public void Quit ()
	{
		SceneManager.LoadScene("Main Menu");
	}
}
