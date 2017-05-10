using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MPPlayer : NetworkBehaviour {

    public Material[] startingColors;

    public Vector3 cameraOffset;

    public Transform mainCamera;
    public GameObject[] noOfPlayers;
    private bool gameHasBegun = false;

    private int eins;
    private int zwei;
    private int nullo;
    private int drei;

    // Use this for initialization
    void Start () {
        //transform.Find("PlayerBody").GetComponent<Renderer>().material = startingColor1;

        //for (int i = 0; i < 100; i++)
        //{
        //    startColorNo = (int)Random.Range(0.01f, 3.99f);
        //    if (startColorNo == 0)
        //    {
        //        nullo++;
        //    }
        //    if (startColorNo == 1)
        //    {
        //        eins++;
        //    }
        //    if(startColorNo == 2)
        //    {
        //        zwei++;
        //    }
        //    if (startColorNo == 3)
        //    {
        //        drei++;
        //    }
        //}

        Debug.Log("0: " + nullo + " 1: " + eins + " 2 " + zwei + " 3 " + drei);

    }
	
	// Update is called once per frame
	void Update () {

        // Damit das SPiel erst beginnt, wenn es (min?) 2 Spieler gibt
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

        // Startfarbe des Players setzen ***MUSS NOCH AUF DEM SERVER GEMACHT WERDEN - am besten einmal in einer anderen Klasse***

        transform.Find("PlayerBody").GetComponent<Renderer>().material = startingColors[startColorNo];
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
    }

    //void MoveCamera()
    //{
    //    mainCamera.position = transform.position;
    //    mainCamera.rotation = transform.rotation;
    //    mainCamera.Translate(cameraOffset);
    //    mainCamera.LookAt(transform); // to make look at player soo...actually player.transform or sth depending on where this script sits
    //}

}
