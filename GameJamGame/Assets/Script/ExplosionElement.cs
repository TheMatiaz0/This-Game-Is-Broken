using Cyberevolver;
using Cyberevolver.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ExplosionElement : MonoBehaviourPlus
{


    private bool shouldStartBeDestroyed;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bug bug;
        if ((bug = collision.GetComponent<Bug>()) != null)
        {
            bug.Rgb.bodyType = RigidbodyType2D.Dynamic;
            bug.GetComponent<Collider2D>().enabled = false;
            bug.Rgb.AddForce((Vector2)(bug.transform.position - this.transform.position).normalized * 300
                + Vector2.up * 150);
            LeanTween.alpha(bug.gameObject, 0, 2).setOnComplete(() => Destroy(bug.gameObject));

            shouldStartBeDestroyed = true;
            Debug.Log("Explode");
        }
    }
    private void Update()
    {
        if (shouldStartBeDestroyed)
            Destroy(this.gameObject);
    }


}