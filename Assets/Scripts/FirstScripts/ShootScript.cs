using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour {
    public GameObject projectile;
    public Transform projectileSpawnPoint;
    public Camera cam;
    public GameObject brush;

    public Crosshairs crosshairs;
    private Vector3 point;

    private void Awake()
    {
        //GameObject projectileSpawner = Instantiate(projectileSpawnPoint);
        //projectileSpawner.transform.parent = transform;
    }

    void Start()
    {
        cam = FindObjectOfType<Camera>();
        crosshairs = FindObjectOfType<Crosshairs>();


    }

    // Update is called once per frame
    void FixedUpdate () {

        float rayDistance;
        // test for shooting direction
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.up);
        if (plane.Raycast(ray, out rayDistance))
        {
            point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);

            crosshairs.transform.position = point;
            //crosshairs.DetectTargets(ray);
            //transform.LookAt(point);
            brush.transform.LookAt(point);
            projectileSpawnPoint.transform.LookAt(point);
        }


        // linker Mousebutton = Fire1
        if (Input.GetButtonDown("Fire1"))  {
            //beim Schießen in die Richtung schauen
         
            // falls die Katze sich beim Schießen in Schießrichtung drehen soll
            //transform.LookAt(point);
            // Bullet wird bei "Fire1" Knopfdruck erschaffen, Bewegung siehe Script Paintball
            GameObject bullet = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        }


    }
}


