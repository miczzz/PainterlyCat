using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Easy peasy lemon squeezy level zum ausprobieren
public class ObjectiveLevelZero : MonoBehaviour {

    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    public Material object1WantedColor;
    public Material object2WantedColor;
    public Material object3WantedColor;

    private Material obj1Color;
    private Material obj2Color;
    private Material obj3Color;

    private bool levelZeroComplete;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!levelZeroComplete) {

            obj1Color = object1.GetComponent<Renderer>().material;
            obj2Color = object2.GetComponent<Renderer>().material;
            obj3Color = object3.GetComponent<Renderer>().material;

            if (obj1Color.color == object1WantedColor.color &&
                obj2Color.color == object2WantedColor.color &&
                obj3Color.color == object3WantedColor.color)
            {
                Debug.Log("Level complete");
                levelZeroComplete = true;
            }
    
        }


	}
}
