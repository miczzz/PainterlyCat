using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float camMove = Input.GetAxis ("CameraMovement");
		//transform.Translate (transform.forward * camMove);
		transform.Translate(Vector3.forward*camMove);


		float camRotation = Input.GetAxis ("CameraRotation");
		//transform.RotateAround (transform.parent, camRotation, 0);


		// ok
//		transform.Translate (Vector3.left * camRotation / 20);
//		transform.LookAt (transform.parent);

		// besser
		//transform.parent.Rotate (0, 1 * camRotation, 0);

		// am besten | Vector 3.up = 0,1,0
		transform.RotateAround(transform.root.position, Vector3.up, camRotation); 
	
	}
}


