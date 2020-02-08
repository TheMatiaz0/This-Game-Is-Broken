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

public class WaveLook : AutoInstanceBehaviour<WaveLook>
{
    [SerializeField,RequiresAny]
    private Transform      upPoint         = null;
    [SerializeField, RequiresAny] 
    private SpriteRenderer prefab          = null;  
    [SerializeField]
    private Sprite[]       sprites         = null;
    [SerializeField]
    private Vector2        elementSize     = new Vector2(0.5f,0.5f);
    [SerializeField]
    private Vector2Int     elementsQuanity = new Vector2Int(10,10);
    [SerializeField]
    private Gradient       gradient        = new Gradient() { colorKeys = new GradientColorKey[] { new GradientColorKey(Color.yellow, 0), new GradientColorKey(Color.red, 1) } };
    [SerializeField, Range(0.2f,40)]
    private float          spriteSpeed     = 1;


    private SpriteRenderer[] elements;
    protected virtual void Start()
    {
        Generate();

    }
    public Sprite GetRandomSprite()
    {
        if (sprites.Length == 0)
            return null;
        else
            return sprites[UnityEngine.Random.Range(0, sprites.Length)];
    }

    private void Generate()
    {
        elements = new SpriteRenderer[elementsQuanity.x * elementsQuanity.y];

        for(int x=0,i=0;x< elementsQuanity.x;x++)
            for(int y=0;y< elementsQuanity.y;y++,i++)
            {
                float xMoved = elementSize.x * UnityEngine.Random.Range(0,1f/2);
                float yMoved = elementSize.y * UnityEngine.Random.Range(0, 1f / 2);
                var obj = Instantiate(prefab, (Vector2)upPoint.position + new Vector2(x, y)*elementSize+ new Vector2(xMoved,yMoved), Quaternion.identity);

                obj.gameObject.AddComponent<WaveSymbol>().Init(gradient, TimeSpan.FromSeconds(1f / spriteSpeed));
                obj.transform.SetParent(this.gameObject.transform);
                elements[i] = obj;


            }
    }
  

}