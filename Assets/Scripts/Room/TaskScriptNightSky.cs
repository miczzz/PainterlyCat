﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskScriptNightSky : MonoBehaviour {

	public GameObject object1;
	public Material object1WantedColor;
	private Material object1CurrentColor;

	public GameObject object2;
	public Material object2WantedColor;
	private Material object2CurrentColor;

	public GameObject object3;
	public Material object3WantedColor;
	private Material object3CurrentColor;

	public GameObject door;

	private bool levelComplete = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!levelComplete) {

			object1CurrentColor = object1.GetComponent<Renderer> ().material;

			if (object1WantedColor.color == object1CurrentColor.color) {

				object2CurrentColor = object2.GetComponent<Renderer> ().material;

				if (object2WantedColor.color == object2CurrentColor.color) {

					object3CurrentColor = object3.GetComponent<Renderer> ().material;

					if (object3WantedColor.color == object3CurrentColor.color) {

						Debug.Log ("Level complete");

						levelComplete = true;

						Destroy (door);
					}
				}
			}
		}
	}
}
