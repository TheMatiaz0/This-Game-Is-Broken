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
    [field: MinMaxRange(0.1f, 10)]
    public float LimitedSpeed { get; private set; } = 1f;
    [field: SerializeField]
    [field: MinMaxRange(0.09f, 1)]
    public float TimeDivider { get; private set; } = 0.3f;
    public bool End { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
    public float GetSpeedForTime(float time)
    {
        const float minRange = 3f / 4;
        const float maxRange = 1f;
        float raw = (float)(Math.Tanh(time / TimeDivider));
        return LimitedSpeed * (minRange + raw * (maxRange - minRange));
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