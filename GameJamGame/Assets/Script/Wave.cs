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
using UnityEngine.SceneManagement;

public class Wave : ActiveElement
{
    public override bool IsBad => false;
    [field: SerializeField]
  
    public Direction Direction { get; private set; } = Direction.Right;
    [field: SerializeField]
    [field: BoxGroup(SpeedName)]
    [field: MinMaxSlider(1, 100)]
    public Range MinMaxSpeed { get; private set; } = new Range(1, 8);
    private const string SpeedName = "Speed";
    public static Wave Instance { get; protected set; }
    [field: BoxGroup(SpeedName)]
    [field:SerializeField]
    public SerializeTimeSpan WhenSpeedWillBeMax { get; private set; }
    = new SerializeTimeSpan(TimeSpan.FromSeconds(5));

    public bool End { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
    public float GetSpeedForTime(float time)
    {
        float percent =(float) Math.Min(time / (float)WhenSpeedWillBeMax.TimeSpan.TotalSeconds,1);
        return (MinMaxSpeed.Max*percent)+MinMaxSpeed.Min;

    }
    protected virtual void FixedUpdate()
    {
        if (End)
            return;
        float speed = GetSpeedForTime(PlayerController.Instance.SceneTime);
        var change = Direction.ToVector2() * speed;
        this.Rgb.velocity = change;

    }
    protected override void OnColidWithPlayer(PlayerController player)
    {
        
        player.Kill();
    }
   





}