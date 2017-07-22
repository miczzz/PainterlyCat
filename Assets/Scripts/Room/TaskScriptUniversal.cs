﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskScriptUniversal : MonoBehaviour {

    private AudioSource audioSource;
    public GameObject[] objects;
	public Material[] objectWantedColors;
	public Material[] objectCurrentColors;
    public GameObject door;
    public int correctColors;

	private bool levelComplete = false;

	void Awake () {

        audioSource = GetComponent<AudioSource>();

        // aktuelle Farben zuweisen
        //for (int i = 0; i < objects.Length; i++)
        //{
        //    objectCurrentColors[i] = objects[i].GetComponent<Renderer>().material;
            
        //}

    }
	
	// Update is called once per frame
	void Update () {
        if (!levelComplete) {

            // aktuelle Farben checken
            getCurrentColors();

            // aktuelle Farben mit gesuchten Farben vergleichen

            for (int i = 0; i < objects.Length; i++)
            {
                if (objectWantedColors[i].color == objectCurrentColors[i].color)
                {
                    correctColors++;
                    
                    // hat alle gecheckt und alles war wie gewünscht
                    if (correctColors == objects.Length)
                    {

                        Debug.Log("Level complete");
                        levelComplete = true;

                        Destroy(door);
                        audioSource.Play();
                    }
                }
                else
                {
                    correctColors = 0;
                }
            }
                
		}
	}

    void getCurrentColors()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objectCurrentColors[i] = objects[i].GetComponent<Renderer>().material;

        }
    }
}
