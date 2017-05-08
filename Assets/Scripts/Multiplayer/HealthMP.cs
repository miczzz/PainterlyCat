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

    public void TakeDamage(int amount)
    {
        // let the server handle this!
        if (!isServer)
        {
            return;
        }

        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("You are dead, dude!");
        }
    }

    void OnChangeHealth(int health)
    {
        // health * x = size of Foreground width (hier 100, also 20 bei maxHealth5)
        healthbar.sizeDelta = new Vector2(health * 20, healthbar.sizeDelta.y);
        //currentHealth = health;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
