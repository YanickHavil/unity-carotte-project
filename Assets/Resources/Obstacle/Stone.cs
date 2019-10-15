using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {
	
	TileMap tl; 

	// Use this for initialization
	void Start () {
		tl = GameObject.FindWithTag ("TileMap").GetComponent<TileMap> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
