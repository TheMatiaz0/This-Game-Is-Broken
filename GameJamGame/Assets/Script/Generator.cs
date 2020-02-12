using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Generator : AutoInstanceBehaviour<Generator>
{

    #region CONST
    private const string Raw = "Values";
    private const string PrefabsName = "Prefabs";
    private const string Transform = "Transform";
    #endregion
    #region ENUMS
    public enum BlockMode
    {
        Left,
        Right,
        Center
    }
    [Flags]
    public enum DrawLineFlags
    {
        None = 0,
        DontPutActiveItems = 1 << 0,
        DontMakeEdge = 1 << 1,
        Flip = 1 << 2,
        DontMakeHoles = 1 << 3,
    }
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

    //Raw
    [SerializeField, BoxGroup(Raw), Range(0, 99)]
    private int howFastGenerate = 3;
    [SerializeField, BoxGroup(Raw)]
    private Percent chanceForUpperPlatform = 0.25f;
    [SerializeField, BoxGroup(Raw)]
    private Percent chanceForAnyActiveItems = 0.33f;
    [SerializeField, BoxGroup(Raw), Range(1, 100)]
    private int howOftenCanHaveItem = 2;
    [SerializeField, BoxGroup(Raw), Range(1, 100)]
    private int blockInOneShoot = 10;
    [SerializeField, BoxGroup(Raw), Range(1, 10)]
    private int whenRemove = 3;
    [SerializeField, BoxGroup(Raw)]
    private bool makeHole = true;
    [SerializeField, BoxGroup(Raw), ShowWhen(nameof(makeHole))]
    private Percent chanceForHole = 0.25f;
    [SerializeField, BoxGroup(Raw), MinMaxRange(0, 10), ShowWhen(nameof(makeHole))]
    private uint minHoleBreak = 2;

    [SerializeField, BoxGroup(Raw), MinMaxSlider(1, 10)]
    private Vector2Int platformsSize = new Vector2Int(2, 6);
    [HelpBox("Value should be lower or equals than 1 time unit = 100 metres", MessageType.Info, UISize.Default)]
    [SerializeField, BoxGroup(Raw)]
    private AnimationCurve elementAmountGrowing = null;


    //Prefabs
    [SerializeField, BoxGroup(PrefabsName), RequiresAny]
    private GameObject blockPrefab = null;
    [SerializeField, BoxGroup(PrefabsName), ArrayAsField("LeftEdge", "RightEdge")]
    private Sprite[] edges = null;

    //Transforms

    [SerializeField, BoxGroup(Transform), RequiresAny]
    private Transform startRespPoint = null;

    [SerializeField, BoxGroup(Transform), RequiresAny]
    private Transform maxUp = null;

    #endregion

    #region PROPERTIES&FIELDS

    private readonly Queue<GameObject[]> blocksPacks = new Queue<GameObject[]>();
    private float lastX = 0;
    public uint PutedBlocksQuanity { get; private set; } = 0;
    private Range YRange => new Range(startRespPoint.position.y, maxUp.position.y);
    #endregion

    #region METHODS


    public Percent GetFinalChanceForAnyActiveItems()
    {
        if (elementAmountGrowing.length == 0)
            return chanceForAnyActiveItems;

        float currentTime = (DistanceManager.Instance.GetMeters() / 100F);
        return chanceForAnyActiveItems + (Percent.Full - chanceForAnyActiveItems) * (Percent)elementAmountGrowing.Evaluate(currentTime);
    }
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
       DrawLineFlags drawLineFlags)
    {
        GameObject[] objs = new GameObject[10];
        Cint cooldown = 0;
        bool[] holes = new bool[(int)blocks + 1];

        if (drawLineFlags.HasFlag(DrawLineFlags.DontMakeHoles) == false && makeHole && blocks >= 3)
            for (int x = 2; x < blocks - 2; x++)
            {
                if (cooldown == 0 && Chance(chanceForHole))
                {
                    holes[x] = true;
                    cooldown = minHoleBreak;
                }
                else
                    cooldown--;
            }

        for (int x = 0; x < blocks; x++)
        {

            BlockMode mode = BlockMode.Center;
            bool dontPut = drawLineFlags.HasFlag(DrawLineFlags.DontPutActiveItems);
            bool anyMode = false;
            if (drawLineFlags.HasFlag(DrawLineFlags.DontMakeEdge) == false)
            {
                anyMode = true;
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
                else
                {
                    anyMode = false;
                }

            }
            if (anyMode == false)
            {
                if (holes[x + 1])
                {
                    mode = BlockMode.Right;
                }
                else if (x > 0 && holes[x - 1])
                {
                    mode = BlockMode.Left;
                }
            }

            if (holes[x] == false)
            {
                GameObject block;
                objs[x] = block = PutBlock(new Vector2(fromX + x, y), dontPut, mode);
                if (drawLineFlags.HasFlag(DrawLineFlags.Flip))
                {
                    block.GetComponent<SpriteRenderer>().flipY = true;
                }
            }

        }
        blocksPacks.Enqueue(objs);
        return fromX + blockInOneShoot;
    }

    public GameObject GetRandomPrefabStuff()
    {
        PutedBlocksQuanity++;
        if (findablePrefabs.Count == 0 || PutedBlocksQuanity % howOftenCanHaveItem != 0)
        {
            return null;
        }
        List<int> acceptalbe = Enumerable.Range(0, findablePrefabs.Count).ToList();
        while (acceptalbe.Count > 0)
        {
            int x = UnityEngine.Random.Range(0, acceptalbe.Count);
            if (Chance(findablePrefabs[acceptalbe[x]].howOften))
            {
                return findablePrefabs[acceptalbe[x]].prefab;
            }
            else
                acceptalbe.RemoveAt(x);
        }
        return null;


    }

    private bool Chance(Percent percent)
    {
        return UnityEngine.Random.Range(0, 1f) <= percent.AsFloatValue;

    }

    public GameObject PutBlock(Vector2 pos, bool dontPutActiveItems = false, BlockMode mode = BlockMode.Center)
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

        if (dontPutActiveItems == false && Chance(GetFinalChanceForAnyActiveItems()))
        {
            GameObject prefab = GetRandomPrefabStuff();
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

    public float GenerateChunk(float fromX, Range range, bool dontPutActiveItems = false, bool dontPutUpperPlatform = false, bool dontMakeEdge = false)
    {
        float result = GenerateOneLine(fromX, blockInOneShoot, startRespPoint.position.y, DrawLineFlags.DontMakeEdge | ((dontPutActiveItems) ? DrawLineFlags.DontPutActiveItems : DrawLineFlags.None));
        GenerateOneLine(fromX, blockInOneShoot, startRespPoint.position.y + maxUp.position.y + 5, DrawLineFlags.DontPutActiveItems | DrawLineFlags.DontMakeEdge | DrawLineFlags.Flip | DrawLineFlags.DontMakeHoles);

        List<GameObject> blocks = new List<GameObject>();

        if (dontPutUpperPlatform == false)
            for (float y = range.Min + 2/*No in basic line and no two cube over basic line*/; y < range.Max; y++)
            {
                if (Chance(chanceForUpperPlatform))
                {
                    DrawLineFlags flags = DrawLineFlags.None;
                    if (dontPutActiveItems)
                        flags |= DrawLineFlags.DontPutActiveItems;
                    if (dontMakeEdge)
                        flags |= DrawLineFlags.DontMakeEdge;
                    int lenght = UnityEngine.Random.Range(platformsSize.x, platformsSize.y);
                    GenerateOneLine(fromX, lenght, y, flags);
                    y += lenght;
                }
            }
        blocksPacks.Enqueue(blocks.ToArray());
        return result;
    }

    #endregion
}
