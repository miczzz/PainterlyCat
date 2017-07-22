using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScriptLobby : MonoBehaviour {

	public Canvas MainMenu;

	public Canvas HelpMenu;

	public void Awake() {
	
		//HelpMenu.enabled = false;

	}

	public void OpenHelpScreen() {
	
		HelpMenu.enabled = true;
		MainMenu.enabled = false;
	}

	public void ReturnToMainScreen() {

        SceneManager.LoadScene(0);
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
