using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private GameObject childMainMenu;

	public void StartGame ()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void Options (GameObject optionsMenu, bool shouldOpenOptions)
	{
		optionsMenu.SetActive(shouldOpenOptions);
		childMainMenu.SetActive(!shouldOpenOptions);
	}

	public void Quit ()
	{
		Application.Quit(0);
	}
}
