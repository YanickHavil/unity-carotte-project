using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerButton : MonoBehaviour {

	Button button;
	TypeMenu menu = TypeMenu.POWER;
	GameObject panelExtendIcon;

	// Use this for initialization
	void Start () {
		button = gameObject.GetComponent<Button> ();
		panelExtendIcon = GameObject.Find ("PanelExtend");
		button.onClick.AddListener(delegate { onClick(); });
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onClick(){
		panelExtendIcon.GetComponent<PanelExtend> ().changePanelExtend (menu);
		
		
		
		/*
		if (panelExtendIcon.activeSelf) {
			panelExtendIcon.SetActive (false);
		} else {
			panelExtendIcon.SetActive (true);
		}


		*/
	}
}
