using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour {


	public float mana = 50;
	public float manaMax = 100;

	public float manaRegen= 5;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("manaManager",1,1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void manaManager(){
		if (mana + manaRegen > 100) {
			mana = manaMax;
		} else {
			mana += manaRegen;
		}



	}
}
