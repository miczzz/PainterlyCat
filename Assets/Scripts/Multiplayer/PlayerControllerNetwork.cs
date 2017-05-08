﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerControllerNetwork : NetworkBehaviour {

	public float walkSpeed = 2;
	public float runSpeed = 6;

	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;

	Animator animator;
	Transform cameraT;
    public Transform walkingFigure;
    public GameObject crosshairs;

	void Start () {
        //animator = GetComponent<Animator> ();

        //if (!GetComponentInParent<NetworkIdentity>().isLocalPlayer)

        Debug.Log("Local player:" + isLocalPlayer);
            //Debug.Log(GetComponentInParent<NetworkIdentity>().isLocalPlayer);
        if (!isLocalPlayer)
        {
           
            Destroy(this);
            return;
        }


        cameraT = Camera.main.transform;
        crosshairs = Instantiate(crosshairs, transform.position, transform.rotation);
        //NetworkServer.Spawn(crosshairs);

    }

	void Update () {

		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
        Vector2 inputDir = input.normalized;
     
        if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            walkingFigure.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(walkingFigure.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}

		bool running = Input.GetKey (KeyCode.LeftShift);
		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        walkingFigure.Translate (walkingFigure.forward * currentSpeed * Time.deltaTime, Space.World);

		float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
		//animator.SetFloat ("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

	}
}
