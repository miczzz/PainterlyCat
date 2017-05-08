using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootScriptMP : NetworkBehaviour
{
    public GameObject projectile;

    public Transform projectileSpawnPoint;

    public Camera cam;
    public GameObject brush;

    public Crosshairs crosshairs;
    private Vector3 point;

    public float movement = 100.14f;

    private void Awake()
    {
        //        GameObject projectileSpawner = Instantiate(projectileSpawnPoint) as GameObject;
   
        //projectileSpawner.transform.parent = transform;
    }

    void Start()
    {

        if (!isLocalPlayer)
        {
            return;
        }
        cam = FindObjectOfType<Camera>();
        crosshairs = FindObjectOfType<Crosshairs>();

        //crosshairs = Instantiate(crosshairs);
        //


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

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
            // Wenn die Katze sein Target anschauen soll
            //transform.LookAt(point);
            brush.transform.LookAt(point);
            projectileSpawnPoint.transform.LookAt(point);
        }


        // linker Mousebutton = Fire1
        if (Input.GetButtonDown("Fire1"))
        {
            //beim Schießen in die Richtung schauen

            // falls die Katze sich beim Schießen in Schießrichtung drehen soll
            //transform.LookAt(point);
            // Bullet wird bei "Fire1" Knopfdruck erschaffen, Bewegung siehe Script Paintball
            CmdFire();

        }


    }

    [Command]
    void CmdFire()
    {
       // bringt nix projectileSpawnPoint.transform.LookAt(point);
        GameObject bullet = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation) as GameObject;
        //bullet.transform.Translate(Vector3.forward * movement);
        Debug.Log(projectileSpawnPoint.position);
        Debug.Log(projectileSpawnPoint.rotation);
        Debug.Log("Hallo?!");
        NetworkServer.Spawn(bullet);
    }
}


