using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum BlockMode
{
    Left,
    Right,
    Center
}

public class Generator : MonoBehaviour
{
    private const string Raw = "Values";
    private const string ChanceName = "Chance";
    private const string PrefabsName = "Prefabs";
    private const string Transform = "Transform";

    [ShowCyberInspector]
    [Serializable]
    public struct FindableItemInfo
    {
        [field: SerializeField]
        [field: AssetOnly]
        public GameObject prefab;
        [field: SerializeField]
        public Percent howOften;
    }
    [SerializeField]
    [WindowArray]
    private List<FindableItemInfo> findablePrefabs = new List<FindableItemInfo>();


    [SerializeField,BoxGroup(ChanceName)]
    private Percent chanceForBlankBlock = 0.15f;
    [BoxGroup(Raw)]
    [SerializeField]
    [Range(1, 100)]
    private int blockInOneShoot = 10;
    [BoxGroup(Raw)]
    [SerializeField]
    [Range(0, 9)]
    private int howFastGenerate = 3;
    [SerializeField]
    [BoxGroup(Raw)]
    [Range(1, 10)]
    private int whenRemove = 3;
    [BoxGroup(Raw)]
    [SerializeField]
    [Range(1, 100)]
    private int howOftenCanHaveItem = 2;
    [SerializeField]
    [BoxGroup(PrefabsName)]
    [RequiresAny]
    private GameObject blockPrefab;
    [SerializeField]
    [BoxGroup(PrefabsName)]
    [ArrayAsField("LeftEdge", "RightEdge")]
    private Sprite[] edges;
    [SerializeField]
    [BoxGroup(Transform)]
    private Transform startRespPoint;
    [SerializeField]
    [BoxGroup(Transform)]
    private Transform maxUp;
    [SerializeField]
    [BoxGroup(Transform)]
    private Transform minUp;

    public uint PutedBlocksQuanity { get; private set; } = 0;
    private Range YRange => new Range(startRespPoint.position.y, maxUp.position.y);
    private Queue<GameObject[]> blocksPacks = new Queue<GameObject[]>();
    private float lastX = 0;

    public float GenerateOneLine(float fromX, float blocks, float y, 
        bool dontPutActiveItems = false,bool dontMakeEdge=false)
    {
        GameObject[] objs = new GameObject[10];
        for (int x = 0; x < blocks; x++)
        {

            BlockMode mode = BlockMode.Center;
            if(dontMakeEdge==false)
            {
                if (x == 0)
                {
                    mode = BlockMode.Left;
                }
                else if (x == blocks - 1)
                {
                    mode = BlockMode.Right;
                }
            }
          
           
            objs[x] = PutBlock(new Vector2(fromX + x, y), dontPutActiveItems,mode);

        }
        blocksPacks.Enqueue(objs);
        return fromX + blockInOneShoot;  
    }

    public GameObject GetRandomStuff()
    {

        PutedBlocksQuanity++;
        if(findablePrefabs.Count == 0 ||PutedBlocksQuanity %howOftenCanHaveItem!=0)
        {
            return null;
        }      
        int x = UnityEngine.Random.Range(0, findablePrefabs.Count);
        // Debug.Log(x);
        if (Chance(findablePrefabs[x].howOften))
        {
            return findablePrefabs[x].prefab;
        }
        else
            return null;
    }

    private bool Chance (Percent percent)
    {
        return UnityEngine.Random.Range(0, 1f + 0.01f) <= percent.AsFloatValue;
    }

    public GameObject PutBlock(Vector2 pos,bool dontPutActiveItems=false, BlockMode mode = BlockMode.Center)
    {
        var block = Instantiate(blockPrefab);
        block.transform.position = pos;
        SpriteRenderer render = block.GetComponent<SpriteRenderer>();
        switch (mode)
        {
            case BlockMode.Left:
                render.sprite = edges[0] ?? render.sprite;
                break;
            case BlockMode.Right:
                render.sprite = edges[1] ?? render.sprite;
                break;
            default: break;
        }

        if (dontPutActiveItems==false&&UnityEngine.Random.Range(0, 4) == 0)
        {
            var prefab = GetRandomStuff();
            if (prefab != null)
            {
                GameObject gameObj = Instantiate(prefab);
                gameObj.transform.position = (Vector2)block.transform.position + Vector2.up;
                
            }
        }
        return block;
    }
    public float GenerateChunk(float fromX, Range range,bool dontPutActiveItems=false)
    {
        float result = GenerateOneLine(fromX,blockInOneShoot,startRespPoint.position.y,dontMakeEdge:true);
        List<Vector2> busy = new List<Vector2>();
        List<GameObject> blocks = new List<GameObject>();
        for (float y = range.Min + 2/*No in basic line and no one cube over basic line*/; y < range.Max; y++)
        {
            if (UnityEngine.Random.Range(0, 3) == 0)
            {
                int lenght = UnityEngine.Random.Range(2, 6);
                GenerateOneLine(fromX,lenght,y,dontPutActiveItems);    
                y += lenght;
            }
        }
        blocksPacks.Enqueue(blocks.ToArray());
        return result;
    }
    private void Start()
    {
        PutBlock(startRespPoint.position, dontPutActiveItems: true, BlockMode.Left);
        lastX = GenerateOneLine(startRespPoint.position.x+1, blockInOneShoot, startRespPoint.position.y,dontMakeEdge:true,dontPutActiveItems:true);
        lastX = GenerateChunk(lastX, YRange,true);

    }
    private void Update()
    {
        if (PlayerController.Instance.transform.position.x > (lastX) - howFastGenerate)
        {

           while (blocksPacks.Count >= whenRemove*2)
            {
                foreach (var item in blocksPacks.Dequeue())
                {
                    if (item != null)
                    Destroy(item.gameObject);
                }
            }
            lastX = GenerateChunk(lastX, YRange);
        }
    }

}
