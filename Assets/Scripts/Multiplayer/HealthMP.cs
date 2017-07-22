using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HealthMP : NetworkBehaviour {

    public const int maxHealth = 5;
    // wenn sich die Health ändert wird die Methode OnChangeHealth vom Server aufgerufen und updated die UI
    [SyncVar (hook = "OnChangeHealth")] public int currentHealth = maxHealth;
    public RectTransform healthbar;
    public ParticleSystem winningFanfare;
    public ParticleSystem deathEffect;

    public Material[] newColors;


    public void TakeDamage(int amount, Color bulletColor, Color playerColor)
    {
        // let the server handle this!
        if (!isServer)
        {
            return;
        }

        if (bulletColor.Equals(playerColor))
        {
            currentHealth -= amount;
            Debug.Log("It's a match!");
            CmdChangeBodyColor();
        }
        else
        {
            Debug.Log("Color mismatch");
        }

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("You are dead, dude!");
                // soll später beim anderen Spieler ausgelöst werden
                 Instantiate(winningFanfare.gameObject, transform);
                 Instantiate(deathEffect.gameObject, transform);

                Destroy(gameObject, 2);
            }
        }     


    void OnChangeHealth(int health)
    {
        // health * x = size of Foreground[Grafik des Balken] width (hier 100, also 20 bei maxHealth5)
        healthbar.sizeDelta = new Vector2(health * 20, healthbar.sizeDelta.y);
        //currentHealth = health;
    }

    //Server ändert die Farbe des getroffenen Players
    [Command]
    void CmdChangeBodyColor()
    {
            int newColorNo = (int)Random.Range(0.01f, 3.99f);

            Debug.Log(newColorNo);
        // Farbe des Players setzen (am besten einmal in einer anderen Klasse)

            Color playerBodyColor = transform.Find("PlayerBody").GetComponent<Renderer>().material.color;

        // Damit die Farbe immer eine andere ist und nicht gleich bleibt (vielleicht soll es aber doch die Option geben, noch unklar)
            while (playerBodyColor.Equals(newColors[newColorNo].color)){
            newColorNo = (int)Random.Range(0.01f, 3.99f);
             }

        transform.Find("PlayerBody").GetComponent<Renderer>().material = newColors[newColorNo];
        RpcSetPlayerColors(newColorNo);

    }

    //Client übernimmt die Änderungen des Servers (ja ich bin auch der Meinung das müsste doch zusammengehen lol)
    [ClientRpc]
    void RpcSetPlayerColors(int colorNo)
    {
        // Wurden vom Server schon gesetzt
        if (isServer)
        {
            return;
        }

        // Nur für den Client
        transform.Find("PlayerBody").GetComponent<Renderer>().material = newColors[colorNo];
    }

}
