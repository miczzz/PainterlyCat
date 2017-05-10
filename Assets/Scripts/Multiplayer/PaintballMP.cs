using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PaintballMP : NetworkBehaviour
{

    public float movement;
    public int lifeTicks;

    public Material bulletColor;
    public Material hitColor;

    public Camera cam;

    private int aliveFor;

    public Crosshairs crosshairs;
    private GameObject hit;

    Plane plane;
    Ray ray;
    public Transform spawnPoint;



    // Use this for initialization
    void Start()
    {
        //crosshairs = FindObjectOfType<Crosshairs>();
        //transform.Translate(Vector3.forward * movement);


    }

   // [ServerCallback]
    private void FixedUpdate()
    {
        // test for shooting direction
        //ray = cam.ScreenPointToRay(Input.mousePosition);
        //plane = new Plane(Vector3.up, Vector3.up);
        //float rayDistance;

        //if (plane.Raycast(ray, out rayDistance))
        //{
        //    Vector3 point = ray.GetPoint(rayDistance);
        //    Debug.DrawLine(ray.origin, point, Color.red);

        //    crosshairs.transform.position = point;
        //    //crosshairs.DetectTargets(ray);

        //    transform.LookAt(point);

        //}

        // Bullet fliegt geradeaus nach vorne | jetzt im ShootScriptMP
        transform.Translate(Vector3.forward * movement);
  
        // Bullet stirbt nach x Frames
        aliveFor++;
        if (aliveFor == lifeTicks)
        {
            NetworkServer.Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected");
        // Wenn der Player vom Bullet getroffen wird, wird Schaden von Health abgezogen
        hit = collision.gameObject;
        CmdBulletHitPlayer();

        // Bei Berührung stirbt Bullet
        Destroy(gameObject);
    }

    void CmdBulletHitPlayer()
    {
        HealthMP health = hit.GetComponent<HealthMP>();
        Debug.Log(hit);
        if (health != null)
        {

            bulletColor = gameObject.GetComponent<Renderer>().material;
            hitColor = hit.transform.Find("PlayerBody").GetComponent<Renderer>().material;
            //brush.transform.Find("Brushhead").GetComponent<Renderer>().material = newBulletColor;
            //GetComponent<Renderer>().material = hitColor;

            Debug.Log("Bullet: " + bulletColor + " Player: " + hitColor);

            health.TakeDamage(1, bulletColor.color, hitColor.color);

        }
    }
}
