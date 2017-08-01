using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseMovement : MonoBehaviour {

	Vector2 mouseLook;
	Vector2 smoothV;
	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;
    public int Min_X = 0;
    public int Min_Y = -54;
    public int Max_X = 255;
    public int Max_Y = 116;
    

    GameObject player;
	// Use this for initialization
	void Start () {
		player = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		var mouseMovement = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

		mouseMovement = Vector2.Scale (mouseMovement, new Vector2 (sensitivity * smoothing, sensitivity * smoothing));
		smoothV.x = Mathf.Lerp (smoothV.x, mouseMovement.x, 1f / smoothing);
		smoothV.y = Mathf.Lerp (smoothV.y, mouseMovement.y, 1f / smoothing);

       // mouseLook.x = Mathf.Clamp(mouseLook.x+smoothV.x, Min_X, Max_X);
        mouseLook.x = mouseLook.x + smoothV.x;

        // Um Kamerabewegung zu beschränken, so dass man die Kamera nicht auf den Kopf stellen kann
        mouseLook.y = Mathf.Clamp(mouseLook.y+smoothV.y, Min_Y, Max_Y);

		transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
		player.transform.localRotation = Quaternion.AngleAxis (mouseLook.x, player.transform.up);

	}
}
