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
            // Der Umweg über den Brush zum brushhead, da das Prefab als 2nd tier child verschachtelt ist (seufz)
            GameObject brush = other.transform.Find("mylittlebrushcolored").gameObject;
            brush.transform.Find("Brushhead").GetComponent<Renderer>().material = newBulletColor;
            Debug.Log(brush.transform.Find("Brushhead"));
           // other.transform.Find("ShootScript").GetComponent<Renderer>().material = newBulletColor;
            bullet.GetComponent<Renderer>().material = newBulletColor;
            brushhead.GetComponent<Renderer>().material = newBulletColor;


            // Für später wenn die Farben geändert werden nachdem man getroffen wurde und am Anfang
            other.transform.Find("PlayerBody").GetComponent<Renderer>().material = newBulletColor;

        }

    }


}
