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

public class KeybordRevert : GlitchEffect
{
    public override string Description => LeanLocalization.GetTranslationText("KeyboardRevertMessage");

    protected override void OnCancel()
    {
        WhenCollect();
    }

    public override void WhenCollect()
    {
        PlayerController player = PlayerController.Instance;
        player.KeysReversed = !player.KeysReversed;
    }
}