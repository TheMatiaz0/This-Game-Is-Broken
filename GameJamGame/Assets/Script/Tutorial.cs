using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//watch this first: https://www.youtube.com/watch?v=0VGosgaoTsw

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private float tutorialSpeed;
    [SerializeField]
    private GameObject myObject;

    protected virtual void Start()
    {
        //tutorialSpeed = Time.timeScale;
    }

    protected virtual void Update()
    {
        Time.timeScale = tutorialSpeed;
        Time.fixedDeltaTime = Time.timeScale * 0.2f;
    }
    protected virtual void TutorialSlowmotion()
    {
        if (Input.GetKeyDown("return"))
        {
            tutorialSpeed = 1f;
            myObject.SetActive(false);
        }
        else
        {
            myObject.SetActive(true);
            tutorialSpeed = 0.1f;
        }
    }
}