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