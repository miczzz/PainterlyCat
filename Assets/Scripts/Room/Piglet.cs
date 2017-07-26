using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piglet : MonoBehaviour {

    public GameObject dialogueBox;
    public Transform target;

    private void OnTriggerEnter(Collider collision)
    {

        // vielleicht noch hinzufügen, dass das Schwein die Katze anschaut, mal schauen
   //     transform.parent.LookAt(target);
        dialogueBox.SetActive(true);
    }

    private void OnTriggerExit(Collider collision)
    {
        
        dialogueBox.SetActive(false);
    }

}
