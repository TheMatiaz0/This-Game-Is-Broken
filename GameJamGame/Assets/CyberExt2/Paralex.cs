using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cyberevolver;
using Cyberevolver.Unity;
using UnityEngine;
using System.Collections;
public class Paralex : MonoBehaviourPlus
{
    [SerializeField]
    private Camera cam;
    private Vector2 startPos;

    [DropdownBy(nameof(GetViev))]
    public uint viev;
    private IEnumerable GetViev()
    {
        for (int x = 0; x < 10; x++)
            yield return x;
    }

    private void Start()
    {
        startPos = this.transform.position;
    }
    private void Update()
    {
        this.transform.position = startPos + ((Vector2)(cam.transform.position * viev));
    }



}