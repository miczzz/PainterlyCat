using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

// adapted from PlayerController script (see https://github.com/SebLague/Blender-to-Unity-Character-Creation) by Sebastian Lague

public class PlayerControllerNetwork : NetworkBehaviour {

	public float walkSpeed = 2;
	public float runSpeed = 6;

	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;

	Transform cameraT;
    public Transform walkingFigure;
    public GameObject crosshairs;

	void Start () {


        Debug.Log("Local player:" + isLocalPlayer);

        if (!isLocalPlayer)
        {
           
            Destroy(this);
            return;
        }


        cameraT = Camera.main.transform;
        crosshairs = Instantiate(crosshairs, transform.position, cameraT.rotation);
        //crosshairs.layer = 31;
        //crosshairs = Instantiate(crosshairs, cameraT.position, Quaternion.identity);

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

		//float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;

	}
}
