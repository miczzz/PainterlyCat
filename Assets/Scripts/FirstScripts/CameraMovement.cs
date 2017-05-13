using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public Transform player;
    public Vector3 offset;
    public Quaternion originalRotation;

    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    // Use this for initialization
    void Start () {
        offset = transform.position - player.transform.position;
        originalRotation = transform.rotation;
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

    private void Update()
    {
        // Rechter Mousebutton Camera Reset für Rotation
        if (Input.GetButton("Fire2"))
        {
            var mouseMovement = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            mouseMovement = Vector2.Scale(mouseMovement, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            smoothV.x = Mathf.Lerp(smoothV.x, mouseMovement.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, mouseMovement.y, 1f / smoothing);
            mouseLook += smoothV;

            // Original
            //transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

            // Wir wollen nur Y-Rotation ändern...
            Vector3 yVectorRotation = new Vector3(transform.localRotation.eulerAngles.x, 1,0);
            //transform.localRotation = Quaternion.AngleAxis(mouseLook.x, Vector3.up);

           // mouseLook = new Vector2(mouseLook.x, -mouseLook.y);
            transform.LookAt(-mouseLook);

            //transform.rotation = Quaternion.Euler(player.forward);
            //transform.rotation = player.rotation;
            //float camRotation = Input.GetAxis("CameraRotation");
            Debug.Log("Something happens...");
        }

        // Kamera auf ursprüngliche Rotation zurücksetzen (Klick auf Mausrad)
        if (Input.GetButton("Fire3"))
        {
            transform.position = player.position + offset;
            Debug.Log("Hello there");
            transform.rotation = originalRotation;
        }
    }

    private void LateUpdate()
    {
            transform.position = player.position + offset;

    }
}


