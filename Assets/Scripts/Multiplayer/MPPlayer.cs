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
    public GameObject[] players;
    private bool gameHasBegun = false;

    [SyncVar]
    public int numberOfPlayers;

    [SyncVar]
    public int startColorNo;

    [SyncVar]
    public int startColorNo2;

    //Server Player
    [SyncVar]
    public GameObject player1;

    //Client Player
    [SyncVar]
    public GameObject player2;

    public NetworkConnection player1connection;
    public NetworkConnection player2connection;

    // Use this for initialization
    void Start()
    {
        numberOfPlayers++;
        //Debug.Log(numberOfPlayers);

        if (isServer)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            player1 = players[0]; // wird wohl nach Adam Riese der Server sein...?!

            //player1connection = gameObject.GetComponent<MPPlayer>().connectionToClient;
            // Ist ja der Server...
            player1connection = connectionToClient;

            if (players.Length >= 2)
            {
                player2 = players[1];
               
            }
        }
        else
        {
            player2connection = connectionToClient;
            SetPlayerColors(startColorNo, startColorNo2);
        }
    }
	
	// Update is called once per frame
	void Update () {

        noOfPlayers = GameObject.FindGameObjectsWithTag("Player");
        
        numberOfPlayers = noOfPlayers.Length;
        //Debug.Log(noOfPlayers.Length);
        // Damit das Spiel erst beginnt, wenn es (min?) 2 Spieler gibt
        if (!isServer)
        {
            return;
        }

        if (!gameHasBegun)
        {
            Debug.Log("Waiting for players...");
            noOfPlayers = GameObject.FindGameObjectsWithTag("Player");

            if (numberOfPlayers == 2)
            {
                Debug.Log("The game may begin!");
                gameHasBegun = true;
                CmdChangePlayerColors();
            }

        }

        //if(gameHasBegun && noOfPlayers.Length < 2)
        //{
        //    GameObject.FindWithTag("RematchMenu").SetActive(true);
        //    GameObject.FindWithTag("RematchMenu").GetComponent<Canvas>().enabled = true;
        //}

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
