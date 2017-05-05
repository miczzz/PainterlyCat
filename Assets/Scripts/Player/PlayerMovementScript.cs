using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

	public float speed = 10.0F;

	// Use this for initialization
	void Start () {
		// make cursor invisble and cursor cant go outside screen
		//Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

		float vertical = Input.GetAxis ("Vertical") * speed;
		float horizonzal = Input.GetAxis ("Horizontal") * speed;
		vertical *= Time.deltaTime;
		horizonzal *= Time.deltaTime;

		transform.Translate (horizonzal, 0, vertical);

		/*
		if (Input.GetKeyDown ("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}
		*/

	}
}
