using System.Collections;
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

    [SyncVar]
    public int numberOfPlayers;

    [SyncVar]
    public int startColorNo;

    [SyncVar]
    public int startColorNo2;

    // Use this for initialization
    void Start()
    {
        numberOfPlayers++;
        Debug.Log(numberOfPlayers);

        if (isServer)
        {
            return;
        }
        else
        {
            SetPlayerColors(startColorNo, startColorNo2);
        }
    }
	
	// Update is called once per frame
	void Update () {

        noOfPlayers = GameObject.FindGameObjectsWithTag("Player");
        numberOfPlayers = noOfPlayers.Length;
        Debug.Log(noOfPlayers.Length);
        // Damit das Spiel erst beginnt, wenn es (min?) 2 Spieler gibt
        if (!isServer)
        {
            return;
        }

        if (!gameHasBegun)
        {
            Debug.Log("Waiting for players...");
            noOfPlayers = GameObject.FindGameObjectsWithTag("Player");
            //if (noOfPlayers.Length >= 2)
            if (numberOfPlayers == 2)
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

        startColorNo = (int)Random.Range(0.01f, 3.99f);
        startColorNo2 = (int)Random.Range(0.01f, 3.99f);

        if (isLocalPlayer)
        {           
            Debug.Log(startColorNo);
            // Startfarbe des Players setzen (am besten einmal in einer anderen Klasse)
            transform.Find("PlayerBody").GetComponent<Renderer>().material = startingColors[startColorNo];
            // Startfarbe des Brushes setzen (gleiche wie Playerfarbe)
            GameObject brush = transform.Find("mylittlebrushcolored").gameObject;
            brush.transform.Find("Brushhead").GetComponent<Renderer>().material = startingColors[startColorNo];
            
        } else
        {            
            Debug.Log(startColorNo2);
            // Startfarbe des Players setzen (am besten einmal in einer anderen Klasse)
            transform.Find("PlayerBody").GetComponent<Renderer>().material = startingColors[startColorNo2];
            // Startfarbe des Brushes setzen (gleiche wie Playerfarbe)
            GameObject brush = transform.Find("mylittlebrushcolored").gameObject;
            brush.transform.Find("Brushhead").GetComponent<Renderer>().material = startingColors[startColorNo2];
        }

        RpcSetPlayerColors(startColorNo, startColorNo2);

    }

    // Farben werden an den Client übergeben
    [ClientRpc]
    void RpcSetPlayerColors(int colorNo, int colorNo2)
    {
        // Wurden vom Server schon gesetzt
        if (isServer)
        {
            return;
        }

        // Nur für den Client

        if (!isLocalPlayer)
        {
            transform.Find("PlayerBody").GetComponent<Renderer>().material = startingColors[colorNo];
            GameObject brush = transform.Find("mylittlebrushcolored").gameObject;
            brush.transform.Find("Brushhead").GetComponent<Renderer>().material = startingColors[colorNo];
        }
        else
        {
            transform.Find("PlayerBody").GetComponent<Renderer>().material = startingColors[colorNo2];
            GameObject brush = transform.Find("mylittlebrushcolored").gameObject;
            brush.transform.Find("Brushhead").GetComponent<Renderer>().material = startingColors[colorNo2];
        }
    }

    // nochmal extra, da es sonst zu einem Bug kam, wo der Client die Startfarben nicht gesetzt hat, wenn er
    // etwas langsamer als der Server war ...
    void SetPlayerColors(int colorNo, int colorNo2)
    {
        // Wurden vom Server schon gesetzt
        if (isServer)
        {
            return;
        }

        // Nur für den Client

        if (!isLocalPlayer)
        {
            transform.Find("PlayerBody").GetComponent<Renderer>().material = startingColors[colorNo];
            GameObject brush = transform.Find("mylittlebrushcolored").gameObject;
            brush.transform.Find("Brushhead").GetComponent<Renderer>().material = startingColors[colorNo];
        }
        else
        {
            transform.Find("PlayerBody").GetComponent<Renderer>().material = startingColors[colorNo2];
            GameObject brush = transform.Find("mylittlebrushcolored").gameObject;
            brush.transform.Find("Brushhead").GetComponent<Renderer>().material = startingColors[colorNo2];
        }
    }


}
