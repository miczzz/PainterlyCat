using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;

public class MenuScript : MonoBehaviour {

	public Canvas MainMenu;
    
	public Canvas HelpMenu;
    public Canvas levelMenu;
    public NetworkManagerHUD networkGUI;
    public NetworkLobbyManager networkLobby;
    public GameObject networkStuff;

    public void Awake() {
	
		HelpMenu.enabled = false;
        levelMenu.enabled = false;

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
        levelMenu.enabled = false;
        HelpMenu.enabled = false;
		MainMenu.enabled = true;
	}


	public void LoadLevelOne() {

        if (PlayerPrefs.HasKey("LevelProgress"))
        {
            MainMenu.enabled = false;
            levelMenu.enabled = true;
        }
        else
        {
            SceneManager.LoadScene(3);
        }
	}

    public void Weiterspielen()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelProgress"));       
    }

    public void VonVornAnfangen()
    {
		PlayerPrefs.DeleteKey("LevelProgress");
        SceneManager.LoadScene(3);       
    }

	public void LoadLevelMultiplayer() {
        SceneManager.LoadScene (1);
	}

	public void QuitGame() {
		Application.Quit ();
	}

}
