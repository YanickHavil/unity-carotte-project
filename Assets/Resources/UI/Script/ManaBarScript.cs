using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarScript : MonoBehaviour {

	ManaManager manaM;
	RectTransform child;
	float scale = 1.0f;
   

	public GameObject text;
	// Use this for initialization
	void Start () {
		child = gameObject.GetComponent<RectTransform> ();
		manaM = GameObject.Find ("Player").GetComponent<ManaManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		updateMana (); 
	}


	void updateMana(){



		float size = ((float)manaM.mana / (float)manaM.manaMax);
		child.transform.localScale = new Vector3 (1 ,scale * size,child.transform.localScale.z);
		float newValue = (50 *size)-50;

		transform.localPosition = new Vector3(-2f, newValue, 0f);

		text.GetComponent<Text> ().text = manaM.mana + "/" + manaM.manaMax;
		//Debug.Log (difference + "   hey mana");
	}
}
