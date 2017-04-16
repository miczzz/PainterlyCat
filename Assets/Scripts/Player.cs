﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script (adapted) from willeml85
// via http://answers.unity3d.com/questions/33380/rotate-to-facing-direction.html

public class Player : MonoBehaviour
{

    private float speed = 1.5f;
    public Transform playergraphic;

    void Update()
    {
        Movement();
    }

    void Movement()
    {

        //Player object movement
        float horMovement = Input.GetAxisRaw("Horizontal");
        float vertMovement = Input.GetAxisRaw("Vertical");
        if (horMovement != 0 && vertMovement != 0)
        {
            speed = 1.0f;
        }
        else
        {
            speed = 1.5f;
        }
        transform.Translate(transform.right * horMovement * Time.deltaTime * speed);
        transform.Translate(transform.forward * vertMovement * Time.deltaTime * speed);

        //Player graphic rotation
        Vector3 moveDirection = new Vector3(horMovement, 0, vertMovement);
        if (moveDirection != Vector3.zero)
        {
            Vector3 right = playergraphic.transform.right;
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            playergraphic.transform.rotation = Quaternion.Slerp(playergraphic.transform.rotation, newRotation, Time.deltaTime * 8);
            // instant turn of the head when 1 instead of "Time.deltaTime * 8"

        }
    }
}