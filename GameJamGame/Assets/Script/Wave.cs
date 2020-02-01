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
    public static Wave Instance { get; protected set; }
    [field: SerializeField]
    public Direction Direction { get; private set; } = Direction.Right;
    [field: SerializeField]
    [field: MinMaxSlider(1, 100)]
    public Range MinMaxSpeed { get; private set; } = new Range(1, 8);
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
        Percent percent= time / (float)WhenSpeedWillBeMax.TimeSpan.TotalSeconds;
        return (MinMaxSpeed.Max*percent.AsFloatValue)+MinMaxSpeed.Min;

    }
    protected virtual void FixedUpdate()
    {
        if (End)
            return;
        float speed = GetSpeedForTime(Time.time);
        var change = Direction.ToVector2() * speed;
        this.Rgb.MovePosition((Vector2)this.transform.position + change);

    }
    protected override void OnColidWithPlayer(PlayerController player)
    {
        
        player.Death();
    }
   





}