using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver.Unity;
using UnityEngine.UI;

public class Countdown : AutoInstanceBehaviour<Countdown>
{
    private System.Timers.Timer timer;

    [SerializeField]
    private Text countdownText = null;

    [SerializeField]
    private GameObject countdownVisualObj = null;

    public bool IsCountdownEnabled { get; private set; } = false; 

    [SerializeField]
    private SerializeTimeSpan timeForEnd;

    private double actualTimer;

    private void Start()
    {
        IsCountdownEnabled = true;
        Invoke(() => TrueStart(), 0.001f);
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
            IsCountdownEnabled = false;
            Destroy(this.gameObject);
        }
    }

    private void OnDisable()
    {
        timer.Stop();
        // timer.Elapsed -= OnSecondPassed;
    }

}
