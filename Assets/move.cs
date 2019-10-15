using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		//GetComponent<NavMeshAgent> ().destination = new Vector3 (50, 0, 50);
	}
	
	// Update is called once per frame
	void Update () {
	
		int x = Mathf.FloorToInt( transform.position.x ); // Il faut transform.positionx et y /tileResolution mais c'est 1.0f pour le moment
		int z = Mathf.FloorToInt( transform.position.z );

		if (GameObject.Find ("TileMap").GetComponent<TileMap> ().getMap ().GetTileAt (x, z).type == EnumTypeTile.EARTH) {
			GetComponent<UnityEngine.AI.NavMeshAgent> ().speed = 2;
		} else {
			GetComponent<UnityEngine.AI.NavMeshAgent> ().speed = 0.5f;

		}

		//Debug.Log("x = " + x + " z = " + z);
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log("Collision");

	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "TileMap") {

		} else {
			Debug.Log("Collider = " + other.name);

		}
	}

	void OnTriggerExit(Collider other){

		Debug.Log ("End Collide with " + other);

	}
}
