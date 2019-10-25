using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {


	
	Button button;
	TypeMenu menu;
	GameObject panelExtendIcon;

	// Use this for initialization
	void Start () {
	
		button = gameObject.GetComponent<Button> ();
		setTypeMenu ();
		if (menu != null) {
			button.onClick.AddListener(delegate { onClick(); });
		}
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

    void setTypeMenu()
    {
        if (gameObject.name == "BuildingButton")
        {
            menu = TypeMenu.BUILDING;
        }
        else if (gameObject.name == "PowerButton")
        {
            menu = TypeMenu.POWER;
        }
        else if (gameObject.name == "CreatureButton")
        {
            menu = TypeMenu.CREATURE;
        }
    }

	public void setExtend(GameObject g){
		panelExtendIcon = g;
	}
}
