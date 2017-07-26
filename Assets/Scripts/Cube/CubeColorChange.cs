using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColorChange : MonoBehaviour {

	private Renderer rend;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        rend = GetComponent<Renderer> ();
		rend.enabled = true;
	}
	
	public void changeColor(Material material) {
		rend.sharedMaterial = material;
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
