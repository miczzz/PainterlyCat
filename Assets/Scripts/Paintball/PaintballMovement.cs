using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintballMovement : MonoBehaviour {

	public float movement = 0.1f;
	public int lifeTicks = 10000;

	private int aliveFor;

	private Camera camera;

	private Vector3 direction;

	// Use this for initialization
	void Start () {
		camera = Camera.main;

		direction = camera.ViewportToWorldPoint (new Vector3(0.5f,0.5f,0.0f));
		direction.z = direction.z + 10;
		transform.LookAt (direction);
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate (Vector3.forward * movement);

		aliveFor++;
		if (aliveFor == lifeTicks)
		{
			Destroy(gameObject);
		}

	}
}
