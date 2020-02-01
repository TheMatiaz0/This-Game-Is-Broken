using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Generator : MonoBehaviour
{




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

    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private Percent chanceForBlankBlock = 0.15f;
    [SerializeField]
    private Transform startRespPoint;
    [SerializeField]
    private Transform maxUp;

    [SerializeField]
    private Transform minUp;

    [SerializeField]
    [Range(0, 9)]
    private int howFastGenerate = 3;
    [SerializeField]
    [Range(1, 100)]

    private int blockInOneShoot = 10;
    [SerializeField]
    [Range(1, 10)]
    private int whenRemove = 3;

    private Range YRange => new Range(startRespPoint.position.y, maxUp.position.y);
    private Queue<GameObject[]> blocksPacks = new Queue<GameObject[]>();
    private float lastX = 0;

    public float GenerateOneLine(float fromX)
    {
        GameObject[] objs = new GameObject[10];
        for (int x = 0; x < blockInOneShoot; x++)
        {
            objs[x] = PutBlock(new Vector2(fromX + x, startRespPoint.position.y));

        }
        blocksPacks.Enqueue(objs);
        return fromX + blockInOneShoot;

    }
    public GameObject GetRandomStuff()
    {
        if (findablePrefabs.Count == 0)
            return null;

        foreach (var item in findablePrefabs)
        {
            if (Chance(item.howOften))
            {
                return item.prefab;
            }
        }

        

        // float max = findablePrefabs.Sum(item => item.howOften.AsFloatValue);
        // float val = UnityEngine.Random.Range(0, max);
        // float full = 0;
        /*
        for (int x = 0; x <= val; x++)
        {
            if (full <= val)
                return findablePrefabs[x].prefab;
            full += findablePrefabs[x].howOften.AsFloatValue;
        }
        */
        return null;

    }

    private bool Chance (Percent percent)
    {
        byte target = percent.AsProcentValue;
        int rndValue = UnityEngine.Random.Range(1, 101);

        if ((rndValue >= 1 && rndValue <= target))
        {
            return false;
        }

        else
        {
            return true;
        }
    }

    public GameObject PutBlock(Vector2 pos)
    {
        if (Chance(chanceForBlankBlock) == false)
        {
            // puts blank block with another one underneath
            return PutBlock(new Vector2(pos.x, minUp.position.y - pos.y));
        }

        var block = Instantiate(blockPrefab);
        block.transform.position = pos;
        if (UnityEngine.Random.Range(0, 4) == 0)
        {
            var prefab = GetRandomStuff();
            if (prefab != null)
            {
                GameObject item = Instantiate(prefab);
                item.transform.position = (Vector2)block.transform.position + Vector2.up;
            }

        }
        return block;
    }
    public float GenerateChunk(float fromX, Range range)
    {
        float result = GenerateOneLine(fromX);
        List<Vector2> busy = new List<Vector2>();
        List<GameObject> blocks = new List<GameObject>();
        for (float y = range.Min + 2/*No in basic line and no one cube over basic line*/; y < range.Max; y++)
        {
            if (UnityEngine.Random.Range(0, 3) == 0)
            {
                int lenght = UnityEngine.Random.Range(1, 6);
                for (float x = fromX; x < fromX + lenght; x++)
                {
                    Vector2 pos = new Vector2(x, y);
                    if (busy.Contains(pos))
                        break;
                    else
                    {
                        blocks.Add(PutBlock(pos));
                        busy.Add(pos);
                    }
                }
                y += lenght;
            }
        }
        blocksPacks.Enqueue(blocks.ToArray());
        return result;
    }
    private void Start()
    {

        lastX = GenerateOneLine(startRespPoint.position.x);
        lastX = GenerateChunk(lastX, YRange);

    }
    private void Update()
    {
        if (PlayerController.Instance.transform.position.x > (lastX) - howFastGenerate)
        {

            if (blocksPacks.Count >= whenRemove)
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
