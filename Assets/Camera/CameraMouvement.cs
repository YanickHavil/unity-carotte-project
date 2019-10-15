using UnityEngine;
using System.Collections;

public class CameraMouvement : MonoBehaviour {

	float speed = 5.0f;
	InputManager inputM;
	// Use this for initialization
	void Start () {
		inputM = GameObject.Find ("Player").GetComponent<InputManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (inputM.avatarModeActive) {

		} else {
			if (Input.GetKey (KeyCode.LeftControl) && Input.GetKey (KeyCode.Z)) {
				Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize-1, 1);

			}
			if (Input.GetKey (KeyCode.LeftControl) && Input.GetKey (KeyCode.S)) {
				Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize+1, 20);

			}
			if (Input.GetKey (KeyCode.D)) {
				transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
			}
			if (Input.GetKey (KeyCode.Q)) {
				transform.Translate (new Vector3 (-speed * Time.deltaTime, 0, 0));
			}
			if (Input.GetKey (KeyCode.S)) {
				transform.Translate (new Vector3 (0, -speed * Time.deltaTime, 0));
			}
			if (Input.GetKey (KeyCode.Z)) {
				transform.Translate (new Vector3 (0, speed * Time.deltaTime, 0));
			}


		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0) 
		{
			Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize-1, 1);
			//speed -=0.3f;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0) 
		{
			Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize+1, 20);
			//speed +=0.3f;

		}


	
	}
}
