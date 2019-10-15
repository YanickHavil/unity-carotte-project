using UnityEngine;
using System.Collections;

public class LightMove : MonoBehaviour {

	int time = 0;
	Vector3 direction;
	// Use this for initialization
	void Start () {

		InvokeRepeating ("LightTime", 1, 1);
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.LookAt (direction);
	
	}

	void LightTime(){

		time++;
		if (time == 5) {
			direction = Vector3.up;
		}
		if (time == 10) {
			direction = Vector3.left;

		}
		if (time == 15) {
			direction = Vector3.down;

		}
		if (time == 20) {
			direction = Vector3.right;
			time = 0;
		}

		//Debug.Log ("Time = " + time);
	}
}
