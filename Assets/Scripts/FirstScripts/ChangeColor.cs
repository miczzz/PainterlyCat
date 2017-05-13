using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {
    private Material newColor;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    // Wenn das Objekt vom Paintball getroffen wird übernimmt es die Farbes des Paintballs
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Paintball"))
        {
            // die Farbe holen vom Paintball
            newColor = other.GetComponent<Renderer>().material;
            Debug.Log(newColor);
            // die Farbe dem Objekt übergeben (wird angemalt)
            GetComponent<Renderer>().material = newColor;
            Debug.Log("The cube (or whatever) changes its color!");
        }

    }
}
