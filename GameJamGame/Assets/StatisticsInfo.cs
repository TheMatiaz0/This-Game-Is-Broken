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
	public Text statisticsText { get; private set; }

	public Stopwatch Timer { get; private set; }
	private float metres = 0;
	private float startX;

	protected new void Awake()
	{
		base.Awake();
		Timer = new Stopwatch();
		Timer.Start();
	}
	private void Start()
	{
		startX = PlayerController.Instance.transform.position.x;
	}

	protected void Update()
	{

		metres = Math.Max(metres, PlayerController.Instance.transform.position.x - startX);


		statisticsText.text = $"<color=red>{Timer.Elapsed.Hours}h, {Timer.Elapsed.Minutes}m, {Timer.Elapsed.Seconds}s</color>\n<color=yellow>{metres} meters</color>";


	}
}
