﻿using Cyberevolver.Unity;
using System;
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
    private float metres = 0;
    private float startX;

	protected new void Awake()
	{
		base.Awake();
		timer = new Stopwatch();
		timer.Start();
	}
    private void OnDisable()
    {
        startX = this.transform.position.x;
    }

    protected void Update ()
	{

        metres = Math.Max(metres, PlayerController.Instance.transform.position.x - startX);
	
		if (timer != null && timer.IsRunning)
		{
			statisticsText.text = $"<color=red>{timer.Elapsed.Hours}h, {timer.Elapsed.Minutes}m, {timer.Elapsed.Seconds}s</color> | <color=yellow>{metres} meters</color>";
		}

	}
}
