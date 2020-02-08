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

    private float timeOnStart;
    public TimeSpan GetTime() =>TimeSpan.FromSeconds(  Time.time - timeOnStart);
	protected new void Awake()
	{
		base.Awake();
        timeOnStart = Time.time;
	}

	protected void Update()
	{
        TimeSpan time = GetTime();
		StatisticsText.text = $"<color=red>{time.Hours}h, {time.Minutes}m, {time.Seconds}s</color>\n<color=yellow>{DistanceManager.Instance.GetMeters()} meters</color>";


	}
}
