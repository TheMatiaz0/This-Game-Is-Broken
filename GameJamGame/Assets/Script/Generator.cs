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

    #region CONST
    private const string Raw = "Values";
    private const string ChanceName = "Chance";
    private const string PrefabsName = "Prefabs";
    private const string Transform = "Transform";
    #endregion

    #region SERIALIZE_FIELDS

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

    [SerializeField, WindowArray]
    private List<FindableItemInfo> findablePrefabs = new List<FindableItemInfo>();

    //chance

    [SerializeField, BoxGroup(ChanceName)]
    private Percent chanceForUpperPlatform  = 0.25f;
    [SerializeField, BoxGroup(ChanceName)]
    private Percent chanceForAnyActiveItems = 0.33f;
   
    //Raw

    [SerializeField, BoxGroup(Raw), Range(1, 100)]
    private int        blockInOneShoot     = 10;
    [SerializeField, BoxGroup(Raw), Range(0, 99)]
    private int        howFastGenerate     = 3;
    [SerializeField, BoxGroup(Raw), Range(1, 10)]
    private int        whenRemove          = 3;
    [SerializeField, BoxGroup(Raw), Range(1, 100)]
    private int        howOftenCanHaveItem = 2;
    [SerializeField, BoxGroup(Raw), MinMaxSlider(1, 10)]
    private Vector2Int platformsSize       = new Vector2Int(2, 6);

    //Prefabs
    [SerializeField, BoxGroup(PrefabsName), RequiresAny]
    private GameObject blockPrefab = null;
    [SerializeField, BoxGroup(PrefabsName), ArrayAsField("LeftEdge", "RightEdge")]
    private Sprite[]   edges       = null;

    //Transforms

    [SerializeField, BoxGroup(Transform), RequiresAny]
    private Transform startRespPoint = null;
 
    [SerializeField, BoxGroup(Transform), RequiresAny]
    private Transform maxUp          = null;

    #endregion

    #region PROPERTIES&FIELDS

    private readonly Queue<GameObject[]> blocksPacks = new Queue<GameObject[]>();
    private float lastX = 0;
    public uint PutedBlocksQuanity { get; private set; } = 0;
    private Range YRange => new Range(startRespPoint.position.y, maxUp.position.y);
    #endregion

    #region METHODS

    protected virtual void Start()
    {
        lastX = startRespPoint.position.x;
        PutBlock(startRespPoint.position, dontPutActiveItems: true, BlockMode.Left);
        lastX = GenerateChunk(startRespPoint.position.x + 1, YRange, dontPutActiveItems: true, dontPutUpperPlatform: true, dontMakeEdge: true);

    }

    protected virtual void Update()
    {
        if (PlayerController.Instance.transform.position.x > (lastX) - howFastGenerate)
        {
            while (blocksPacks.Count >= whenRemove * 2)
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

    public float GenerateOneLine(float fromX, float blocks, float y, 
        bool dontPutActiveItems = false,bool dontMakeEdge=false,bool flip=false)
    {
        GameObject[] objs = new GameObject[10];
        for (int x = 0; x < blocks; x++)
        {

            BlockMode mode = BlockMode.Center;
            bool dontPut = dontPutActiveItems;
            if (dontMakeEdge==false)
            {
                if (x == 0)
                {
                    mode = BlockMode.Left;
                    dontPut = true;
                }
                else if (x == blocks - 1)
                {
                    mode = BlockMode.Right;
                    dontPut = true;
                }
            }

            GameObject block;
            objs[x] = block = PutBlock(new Vector2(fromX + x, y), dontPut, mode);
            if (flip)
            {
                block.GetComponent<SpriteRenderer>().flipY = true;
            }
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
        GameObject block = Instantiate(blockPrefab);
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

        if (dontPutActiveItems==false&&Chance(chanceForAnyActiveItems))
        {
            var prefab = GetRandomStuff();
            if (prefab != null)
            {
                GameObject gameObj = Instantiate(prefab);
                gameObj.transform.SetParent(block.transform);
                gameObj.transform.position = (Vector2)block.transform.position + Vector2.up;
                
            }
        }
        block.isStatic = true;
        return block;
    }

    public float GenerateChunk(float fromX, Range range,bool dontPutActiveItems=false,bool dontPutUpperPlatform=false,bool dontMakeEdge=false)
    {
        float result = GenerateOneLine(fromX,blockInOneShoot,startRespPoint.position.y,dontMakeEdge:true,dontPutActiveItems:dontPutActiveItems);
        GenerateOneLine(fromX, blockInOneShoot, startRespPoint.position.y+maxUp.position.y+5, dontPutActiveItems:true,dontMakeEdge: true,flip:true);
        
        List<GameObject> blocks = new List<GameObject>();

        if (dontPutUpperPlatform == false)
            for (float y = range.Min + 2/*No in basic line and no two cube over basic line*/; y < range.Max; y++)
            {
                if (Chance(chanceForUpperPlatform))
                {
                    int lenght = UnityEngine.Random.Range(platformsSize.x, platformsSize.y);
                    GenerateOneLine(fromX, lenght, y, dontPutActiveItems,dontMakeEdge);
                    y += lenght;
                }
            }
        blocksPacks.Enqueue(blocks.ToArray());
        return result;
    }

    #endregion


}
