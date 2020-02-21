using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver.Unity;
using UnityEngine.UI;

public class Countdown : MonoBehaviourPlus
{
    private System.Timers.Timer timer;

    [SerializeField]
    private Text countdownText = null;

    [SerializeField]
    private GameObject countdownVisualObj = null;

    [SerializeField]
    private SerializeTimeSpan timeForEnd;

    private int actualTimer;

    private void Start()
    {
        Invoke(() => TrueStart(), 0.5f);
        countdownVisualObj.SetActive(true);
    }

    private void TrueStart ()
    {
        // 1 second interval
        InvokeRepeating(() => )
        Time.timeScale = 0f;
    }

    private void OnSecondPassed(object sender, System.Timers.ElapsedEventArgs e)
    {
        countdownText.text = actualTimer.ToString();

        if (actualTimer >= timeForEnd.TimeSpan.TotalSeconds)
        {
            timer.Stop();
            Time.timeScale = 1f;
            return;
        }

        actualTimer += 1;
    }

    private void OnDisable()
    {
        timer.Stop();
        timer.Elapsed -= OnSecondPassed;
    }

}
