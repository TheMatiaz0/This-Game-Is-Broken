using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsInfo : AutoInstanceBehaviour<StatisticsInfo>
{
	[Auto]
	public Text StatisticsText { get; private set; }

	public Stopwatch Timer { get; private set; }

	protected new void Awake()
	{
		base.Awake();
		Timer = new Stopwatch();
		Timer.Start();
	}

	protected void Update()
	{
		StatisticsText.text = $"<color=red>{Timer.Elapsed.Hours}h, {Timer.Elapsed.Minutes}m, {Timer.Elapsed.Seconds}s</color>\n<color=yellow>{DistanceManager.Instance.GetMeters()} meters</color>";


	}
}
