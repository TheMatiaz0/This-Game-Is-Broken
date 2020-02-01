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

public class KeybordRevert : GlitchEffect
{
    public override string Description => "LoadKeyboardException";
    public override void WhenCollect()
    {
        var temp = PlayerController.Instance.LeftKey;
        PlayerController.Instance.LeftKey = PlayerController.Instance.RightKey;
        PlayerController.Instance.RightKey = temp;
    }
}