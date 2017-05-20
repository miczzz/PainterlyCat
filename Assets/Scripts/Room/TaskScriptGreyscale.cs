using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskScriptGreyscale : MonoBehaviour {

    // das geht sicher effizienter...Array mit Vektor2 Elementen vielleicht?

	public GameObject object1;
	public Material object1WantedColor;
	private Material object1CurrentColor;

    public GameObject object2;
    public Material object2WantedColor;
    private Material object2CurrentColor;

    public GameObject object3;
    public Material object3WantedColor;
    private Material object3CurrentColor;

    public GameObject object4;
    public Material object4WantedColor;
    private Material object4CurrentColor;

    public GameObject object5;
    public Material object5WantedColor;
    private Material object5CurrentColor;



    public GameObject door;

	private bool levelComplete = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!levelComplete) {

			object1CurrentColor = object1.GetComponent<Renderer> ().material;
            object2CurrentColor = object2.GetComponent<Renderer>().material;
            object3CurrentColor = object3.GetComponent<Renderer>().material;
            object4CurrentColor = object4.GetComponent<Renderer>().material;
            object5CurrentColor = object5.GetComponent<Renderer>().material;

            if (object1WantedColor.color == object1CurrentColor.color
                && object2WantedColor.color == object2CurrentColor.color
                && object3WantedColor.color == object3CurrentColor.color
                && object4WantedColor.color == object4CurrentColor.color
                && object5WantedColor.color == object5CurrentColor.color
                ) {
				Debug.Log ("Level complete");
				levelComplete = true;

				Destroy (door);
			}
		}
	}
}
