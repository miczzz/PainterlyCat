using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TeleportToNextLevelScript : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{


		int scene = SceneManager.GetActiveScene ().buildIndex;
	
	
			SceneManager.LoadScene (scene + 1);
		
	}
}
