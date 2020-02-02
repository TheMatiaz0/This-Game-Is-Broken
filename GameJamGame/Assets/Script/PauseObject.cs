using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		Application.Quit(0);
	}
}
