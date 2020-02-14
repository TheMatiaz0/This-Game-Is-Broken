using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedExceptionBug : GlitchEffect
{
    public override string Description => "NullReferenceException: Time.timeScale is null";

    protected override void OnCancel()
    {
        Time.timeScale = 1f;
    }
    public override void WhenCollect()
    {
        Time.timeScale = 1.33f;

    }
}
