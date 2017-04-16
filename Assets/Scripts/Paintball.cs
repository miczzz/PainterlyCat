using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintball : MonoBehaviour {

    public float movement;
    public int lifeTicks;

    private int aliveFor;

    // Use this for initialization
    void Start () {
		
	}

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * movement);

        aliveFor++;
        if (aliveFor == lifeTicks)
        {
            Destroy(gameObject);
        }
    }
}
