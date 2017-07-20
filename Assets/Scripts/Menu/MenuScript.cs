using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour {

	public Canvas MainMenu;

	public Canvas HelpMenu;

	public void Awake() {
	
		HelpMenu.enabled = false;

	}

	public void OpenHelpScreen() {
	
		HelpMenu.enabled = true;
		MainMenu.enabled = false;
	}

	public void ReturnToMainScreen() {
	
		HelpMenu.enabled = false;
		MainMenu.enabled = true;
	}


	public void LoadLevelOne() {
		SceneManager.LoadScene (2);
	}

	public void LoadLevelMultiplayer() {
		SceneManager.LoadScene (1);
	}

	public void QuitGame() {
		Application.Quit ();
	}

}
