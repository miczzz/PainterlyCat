using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour {

	// Raycast

	//public int gunDamage = 1;                                           // Set the number of hitpoints that this gun will take away from shot objects with a health script
	//public float hitForce = 100f;                                       // Amount of force which will be added to objects with a rigidbody shot by the player

	public float fireRate = 1.0f;                                      // Number in seconds which controls how often the player can fire
	public float laserRange = 50f;                                     // Distance in Unity units over which the player can fire
	public Transform startPosition;                                    // Startposition of laser 
	private Camera cam;                                              	// Main camera
	private WaitForSeconds shotDuration = new WaitForSeconds(1.0f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible

	private LineRenderer laserLine;                                     // Reference to the LineRenderer component which will display laserline
	private float nextFire;   

	public Material laserColor;

	// Use this for initialization
	void Start () {
		// Get and store a reference to our LineRenderer component
		laserLine = GetComponent<LineRenderer>();

		laserLine.sharedMaterial = laserColor;

		cam = Camera.main;
	}

	public void Update() {
		
		if (Input.GetButtonDown("Fire1") && Time.time > nextFire) 
		{
			// Update the time when player can fire next
			nextFire = Time.time + fireRate;

			// Start ShotEffect coroutine to turn our laser line on and off
			StartCoroutine (ActivateLaser());

			// Create a vector at the center of our camera's viewport
			Vector3 rayOrigin = cam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));

			// Declare a raycast hit to store information about what our raycast has hit
			RaycastHit hit;

			// set startposition for laser
			laserLine.SetPosition (0, startPosition.position);

			// Check if our raycast has hit anything
			if (Physics.Raycast (rayOrigin, cam.transform.forward, out hit, laserRange))
			{
				// Set the end position for our laser line 
				laserLine.SetPosition (1, hit.point);

				PaintPotScript paintPot = hit.collider.GetComponent<PaintPotScript> ();
				if (paintPot != null) {
					if (paintPot.getColor () != null) {
						laserColor = paintPot.getColor ();
						laserLine.sharedMaterial = laserColor;
					}
				} 
				else {
					CubeColorChange colorChange = hit.collider.GetComponent<CubeColorChange> ();
					if (colorChange != null) {
						colorChange.changeColor (laserColor);
					}
				}
						

				/*
				// Get a reference to a health script attached to the collider we hit
				ShootableBox health = hit.collider.GetComponent<ShootableBox>();
				*/
				// If there was a health script attached
				/*
				if (health != null)
				{
					// Call the damage function of that script, passing in our gunDamage variable
					health.Damage (gunDamage);
				}
				*/

				// Check if the object we hit has a rigidbody attached
				/*
				if (hit.rigidbody != null)
				{
					// Add force to the rigidbody we hit, in the direction from which it was hit
					hit.rigidbody.AddForce (-hit.normal * hitForce);
				}
				*/
			}
			else
			{
				// If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
				laserLine.SetPosition (1, rayOrigin + (cam.transform.forward * laserRange));
			}
		}
	}
		

	private IEnumerator ActivateLaser()
	{

		// Turn on line renderer
		laserLine.enabled = true;

		//Wait for 1 second
		yield return shotDuration;

		// Deactivate line renderer after waiting
		laserLine.enabled = false;
	}
}
