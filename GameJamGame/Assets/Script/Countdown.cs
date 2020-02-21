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

    private double actualTimer;

    private void Start()
    {
        Invoke(() => TrueStart(), 0.3f);
        countdownVisualObj.SetActive(true);
    }

    private void TrueStart ()
    {
        actualTimer = timeForEnd.TimeSpan.TotalSeconds;
        Time.timeScale = 0f;
        // 1 second interval
        timer = new System.Timers.Timer(1000);
        timer.Start();
        timer.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) => actualTimer -= 1;

    }

    private void Update()
    {
        if (timer == null)
        {
            return;
        }

        countdownText.text = (actualTimer).ToString();

        if (actualTimer <= 0)
        {
            timer.Stop();
            countdownVisualObj.SetActive(false);
            Time.timeScale = 1f;
            Destroy(this.gameObject);
        }
    }

    private void OnDisable()
    {
        timer.Stop();
        // timer.Elapsed -= OnSecondPassed;
    }

}
