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

public class Hammer : Collectable
{
    protected override void OnCollect()
    {
        GameManager.Instance.AddScore(10);
    }
}