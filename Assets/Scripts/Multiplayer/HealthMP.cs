using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class HealthMP : NetworkBehaviour {

    public const int maxHealth = 5;
    // wenn sich die Health ändert wird die Methode OnChangeHealth vom Server aufgerufen und updated die UI
    [SyncVar (hook = "OnChangeHealth")] public int currentHealth = maxHealth;
    public int maximumHealth = 5;
    public RectTransform healthbar;
    public ParticleSystem[] effects;
    public ParticleSystem winningFanfare;
    public ParticleSystem deathEffect;
    public AudioSource wasHitSound;
    public MPPlayer otherPlayer;
    public Transform serverPlayerPlace;
    public Transform clientPlayerPlace;
    public GameObject restartMenu;
    public GameObject[] paintballs;
    //public Canvas restartMenuCanvas;
  
    public Material[] newColors;
    public GameObject gameOverText;
    public Text gameOverTextText;

    private void Start()
    {
        restartMenu = GameObject.FindWithTag("RematchMenu");
        Debug.Log(restartMenu);
        //restartMenuCanvas = restartMenu.GetComponent<Canvas>();
        //restartMenuCanvas.enabled = false;
        //restartMenu.SetActive(false);
        //FindObjectOfType(PlayerControllerNetwork).gameObject;
    }

    private void Update()
    {
        // In Start findet er das nicht...da zu schnell?
        //if (restartMenu = null)
        //{
        //while (restartMenu == null)
        
            restartMenu = GameObject.FindWithTag("RematchMenu");
        
        //}
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        // um eine neue Farbe zu geben
        CmdChangeBodyColor();

        effects = ParticleSystem.FindObjectsOfType<ParticleSystem>();
        foreach (ParticleSystem effect in effects)
        {
            effect.Stop(true);
        }

        // Alle Restbälle dieser Runde löschen
        paintballs = GameObject.FindGameObjectsWithTag("Paintball");
        foreach (GameObject ball in paintballs)
        {
            Destroy(ball);
        }
    }

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
            wasHitSound.Play();
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

                RpcSetGameOverText();
        
                Debug.Log("You are dead, dude!");

            //passiert halt jetzt sofort...naja
            //gameObject.GetComponent<Renderer>().enabled = false;

            
            //ResetHealth();
            //gameObject.SetActive(false);
            //Destroy(gameObject, 2);
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
        // wenn er stirbt muss er ja die Farbe nicht mehr ändern
        if (currentHealth >= 1)
        {
            int newColorNo = (int)Random.Range(0.01f, 3.99f);

            Debug.Log(newColorNo);
            // Farbe des Players setzen (am besten einmal in einer anderen Klasse)

            Color playerBodyColor = transform.Find("PlayerBody").GetComponent<Renderer>().material.color;

            // Damit die Farbe immer eine andere ist und nicht gleich bleibt (vielleicht soll es aber doch die Option geben, noch unklar)
            while (playerBodyColor.Equals(newColors[newColorNo].color))
            {
                newColorNo = (int)Random.Range(0.01f, 3.99f);
            }

            transform.Find("PlayerBody").GetComponent<Renderer>().material = newColors[newColorNo];
            RpcSetPlayerColors(newColorNo);
        }

        // like a ghost!
        if(currentHealth == 0)
        {
            // transform.Find("PlayerBody").GetComponent<Renderer>().material.color = Color.white;
        }

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

    // calling all clients ( I guess? )
    [ClientRpc]
    void RpcSetGameOverText()
    {
        restartMenu.transform.GetChild(0).gameObject.SetActive(true);

        gameOverText = GameObject.FindWithTag("GameOverText");
        gameOverTextText = gameOverText.GetComponent<Text>();
        deathEffect.GetComponent<ParticleSystemRenderer>().material = transform.Find("PlayerBody").GetComponent<Renderer>().material;
        Instantiate(deathEffect.gameObject, transform);

        // just a ghost...

        //if (!deathEffect.isPlaying)
        //{
        //    transform.Find("PlayerBody").GetComponent<Renderer>().material.color = Color.white;
        //}

        if (isLocalPlayer)
        {
            gameOverTextText.text = "VERLOREN";
            //restartMenuCanvas.enabled = true;
            if (isServer)
            {
                CmdGameOverEffects(false);
            }

        }
        else
        {
            gameOverTextText.text = "GEWONNEN";
            //restartMenuCanvas.enabled = true;
            if (!isServer)
            {
                CmdGameOverEffects(true);
            }
            else
            {
                RpcGameOverEffects(true);
            }

        }      

    }

    [Command]
    void CmdGameOverEffects(bool serverWon)
    {
        // aus Sicht des Gewinners:
        otherPlayer = FindObjectOfType<MPPlayer>();

        serverPlayerPlace = otherPlayer.player1.transform;
        clientPlayerPlace = otherPlayer.player2.transform;

        MPPlayer serverPlayer = serverPlayerPlace.GetComponentInParent<MPPlayer>();
        MPPlayer clientPlayer = clientPlayerPlace.GetComponentInParent<MPPlayer>();


        // Beim Host wird es richtig angezeigt, beim Server falsch...

        //if (serverWon)
        //    {
        //        Instantiate(winningFanfare.gameObject, serverPlayer.transform);
        //    }
        //    else
        //    {
        //        Instantiate(winningFanfare.gameObject, clientPlayer.transform);
        //    }

        //restartMenuCanvas.enabled = true;

        //restartMenu.SetActive(true);

        //Destroy(serverPlayer);
        //Destroy(clientPlayer);

        }


    // From server to clients
    [ClientRpc]
    void RpcGameOverEffects(bool serverWon)
    {
        // aus Sicht des Gewinners:
        otherPlayer = FindObjectOfType<MPPlayer>();

        serverPlayerPlace = otherPlayer.player1.transform;
        clientPlayerPlace = otherPlayer.player2.transform;

        MPPlayer serverPlayer = serverPlayerPlace.GetComponentInParent<MPPlayer>();
        MPPlayer clientPlayer = clientPlayerPlace.GetComponentInParent<MPPlayer>();


        // Beim Host wird es richtig angezeigt, beim Server falsch...

        if (serverWon)
        {
            Instantiate(winningFanfare.gameObject, serverPlayer.transform);
        }
        else
        {
            Instantiate(winningFanfare.gameObject, clientPlayer.transform);
        }

        //restartMenuCanvas.enabled = true;


        //restartMenu.SetActive(true);

        // unnötig?
        //Destroy(serverPlayer);
        //Destroy(clientPlayer);

    }

}



