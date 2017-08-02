using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;

public class MenuScriptLobby : MonoBehaviour {

    public GameObject networkAnnoyance;
    public NetworkManager networkAnnoyance2;
    public GameObject anleitung;
    public GameObject networkParent;

    // OK, schön ist das nicht, aber immerhin verhält es sich endlich wie gewünscht, dass
    // der Network HUD verschwindet wenn man das MP-Menü verlässt und dann wiederkommt, wenn man wieder reingeht
	public void Start() {
        anleitung.SetActive(false);
        networkAnnoyance = FindObjectOfType<NetworkManager>().gameObject;
        networkAnnoyance.SetActive(true);
        networkAnnoyance2.dontDestroyOnLoad = true;
    }

    public void OnEnable()
    {
        networkAnnoyance = FindObjectOfType<NetworkManager>().gameObject;
        networkAnnoyance.SetActive(true);
        networkAnnoyance2.dontDestroyOnLoad = true;
    }

    public void OpenHelpScreen() {
        // toggle Anleitung
        anleitung.SetActive(!anleitung.activeSelf);

        if (networkAnnoyance != null)
        {
            networkAnnoyance.SetActive(!networkAnnoyance.activeSelf);
        }
    }

	public void ReturnToMainScreen() {
       

        networkAnnoyance2.dontDestroyOnLoad = false;

        SceneManager.LoadScene(0);

        //etwas unschön, aber besser als der Bug, dass es sonst gar nicht mehr auftaucht, das LobbyMenü
        if (networkAnnoyance != null)
        {
            networkAnnoyance.SetActive(true);
        }
    }


	public void LoadLevelOne() {
		SceneManager.LoadScene (2);
	}

	public void LoadLevelMultiplayer() {
		SceneManager.LoadScene (1);
	}

	public void QuitGame() {
		Application.Quit ();
	}

}
