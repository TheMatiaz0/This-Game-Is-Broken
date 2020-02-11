using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderWithText : SliderWithCounter
{
    protected void Start()
    {
        OnDrag();
    }

    public override void OnDrag()
    {
        TextCounter.text = slider.value.ToString();
    }
}
