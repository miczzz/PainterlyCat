using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletChangerMP : NetworkBehaviour {

    public GameObject bullet;
    public Material newBulletColor;
    public GameObject brushhead;
    public GameObject playerBody;
    public Material[] brushColors;
    public GameObject playerInteracting;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Der Paintball ändert die Farbe, wenn der Player die neue Farbe aufnimmt
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            
            //if (!isLocalPlayer)
            //{
            //    Debug.Log("Hello not-Player");
            //    return;
            //}
            playerInteracting = other.transform.gameObject;
            Debug.Log(playerInteracting);
            GameObject brush = other.transform.Find("mylittlebrushcolored").gameObject;
            CmdChangeBulletColor(brush);
        }

    }

    [Command]
    void CmdChangeBulletColor(GameObject brush)
    {
        
        Debug.Log("The bullet changes its color!");
        // Der Umweg über den Brush zum brushhead, da das Prefab als 2nd tier child verschachtelt ist (seufz)
        
        brush.transform.Find("Brushhead").GetComponent<Renderer>().material = newBulletColor;
        // prob not Renderer brushHead = brush.transform.Find("Brushhead").GetComponent<Renderer>();

        Debug.Log(brush.transform.Find("Brushhead"));

        bullet.GetComponent<Renderer>().material = newBulletColor;
        //brushhead.GetComponent<Renderer>().material = newBulletColor;

        int colorPick = ColorFind();

        RpcSetBulletColor(colorPick, playerInteracting);        

    }

    int ColorFind()
    {
        for (int i = 0; i < brushColors.Length; i++)
        {
            if (newBulletColor.Equals(brushColors[i]))
            {
                return i;
            }
        }
        Debug.Log("Error - color not found.");
        return -1;
    }

    // Clients die neue Bulletfarbe übergeben
    [ClientRpc]
    void RpcSetBulletColor(int colorNo, GameObject playerToChange)
    {
        GameObject brush = playerToChange.transform.Find("mylittlebrushcolored").gameObject;
        brush.transform.Find("Brushhead").GetComponent<Renderer>().material = brushColors[colorNo];

        // Nur für den Client
        // transform.Find("PlayerBody").GetComponent<Renderer>().material = newColors[colorNo];
    }


}
