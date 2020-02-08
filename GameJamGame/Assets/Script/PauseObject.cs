using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseObject : MonoBehaviour
{
	private void OnEnable()
	{
		StatisticsInfo.Instance.Timer.Stop();
	}

	private void OnDisable()
	{
		StatisticsInfo.Instance.Timer.Start();
	}

	public void Quit ()
	{
		SceneManager.LoadScene("Main Menu");
	}
}
