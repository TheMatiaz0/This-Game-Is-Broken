using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsInfo : MonoBehaviourPlus
{
	[Auto]
	public Text statisticsText { get; private set; }

	private Stopwatch timer;

	protected new void Awake()
	{
		base.Awake();
		timer = new Stopwatch();
		timer.Start();
	}

	protected void Update ()
	{
		if (timer != null && timer.IsRunning)
		{
			statisticsText.text = $"<color=red>{timer.Elapsed.Hours}h, {timer.Elapsed.Minutes}m, {timer.Elapsed.Seconds}s</color> | <color=yellow>900 meters</color>";
		}
		
	}
}
