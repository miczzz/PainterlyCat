using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletChangerMP : NetworkBehaviour {

    public GameObject bullet;
    public Material newBulletColor;
    public GameObject brushhead;
    public GameObject playerBody;

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
            Debug.Log("The bullet changes its color!");
            bullet.GetComponent<Renderer>().material = newBulletColor;
            brushhead.GetComponent<Renderer>().material = newBulletColor;
            CmdChangeBodyColor();

            // Für später wenn die Farben geändert werden nachdem man getroffen wurde und am Anfang
            other.transform.Find("PlayerBody").GetComponent<Renderer>().material = newBulletColor;

        }

    }

    [Command]
    private void CmdChangeBodyColor()
    {
       // playerBody.GetComponent<Renderer>().material = newBulletColor;

    }

}
