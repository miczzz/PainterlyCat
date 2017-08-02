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

        // note: Due to unity's use of the escape key the cursor will not reset to the center of the screen in the editor
        // but it will in the build (which is where it counts)
        // when using 
        // Cursor.lockState = CursorLockMode.Locked; followed by
        // Cursor.lockState = CursorLockMode.None;
        if (Input.GetKeyDown ("escape")) {
            Cursor.visible = true;


            // Wenn man das Menü aufruft
            if (!PauseMenu.enabled)
            {
                FindObjectOfType<CameraMouseMovement>().mouseEnabled = false;
                PauseMenu.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.lockState = CursorLockMode.None;
                // Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.Locked;

            } else          
            // Wenn das Menü weggedrückt wird
            {
                FindObjectOfType<CameraMouseMovement>().mouseEnabled = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
                PauseMenu.enabled = false;
            }
		}
	

	}
}
