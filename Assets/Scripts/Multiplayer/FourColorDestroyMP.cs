using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// Skript mit dem sich das Objekt selbst entfernt, nachdem es von vier verschiedenen Farben getroffen wurde (Spielerei, ich gebs zu)
public class FourColorDestroyMP : NetworkBehaviour {
    public Material newColor;
    public Vector4 newColorVec;
    public Color[] collectedColors;
    public int colorPointer = 0;
    //public int collectedColorSize = 4;

    // Use this for initialization 
    void Start () {
        collectedColors = new Color[4];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Wenn das Objekt vom Paintball getroffen wird übernimmt es die Farbes des Paintballs
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Paintball"))
        {
            // die Farbe holen vom Paintball
            newColor = other.GetComponent<Renderer>().material;
            Debug.Log(newColor);
            // die Farbe dem Objekt übergeben (wird angemalt)
            newColorVec = newColor.color;
            OnColorChange(newColorVec);

            CheckIfColorCollected();

            Debug.Log("The cube (or whatever) changes its color!");
        }

    }

    private void CheckIfColorCollected()
    {
        foreach (var color in collectedColors)
        {
            if (color.Equals(newColor.color))
                return;
        }
        collectedColors.SetValue(newColor.color, colorPointer);
        colorPointer++;
        if (colorPointer == 4)
        {
            KillCube();
        }
    }

    private void KillCube()
    {
        Destroy(gameObject, 2);
        // Vielleicht particle effect mit kleinen (bunten?) Cubes
    }

    private void OnColorChange(Vector4 newestColor) {
        //GetComponent<Renderer>().material.color = newestColor;
        CmdChangeBodyColor(newestColor);
    }

    //Server ändert die Farbe des getroffenen Cubes
    [Command]
    void CmdChangeBodyColor(Vector4 newestColor)
    {
        GetComponent<Renderer>().material.color = newestColor;
        RpcSetPlayerColors(newestColor);
    }

    //Client übernimmt die Änderungen des Servers
    [ClientRpc]
    void RpcSetPlayerColors(Vector4 newestColor)
    {
        // Wurden vom Server schon gesetzt
        if (isServer)
        {
            return;
        }

        // Nur für den Client
        GetComponent<Renderer>().material.color = newestColor;
    }

}
