using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintPotScript : MonoBehaviour {

	private Renderer rend;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        rend = GetComponent<Renderer> ();
		rend.enabled = true;
       
	}
	
	// Update is called once per frame
	public Material getColor() {
        if (audioSource != null)
        {
            audioSource.Play();
        }

        if (rend.material.name != null) {
			return rend.material;
		}

        Debug.Log ("Paint Pot has no color!");
		return null;
	}
}
