using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {

	public Canvas PauseMenu;

	public void ReturnToMainScreen() {

		SceneManager.LoadScene (0);
	}


	public void ResumeGame() {
		PauseMenu.enabled = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
		
}
