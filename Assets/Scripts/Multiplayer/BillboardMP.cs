using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardMP : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // Grafik schaut in die gleiche Richtung wie die Kamera
        transform.rotation = Quaternion.Slerp(transform.rotation, Camera.main.transform.rotation, 0.5f);
    }
}
