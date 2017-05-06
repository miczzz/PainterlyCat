using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskScript : MonoBehaviour {

	public GameObject object1;
	public Material object1WantedColor;
	private Material object1CurrentColor;

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
				Debug.Log ("Level complete");
				levelComplete = true;

				Destroy (door);
			}
		}
	}
}
