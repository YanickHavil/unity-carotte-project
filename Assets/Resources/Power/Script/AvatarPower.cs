using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarPower : MonoBehaviour {


	// Use this for initialization
	void Start () {

		Transformation ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void Transformation(){
		Vector3 v3 = transform.position;
		GameObject g = Instantiate(Resources.Load ("Entity/Avatar/Avatar") , v3, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
		v3 = new Vector3(transform.position.x , 1.0f,transform.position.z);



		Destroy (gameObject);
	}
}
