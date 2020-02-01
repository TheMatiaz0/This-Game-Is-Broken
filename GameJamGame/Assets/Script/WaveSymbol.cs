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

public class WaveSymbol : MonoBehaviourPlus
{
    [Auto]
    public SpriteRenderer Render { get; private set; }
  
    private void Start()
    {
        StartCoroutine(EUpdate());
    }
    private IEnumerator EUpdate()
    {
        while(true)
        {

            this.Render.sprite = WaveLook.Instance.GetRandomSprite();
            yield return Async.Wait(TimeSpan.FromSeconds(0.3f));
        }
    }
   

}