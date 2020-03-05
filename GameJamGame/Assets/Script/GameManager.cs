using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : AutoInstanceBehaviour<GameManager>
{
    [SerializeField]
    private Text scoreEntity;

    [SerializeField]
    private FreezeMenu tutorialManager;

    [SerializeField]
    private GameObject countdownObject = null;

    public GameObject CountdownObject => countdownObject;

    private int _Score;
    public int Score
    {
        get => _Score;
        set
        {
            if (_Score != value)
            {
                _Score = value;
                OnScoreChanged(this, value);
            }

        }
    }
 

    private void OnEnable()
    {
        OnScoreChanged += IfScoreChanged;
        IfScoreChanged(this, Score);
    }

    private void Start()
    {
        bool firstTime = OptionsManager.CurrentConfig.FirstTime;//it has to be here becuase "delayed invoking"

        if (firstTime)
        {
            countdownObject.SetActive(false);
        }

        else
        {
            countdownObject.SetActive(true);
            return;
        }
        Invoke(() => tutorialManager.EnableMenuWithPause(firstTime), 0.05f);
        OptionsManager.CurrentConfig.FirstTime = false;
        OptionsManager.CurrentConfig.Save();
    }

    public void Back ()
    {
        tutorialManager.EnableMenuWithPause(false);
        StartCoroutine(BackToNormalTime());
    }

    private IEnumerator BackToNormalTime()
    {
        while (Time.timeScale < 1f)
        {
            Time.timeScale += 0.001f;
            yield return Async.NextFrame;
        }
    }

    private void IfScoreChanged(object sender, SimpleArgs<int> e)
    {
        scoreEntity.text = $"{Score}";
    }

    public void AddScore(int val)
    {
        Score += val;
    }
    public event EventHandler<SimpleArgs<int>> OnScoreChanged = delegate { };
#if UNITY_EDITOR

    [StartHorizontal]
    [field: SerializeField]
    [CustomGui("")]
    private int _devS;
    [Button("AddScore")]
    [EndAfter]
    public void _AddScoreManual()
    {
        AddScore(_devS);
    }
#endif



}