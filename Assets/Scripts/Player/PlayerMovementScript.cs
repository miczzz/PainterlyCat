using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

	public float speed = 10.0F;

	public Canvas PauseMenu;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Awake () {
		PauseMenu.enabled = false;
        Cursor.visible = false;
    }

	// Use this for initialization
	void Start () {
        // make cursor invisble and cursor cant go outside screen
        // press escape to show cursor 
       // Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        //Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

		float vertical = Input.GetAxis ("Vertical") * speed;
		float horizonzal = Input.GetAxis ("Horizontal") * speed;
		vertical *= Time.deltaTime;
		horizonzal *= Time.deltaTime;

		transform.Translate (horizonzal, 0, vertical);

	
		if (Input.GetKeyDown ("escape")) {
            Cursor.visible = true;

            Cursor.lockState = CursorLockMode.Confined;


            // führt zu Problemen:
            //Cursor.lockState = CursorLockMode.Locked;

            // Wenn man das Menü aufruft
            if (!PauseMenu.enabled)
            {            
                PauseMenu.enabled = true;
                //Cursor.lockState = CursorLockMode.Locked;
                // Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.Locked;

            } else          
            // Wenn das Menü weggedrückt wird
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
                PauseMenu.enabled = false;
            }
		}
	

	}
}
