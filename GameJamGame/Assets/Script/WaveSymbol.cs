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
    public Gradient Gradient { get; private set; }
    public TimeSpan Delay { get; private set; }
    private float moverValue;

    private void Start()
    {
        moverValue = UnityEngine.Random.Range(0f, 1f);
        StartCoroutine(MainLoop());
    }
    public void Init(Gradient gradient, TimeSpan delay )
    {
        Gradient = gradient;
        Delay = delay;
      
       
    }
    private IEnumerator MainLoop()
    {

        while(true)
        {

            this.Render.sprite = WaveLook.Instance.GetRandomSprite();

            yield return Async.Wait(Delay);
        }
    }
    private void Update()
    {
        this.Render.color = Gradient.Evaluate((((moverValue+ PlayerController.Instance.SceneTime) * 100) % 100) / 100);
    }


}