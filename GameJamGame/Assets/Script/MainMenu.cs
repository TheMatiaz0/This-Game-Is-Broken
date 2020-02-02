
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject childMainMenu;

    public void StartGame()
    {
        SceneManager.LoadScene("B");
    }

    public void OpenOptions(GameObject optionsObject) => OptionsActivation(optionsObject, true);

    private void OptionsActivation(GameObject optionsObject, bool areYouSure)
    {
        optionsObject.SetActive(areYouSure);
        childMainMenu.SetActive(!areYouSure);
    }

    public void CloseOptions(GameObject optionsObject) => OptionsActivation(optionsObject, false);

    public void Quit()
    {
        Application.Quit(0);
    }
}