using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MPMenuScript : NetworkBehaviour {

	public GameObject rematchMenu;
    public bool enableMe;
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public MPPlayer otherPlayer;
    public Transform serverPlayerPlace;
    public Transform clientPlayerPlace;
    public NetworkConnection connectionServer;
    public NetworkConnection connectionClient;
    public HealthMP serverHealthMP;
    public HealthMP clientHealthMP;

    [SyncVar]
    public bool alreadyPlayer1Spawned;

    public GameObject playerPrefab;
    public Text gameOverText;

    public GameObject[] players;
    public GameObject rematchButton;

    [SyncVar]
    public GameObject player1;

    //Client Player
    [SyncVar]
    public GameObject player2;

    public void Start()
    {
        
        alreadyPlayer1Spawned = false;

        otherPlayer = FindObjectOfType<MPPlayer>();

        

        if (isServer)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            Debug.Log(players.Length);
            player1 = players[0]; // wird wohl nach Adam Riese der Server sein...?!

            if (players.Length >= 2)
            {
                player2 = players[1];
            }
        }

        serverPlayerPlace = otherPlayer.player1.transform;
        
        if(players.Length >= 2)
        {
            clientPlayerPlace = otherPlayer.player2.transform;
        }

    }

  



	public void ReturnToMainScreen() {

		SceneManager.LoadScene (0);
	}

    
    public void Rematch()
    {
        rematchButton.SetActive(false);
        if (!isServer)
        {
            return;
        }
        CmdRematch();
    }

    //[ClientRpc]
    //public void RpcRematch()
    //{
       
    //    if (isServer)
    //    {
    //        player1.SetActive(true);
    //        player1.transform.position = spawnPoint1.transform.position;
    //        player1.transform.rotation = spawnPoint1.transform.rotation;
    //        HealthMP serverHealthMP = player1.GetComponentInParent<HealthMP>();
    //        // keine Ahnung warum ich auf maxHealth so nicht zugreifen kann, aber was solls ne...
    //        serverHealthMP.ResetHealth();
    //    }
    //    else
    //    {
    //        CmdRematch();
    //    }

    //}

    [Command]
    public void CmdRematch()
    {
        FindPlayers();

        gameOverText.text = "";
        player2.transform.position = spawnPoint2.transform.position;
        player2.transform.rotation = spawnPoint2.transform.rotation;
        clientHealthMP = player1.GetComponentInParent<HealthMP>();
        player1.transform.position = spawnPoint1.transform.position;
        player1.transform.rotation = spawnPoint1.transform.rotation;
        serverHealthMP = player2.GetComponentInParent<HealthMP>();
 
        clientHealthMP.ResetHealth();
        serverHealthMP.ResetHealth();

        RpcRematch();
    }

    [ClientRpc]
    public void RpcRematch()
    {
        if (isServer)
        {
            return;
        }
        FindPlayers();
        gameOverText.text = "";
        player2.transform.position = spawnPoint2.transform.position;
        player2.transform.rotation = spawnPoint2.transform.rotation;
        clientHealthMP = player1.GetComponentInParent<HealthMP>();
        player1.transform.position = spawnPoint1.transform.position;
        player1.transform.rotation = spawnPoint1.transform.rotation;
        serverHealthMP = player2.GetComponentInParent<HealthMP>();

        clientHealthMP.ResetHealth();
        serverHealthMP.ResetHealth();

        rematchButton.SetActive(false);
    }

    public void FindPlayers()
    {
        // hm?
        //otherPlayer = FindObjectOfType<MPPlayer>();

        //if (isServer)        {
            players = GameObject.FindGameObjectsWithTag("Player");
            player1 = players[0]; // wird wohl nach Adam Riese der Server sein...?!

            if (players.Length >= 2)
            {
                player2 = players[1];
            }
            else
            {
                player2 = otherPlayer.GetComponentInParent<GameObject>();
            }
        //}
    }

    // SPAWNPOINTS
    // GameObject newPlayer = Instantiate(playerPrefab, spawnPoint2.transform.position, spawnPoint2.transform.rotation);



    public void EnableButton()
    {
        rematchButton.SetActive(true);
    }



    public void ResumeGame() {
		//pauseMenu.enabled = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void SkipLevel() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

    [ClientRpc]
    public void RpcRestartLevel() {
		
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}



		
}
