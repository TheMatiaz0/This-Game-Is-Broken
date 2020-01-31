using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cyberevolver;
using Cyberevolver.Unity;

public  class Wave : AutoInstanceBehaviour<Wave>
{
    

   [Auto]
   public Rigidbody2D Rgb { get; private set; }
    [field: SerializeField]
    public Direction Direction { get; private set; } = Direction.Right;
    [field: SerializeField]
    [field: MinMaxRange(0.1f, 10)]
    public float LimitedSpeed { get; private set; } = 1f;
    [field: SerializeField]
    [field: MinMaxRange(0.09f, 1)]
    public float TimeDivider { get; private set; } = 0.3f;




    public float GetSpeedForTime(float time)
    {
        const float minRange = 3f / 4;
        const float maxRange = 1f;
        float raw = (float)(Math.Tanh(time/ TimeDivider));
        return LimitedSpeed* (minRange + raw * (maxRange - minRange));
    }
    protected virtual void FixedUpdate()
    {      
        float speed = GetSpeedForTime(Time.time);

        var change = Direction.ToVector2() * speed;
        Debug.Log(change.x);
  
        this.Rgb.MovePosition((Vector2)this.transform.position + change);

    }

#if UNITY_EDITOR
    [StartHorizontal]
    public AnimationCurve curve;
    [Button()]
    [EndAfter]
    public void Generate()
    {
        curve = new AnimationCurve();
        for(float val=0; val<5; val+=0.1f)
        {
            curve.AddKey(val, GetSpeedForTime(val));
        }
    }

#endif



}