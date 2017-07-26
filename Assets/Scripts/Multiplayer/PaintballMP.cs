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

    public Transform spawnPoint;


   
    private void FixedUpdate()
    {

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

            Debug.Log("Bullet: " + bulletColor + " Player: " + hitColor);

            health.TakeDamage(1, bulletColor.color, hitColor.color);

        }
    }
}
