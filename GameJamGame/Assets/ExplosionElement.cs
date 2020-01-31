﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cyberevolver;
using Cyberevolver.Unity;

public class ExplosionElement : MonoBehaviourPlus
{
    protected void Start ()
    {
        StartCoroutine(DestroyAllBugs());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bug bug;
        if((bug=collision.GetComponent<Bug>())!=null)
        {
            bug.Rgb.AddForce((this.transform.position - bug.transform.position).normalized*20);
            LeanTween.alpha(bug.gameObject, 0, 2);
            Debug.Log("Explode");
        }
    }

    private IEnumerator DestroyAllBugs()
    {
        yield return Async.FixedUpdate;
        yield return Async.FixedUpdate;
        Destroy(this.gameObject);
    }
}