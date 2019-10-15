using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour {

	GameObject panelExtendIcon;
	List<GameObject> buttons;

	// Use this for initialization
	void Start () {
	
		buttons = new List<GameObject> ();
		panelExtendIcon = GameObject.Find ("PanelExtend");
		initialiseButtons ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void initialiseButtons(){
		GameObject[] b = GameObject.FindGameObjectsWithTag ("ButtonMenu");

		foreach (GameObject bu in b) {

			bu.GetComponent<MenuButton>().setExtend(panelExtendIcon);
		}

		panelExtendIcon.SetActive (false);
	}

	public void refeshAvailable(){
		panelExtendIcon.GetComponent<PanelExtend> ().refreshAvailable ();
	}
}
