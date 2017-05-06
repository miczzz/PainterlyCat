using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://unity3d.com/de/learn/tutorials/projects/2d-ufo-tutorial/following-player-camera

public class CompleteCameraController : MonoBehaviour
{
    public float cam_speed = 1f;
    public Transform player;       //Public variable to store a reference to the player game object
    public GameObject gameCam;

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offset = transform.position - player.transform.position;
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        //test
        if (Input.GetKeyDown("p"))
        {
            Debug.Log("Peepee!");
            gameCam.transform.position = player.transform.position + offset;
            Debug.Log(transform.position);
        }
    }

        // LateUpdate is called after Update each frame
        void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.

        // transform.position = player.transform.root.position + offset;
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, cam_speed * Time.deltaTime);

            //transform.position = player.position + offset;


    }
}