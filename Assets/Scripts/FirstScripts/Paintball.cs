using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintball : MonoBehaviour {

    public float movement;
    public int lifeTicks;

    public Camera cam;

    private int aliveFor;

    public Crosshairs crosshairs;


    // Use this for initialization
    void Start()
    {
    }
    //    cam = FindObjectOfType<Camera>();
    //    crosshairs = FindObjectOfType<Crosshairs>();

    //    float rayDistance;
    //    // test for shooting direction
    //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    //    Plane plane = new Plane(Vector3.up, Vector3.up);
    //    if (plane.Raycast(ray, out rayDistance))
    //    {
    //        Vector3 point = ray.GetPoint(rayDistance);
    //        Debug.DrawLine(ray.origin, point, Color.red);

    //        crosshairs.transform.position = point;
    //        //crosshairs.DetectTargets(ray);

    //        transform.LookAt(point);

    //    }
    //}

    private void FixedUpdate()
    {

        transform.Translate(Vector3.forward * movement);


        aliveFor++;
        if (aliveFor == lifeTicks)
        {
            Destroy(gameObject);
        }
    }
}
