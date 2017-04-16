using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour {
    public GameObject projectile;
    public Transform projectileSpawnPoint;


    private void Awake()
    {
        //GameObject projectileSpawner = Instantiate(projectileSpawnPoint);
        //projectileSpawner.transform.parent = transform;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () { 
                 if (Input.GetButtonDown("Fire1"))  {

            // Bullet wird bei "Fire1" Knopfdruck erschaffen, Bewegung siehe Script Paintball
            GameObject bullet = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        }
        }
}


