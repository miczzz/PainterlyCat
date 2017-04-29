using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public Transform player;
    public Vector3 offset;

    // Use this for initialization
    void Start () {
        offset = transform.position - player.transform.position;
        
    }

    // Update is called once per frame
    //void Update () {
    //	float camMove = Input.GetAxis ("CameraMovement");
    //	//transform.Translate (transform.forward * camMove);
    //	//transform.Translate(Vector3.forward*camMove);



    //       //transform.forward = -1 * transform.position.normalized;
    //       float camRotation = Input.GetAxis ("CameraRotation");
    //       //transform.RotateAround (transform.parent, camRotation, 0);


    //       // ok
    //       //		transform.Translate (Vector3.left * camRotation / 20);
    //       //		transform.LookAt (transform.parent);

    //       // besser
    //       //		transform.parent.Rotate (0, 1 * camRotation, 0);

    //       // am besten | Vector 3.up = 0,1,0
    //       //transform.RotateAround(transform.root.position, Vector3.up, camRotation); 

    //       //transform.position = player.position + offset;
    //   }

    private void LateUpdate()
    {
            transform.position = player.position + offset;

    }
}


