using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsInfo : MonoBehaviour
{
	[SerializeField]
	private Text statisticsText = null;

	private Stopwatch timer;

	protected void Awake()
	{
		timer = new Stopwatch();
		timer.Start();
	}

	protected void Update ()
	{
		statisticsText.text = $"<color=red>{timer.Elapsed.TotalHours}h, {timer.Elapsed.TotalMinutes}m, {timer.Elapsed.TotalSeconds}s</color> | <color=yellow>900 meters</color>";
	}
}
