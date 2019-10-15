using UnityEngine;
using System.Collections;

public class PollenManager : MonoBehaviour {

	public string pollen;
	public int rate =10; // poucentage of making a pollen
	public int speedRate = 10;
	// Use this for initialization
	void Start () {
	
		InvokeRepeating("Reproduction",speedRate,speedRate); // every speedRate second
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Reproduction(){
		int token = Random.Range (0, 101);

		if (token <= rate) {
			Instantiate (Resources.Load ("Plant/Pollen/" + pollen),
			                          new Vector3 (transform.position.x, transform.position.y, transform.position.z),
			                          Quaternion.Euler (new Vector3 (0, 30, 0)));
		} else {

		}
	}
}
