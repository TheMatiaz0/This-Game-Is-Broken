using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsInfo : MonoBehaviour
{
	[Auto]
	public Text statisticsText { get; private set; }

	private Stopwatch timer;
    private float metres = 0;
    private float startX;

	protected void Awake()
	{
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
		statisticsText.text = $"<color=red>{timer.Elapsed.TotalHours}h, {timer.Elapsed.TotalMinutes}m, {timer.Elapsed.TotalSeconds}s</color> | <color=yellow>{metres} meters</color>";
	}
}
