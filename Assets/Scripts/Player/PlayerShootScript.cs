using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour {

	private int cooldown;
	public int maxCooldown = 100;

	public GameObject startPosition;

	public GameObject projectile;     

	// Use this for initialization
	void Start () {
		cooldown = maxCooldown;
	}

	public void FixedUpdate() {
		
		cooldown--;

		if (Input.GetButtonDown ("Fire1")) {
			if (cooldown <= 0) {
				GameObject bullet = Instantiate (projectile);
				bullet.transform.position = startPosition.transform.position;
				cooldown = maxCooldown;
			}
		}
	
	}
}
