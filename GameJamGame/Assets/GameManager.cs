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
    [SerializeField]
    private LeanTweenType scoreAnim = LeanTweenType.easeInOutBounce;
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
#if UNITY_EDITOR
    [StartHorizontal]
    [field:SerializeField]
    [CustomGui("Score")]
    private int _devS;
    [Button("AddScore")]
    [EndAfter]
    public void _AddScoreManual()
    {
        AddScore(_devS);
    }
#endif
    public void AddScore(int val)
    {
        Score += val;
        var anim = LeanTween.scale(scoreEntity.rectTransform, new Vector2(2, 2), 1f);
        anim.setEase(scoreAnim).setOnComplete(() => LeanTween.scale(scoreEntity.rectTransform, new Vector2(1, 1), 1f));
    }


}