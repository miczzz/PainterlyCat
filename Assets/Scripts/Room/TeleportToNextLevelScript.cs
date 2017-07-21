using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TeleportToNextLevelScript : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{


		int scene = SceneManager.GetActiveScene ().buildIndex;
	
	
		if (SceneManager.sceneCountInBuildSettings > scene + 3)
			SceneManager.LoadScene (scene + 1);
		else {
			Debug.Log ("no more levels available");
		}
	}
}
