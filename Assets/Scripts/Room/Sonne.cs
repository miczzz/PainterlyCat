using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Sonne : MonoBehaviour {

    public Material sonnenFarbe;
    public GameObject licht;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.GetComponent<Renderer>().material.color == sonnenFarbe.color)
        {
            licht.SetActive(true);
        }

    }
}
