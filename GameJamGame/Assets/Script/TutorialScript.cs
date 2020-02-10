using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//watch this first: https://www.youtube.com/watch?v=0VGosgaoTsw

public class TutorialScript : MonoBehaviour
{
    public float tutorialSpeed;

    public GameObject myObject;

       void Start()
    {
        //tutorialSpeed = Time.timeScale;
}

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = tutorialSpeed;
        

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
