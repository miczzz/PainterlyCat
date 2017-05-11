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
    public Material bulletColor;

    public Crosshairs crosshairs;
    private Vector3 point;
    private GameObject playerActive;
    private Transform brushHead;
    public GameObject brushHeadGameObject;

    public Vector4 bulletColorVector4;
    public float movement = 100.14f;

    void Start()
    {

        if (!isLocalPlayer)
        {
            return;
        }
        cam = FindObjectOfType<Camera>();
        crosshairs = FindObjectOfType<Crosshairs>();

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

            // Paintballfarbe wird vom Brushhead übernommen
            //bulletColor = brush.transform.Find("Brushhead").GetComponent<Renderer>().material;
            bulletColor = brushHeadGameObject.GetComponent<Renderer>().material;

            projectile.GetComponent<Renderer>().material = bulletColor;
            

            // Vector4, da komplexe Variablen wie Color, Material usw wohl nicht so einfach vom Network unterstützt werden
            bulletColorVector4 = bulletColor.color;
            // Bullet wird bei "Fire1" Knopfdruck erschaffen, Bewegung siehe Script Paintball

            if (!isServer)
            {
                CmdFire(bulletColorVector4);
            } else
            {
                //Debug.Log("So you are the client, huh?");
                RpcColorBullet(bulletColorVector4);
            }

        }

    }

    // Client lässt an Server ausführen
    [Command]
    public void CmdFire(Vector4 bulletColorVector)
    {
        // Paintball wird erstellt

        GameObject bullet = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation) as GameObject;
        bullet.GetComponent<Renderer>().material.color = bulletColorVector;

        NetworkServer.Spawn(bullet);
    }

    // Von Server zu Clients
    [ClientRpc]
    public void RpcColorBullet(Vector4 bulletColorClient)
    {
        GameObject bullet = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation) as GameObject;
        bullet.GetComponent<Renderer>().material.color = bulletColorClient;
    }

}


