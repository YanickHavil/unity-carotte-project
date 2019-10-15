using UnityEngine;
using System.Collections;

public class InvisibleObstacleTile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		Instantiate (Resources.Load("ObstacleTest/ObstacleTile"),new Vector3 (14.5f,0,14.5f),Quaternion.identity);
		StartCoroutine(disapear());

	}
	
	// Update is called once per frame
	void Update () {


	
	}

	IEnumerator disapear() {
		yield return new WaitForSeconds(4);
		Destroy (gameObject);
		Debug.Log ("Destroy");

	}
}
