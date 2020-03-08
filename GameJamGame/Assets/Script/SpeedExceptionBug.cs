using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedExceptionBug : GlitchEffect
{
    public override string Description => LeanLocalization.GetTranslationText("SpeedExceptionMessage");

    protected override void OnCancel()
    {
        Time.timeScale = 1f;
    }
    public override void WhenCollect()
    {
        Time.timeScale = 1.33f;

    }
}
