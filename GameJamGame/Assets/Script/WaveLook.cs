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

    [SerializeField]
    [AssetOnly]
    private SpriteRenderer prefab;
    

    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private Vector2 elementSize;
    [SerializeField]
    private Vector2Int elementsQuanity;



    private SpriteRenderer[] elements;



    private Sprite GetRandomSprite()
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
                var obj = Instantiate(prefab, (Vector2)this.transform.position + new Vector2(x, y), Quaternion.identity);

                obj.sprite = GetRandomSprite();
                elements[i] = obj;


            }
    }
    private void Start()
    {
        Generate();

    }
    protected override void OnGUI()
    {
        base.OnGUI();
        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.transform.position, elementsQuanity * elementSize);

    }
}