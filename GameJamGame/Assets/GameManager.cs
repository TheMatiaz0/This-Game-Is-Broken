using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Cyberevolver;
using Cyberevolver.Unity;

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
            if(_Score!=value)
            {
                _Score = value;
                OnScoreChanged(this, value);
            }
            
        }
    }
    private void OnEnable()
    {
        OnScoreChanged += IfScoreChanged;
        IfScoreChanged(this,Score);
    }

    private void IfScoreChanged(object sender, SimpleArgs<int> e)
    {
        scoreEntity.text = $"Score:{Score}";

    }

    public event EventHandler<SimpleArgs<int>> OnScoreChanged = delegate { };
   
    public void AddScore(int val)
    {
        Score += val;
        LeanTween.size(scoreEntity.,new Vector2(2,2),1f).setOnComplete(()=>scoreEntity.)
    }


}