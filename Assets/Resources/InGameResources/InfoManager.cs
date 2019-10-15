using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoManager : MonoBehaviour {


	// Info Selection
	string info;


	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnMouseDown(){
		if (!EventSystem.current.IsPointerOverGameObject ()) {

			clickOn ();

		}
	}
	void clickOn(){
		
		refreshInfo ();
		GameObject.Find("Player").GetComponent<Player>().setSelection(gameObject);


		if (tag == "Nest") {
			GetComponent<Nest>().showTerritory();
		}

	}

	public void refreshInfo(){
		if (tag == "Resource") {
			string text = "";
			text += name;
			text += "\n Resource : " + GetComponent<ResourceManager> ().type;
			text += "\n Nb : " + GetComponent<ResourceManager> ().nbResource;
			info = text;
		}

	}
	public string getInfo(){
		return info;
	}
}
