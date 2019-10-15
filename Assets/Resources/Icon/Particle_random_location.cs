using UnityEngine;
using System.Collections;

public class Particle_random_location : MonoBehaviour {
	private Renderer rend;
	private int lifetime = 160; // Lifetime of Icon
	float rand;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		transform.rotation = Quaternion.Euler(new Vector3 (90, 0, 0));

		move ();

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(0.0015f*rand,0,0.0015f*rand));
		lifetime--;
		rend.material.color -= new Color(0.1F, 0, 0) * Time.deltaTime/4;
		if (lifetime <= 0) {
			Destroy (gameObject);
		}


	
	}

	void move(){

		rand = Random.Range (-1.0f, 1.0f);
		//rand2 = Random.Range (0, 1);



	}
}
