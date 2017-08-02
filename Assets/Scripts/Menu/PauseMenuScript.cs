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
        FindObjectOfType<CameraMouseMovement>().mouseEnabled = true;
        Cursor.lockState = CursorLockMode.Locked;
	}

	public void SkipLevel() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void RestartLevel() {
		
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
		
}
