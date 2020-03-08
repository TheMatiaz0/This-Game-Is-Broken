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
using Lean.Localization;

public class SpriteFlip : GlitchEffect
{
    public override string Description => LeanLocalization.GetTranslationText("SpriteFlipMessage");

    protected override void OnCancel()
    {
         PlayerController.Instance.Sprite.flipY = false;
    }
    public override void WhenCollect()
    {
        PlayerController.Instance.Sprite.flipY = true;

    }
}