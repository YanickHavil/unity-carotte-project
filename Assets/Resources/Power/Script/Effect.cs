using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Run", 2, 2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Run(){
		DestroyImmediate(gameObject);
	}
}
