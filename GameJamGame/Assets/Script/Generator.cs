using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
  
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField] 
    private Transform startRespPoint;
    [SerializeField]
    [Range(0,9)]
    private int howFastGenerate = 3;
    [SerializeField]
    [Range(1, 100)]
   
    private int blockInOneShoot = 10;
    [SerializeField]
    [Range(1, 10)]
    private int whenRemove = 3;
   
   
    private Queue<GameObject[]> blocksPacks = new Queue<GameObject[]>();
    private float lastX=0;
    public float GenerateOneLine( float fromX)
    {
        
        GameObject[] objs = new GameObject[10];
        for(int x=0;x< blockInOneShoot; x++)
        {    
            var block= Instantiate(blockPrefab);
            block.transform.position = new Vector2(fromX + x,startRespPoint.position.y);
            objs[x] = block;

        }
        blocksPacks.Enqueue(objs);
        return fromX + blockInOneShoot;
    }
    private void Start()
    {
        GenerateOneLine(startRespPoint.position.x-blockInOneShoot);
        lastX = startRespPoint.position.x;
        lastX= GenerateOneLine(lastX);
        
    }
    private void Update()
    {
        if(PlayerController.Instance.transform.position.x>(lastX)-howFastGenerate)
        {
            lastX= GenerateOneLine(lastX);
            if(blocksPacks.Count>= whenRemove)
            {
               foreach(var item in  blocksPacks.Dequeue())
                {
                    Destroy(item.gameObject);
                }
            }
        }
    }

}
