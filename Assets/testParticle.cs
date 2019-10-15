using UnityEngine;
using System.Collections;

public class testParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Test", 5, 5);
		GetComponent<ParticleSystem> ().Stop ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Test(){
		if (GetComponent<ParticleSystem> ().isStopped) {
			GetComponent<ParticleSystem> ().Play();
			//GetComponent<ParticleSystem> ().Simulate (0);
		} else {
			GetComponent<ParticleSystem> ().Stop();
		}
	}
}
