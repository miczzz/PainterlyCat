using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;

public class MenuScript : MonoBehaviour {

	public Canvas MainMenu;
    
	public Canvas HelpMenu;
    public NetworkManagerHUD networkGUI;
    public NetworkLobbyManager networkLobby;
    public GameObject networkStuff;

    public void Awake() {
	
		HelpMenu.enabled = false;
        
    }

    public void Update()
    {
        networkLobby = FindObjectOfType<NetworkLobbyManager>();
        networkGUI = FindObjectOfType<NetworkManagerHUD>();
        if (networkGUI != null)
        {
            Destroy(networkGUI);
        }
        if(networkLobby != null)
        {
            Destroy(networkLobby);
        }
    }

	public void OpenHelpScreen() {
	
		HelpMenu.enabled = true;
		MainMenu.enabled = false;
	}

	public void ReturnToMainScreen() {
	
		HelpMenu.enabled = false;
		MainMenu.enabled = true;
	}


	public void LoadLevelOne() {
		SceneManager.LoadScene (3);
	}

	public void LoadLevelMultiplayer() {
        SceneManager.LoadScene (1);
	}

	public void QuitGame() {
		Application.Quit ();
	}

}
