﻿using Cyberevolver;
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

    private const string SpeedName = "Speed";

    public static Wave Instance { get; protected set; }

    [field: SerializeField]
    public Direction         Direction          { get; private set; } = Direction.Right;
    [field: SerializeField , BoxGroup(SpeedName),  MinMaxSlider(1, 100)]
    public Range             MinMaxSpeed        { get; private set; } = new Range(1, 8);
    [field:SerializeField, BoxGroup(SpeedName)]
    public SerializeTimeSpan WhenSpeedWillBeMax { get; private set; } = new SerializeTimeSpan(TimeSpan.FromSeconds(5));

    public override bool IsBad => false;
 
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
        Vector2 change = Direction.ToVector2() * speed;
        this.Rgb.velocity = change;

    }
    protected override void OnColidWithPlayer(PlayerController player)
    {
        player.GlitchedSnapshot.TransitionTo(.5f);
        player.LensDistortion.enabled.value = true;
        StartCoroutine(ScaleUp(player));

        player.Kill();
    }

    private IEnumerator ScaleUp (PlayerController player)
    {
        player.LensDistortion.scale.value = 1f;

        while (player.LensDistortion.scale.value >= 0.30f)
        {
            player.LensDistortion.scale.value -= 0.004f;
            yield return Async.NextFrame;
        }
    }

}