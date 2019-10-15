using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InvokeRepeating("destroyParticule", 5, 5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void destroyParticule()
    {
        DestroyImmediate(gameObject);
    }
}
