using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public int velocity = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float verticalMovement = Input.GetAxis("Vertical") * Time.deltaTime * velocity;
        float horizontalMovement = Input.GetAxis("Horizontal") * Time.deltaTime * velocity;

        transform.Translate(horizontalMovement, 0, verticalMovement);
        
    }
}
