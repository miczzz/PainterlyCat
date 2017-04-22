using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletChanger : MonoBehaviour {

    public GameObject bullet;
    public Material newBulletColor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("The bullet changes its color!");
            bullet.GetComponent<Renderer>().material = newBulletColor;
        }

    }

}
