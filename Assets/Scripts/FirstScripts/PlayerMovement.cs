using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public int velocity = 1;
    //public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float verticalMovement = Input.GetAxis("Vertical") * Time.deltaTime * velocity;
        float horizontalMovement = Input.GetAxis("Horizontal") * Time.deltaTime * velocity;

        //float verticalRotation = Input.GetAxis("Vertical") * Time.deltaTime * velocity;
        //float horizontalRotation = Input.GetAxis("Horizontal") * Time.deltaTime * velocity;

        
        //transform.Translate(horizontalMovement, 0, verticalMovement);
        //transform.forward = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));
        //transform.forward = Vector3.Normalize(new Vector3(horizontalMovement, 0f, verticalMovement));

        float translate = Input.GetAxis("Horizontal");
        transform.forward = new Vector3(translate, 0, 0);
        transform.Translate(Vector3.forward * Mathf.Abs(translate) * 5 * Time.deltaTime);

        //float faceDirection = Input.GetAxisRaw("Horizontal");
        //if (faceDirection != 0)
        //{
        //    transform.forward = new Vector3(faceDirection, transform.Y, transform.Z);
        //}

    }
}
