﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MPPlayer : NetworkBehaviour {

    public Material[] startingColors;

    public Vector3 cameraOffset;

    public Transform mainCamera;
    public Camera cam;

    public GameObject[] noOfPlayers;
    private bool gameHasBegun = false;

    // Use this for initialization
    void Start()
    {

        //cam = FindObjectOfType<Camera>();
        //mainCamera = cam.transform;
    }
	
	// Update is called once per frame
	void Update () {

        // Damit das Spiel erst beginnt, wenn es (min?) 2 Spieler gibt
        if (!isServer)
        {
            return;
        }

        if (!gameHasBegun)
        {
            Debug.Log("Waiting for players...");
            noOfPlayers = GameObject.FindGameObjectsWithTag("Player");
            if (noOfPlayers.Length >= 2)
            {
                Debug.Log("The game may begin!");
                gameHasBegun = true;
                CmdChangePlayerColors();
            }

        }

    }

    // Server bestimmt die Farben und setzt sie
    [Command]
    void CmdChangePlayerColors()
    {
        if (!isServer)
        {
            return;
        }

        int startColorNo = (int)Random.Range(0.01f, 3.99f);
        Debug.Log(startColorNo);

        // Startfarbe des Players setzen (am besten einmal in einer anderen Klasse)
        
        transform.Find("PlayerBody").GetComponent<Renderer>().material = startingColors[startColorNo];

        // Startfarbe des Brushes setzen (gleiche wie Playerfarbe)
        GameObject brush = transform.Find("mylittlebrushcolored").gameObject;
        brush.transform.Find("Brushhead").GetComponent<Renderer>().material = startingColors[startColorNo];

        RpcSetPlayerColors(startColorNo);
    }

    // Farben werden an den Client übergeben
    [ClientRpc]
    void RpcSetPlayerColors(int colorNo)
    {
        // Wurden vom Server schon gesetzt
        if (isServer)
        {
            return;
        }

        // Nur für den Client
        transform.Find("PlayerBody").GetComponent<Renderer>().material = startingColors[colorNo];
        GameObject brush = transform.Find("mylittlebrushcolored").gameObject;
        brush.transform.Find("Brushhead").GetComponent<Renderer>().material = startingColors[colorNo];
    }

    //void MoveCamera()
    //{
    //    mainCamera.position = transform.position;
    //    mainCamera.rotation = transform.rotation;
    //    mainCamera.Translate(cameraOffset);
    //    mainCamera.LookAt(transform); // to make look at player soo...actually player.transform or sth depending on where this script sits
    //}

}
