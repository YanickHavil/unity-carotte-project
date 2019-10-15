using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;

public class PanelExtend : MonoBehaviour {

	GameObject[] buttons;
	MenuCategorie categorie;
	Player player;
	// Use this for initialization
	void Start () {
		buttons = GameObject.FindGameObjectsWithTag ("ButtonCat").OrderBy (bu => bu.name).ToArray ();
		player = GameObject.Find ("Player").GetComponent<Player>();
        InvokeRepeating("checkRefreshButton", 0.5f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void checkRefreshButton()
    {
        refreshAvailable();
    }

	public void refreshAvailable(){
		if (categorie != null) {
			bool[] available = categorie.getAvailable();
			for (int i = 0; i < available.Length; i++) {
				//Si le joueur peut utiliser le bouton 
				if(available[i]){

					buttons[i].GetComponent<Image>().CrossFadeAlpha(1,0,true);

					Button button =  buttons[i].GetComponent<Button>();
					button.interactable = true;

					//Retire les listeners et ajoute le bon.
					button.onClick.RemoveAllListeners();
					button.onClick.AddListener(delegate {effectButton(button.gameObject);});	

				}
				else{
					buttons[i].GetComponent<Image>().CrossFadeAlpha(0.5f,0,true);
					buttons[i].GetComponent<Button>().interactable = false;
				}
			}
		}
	}


	public void changePanelExtend(TypeMenu menu){

		if (categorie != null) {
			if (categorie.type == menu) {
				categorie = null;
				gameObject.SetActive(false);
			}
			else{
				initializePanelExtend(menu);
			}
		} else {
			initializePanelExtend(menu);

		}




		/*
		switch (menu) {
		case "Building":
	
			for(int i = 0;i<buttons.Length;i++){
				Sprite button = Resources.Load<Sprite>("UI/Buttons/ButtonBuilding" + i.ToString());
				//Debug.Log (button);
				if(button == null ){
					buttons[i].GetComponent<Image>().sprite = null;
					buttons[i].GetComponent<Image>().CrossFadeAlpha(0,0,true);
					buttons[i].GetComponent<Button>().interactable = false;
				}
				else{
					buttons[i].GetComponent<Image>().sprite = button ;
					buttons[i].GetComponent<Button>().interactable = true;
					buttons[i].GetComponent<Image>().CrossFadeAlpha(1,0,true);
				}
			}
			break;

		default :
			break;

		}
		*/
	}
	
    public void refreshButtons()
    {

    }
    


	void initializePanelExtend(TypeMenu menu){
		categorie = new MenuCategorie (menu);
		
		if (categorie != null) {
			
			if (!gameObject.activeSelf) {
				gameObject.SetActive(true);
				
			}
			Sprite[] spritesCat = categorie.GetSprites();
			bool[] available = categorie.getAvailable();
			for (int i = 0; i < spritesCat.Length; i++) {
				
				if(spritesCat[i] == null ){
					buttons[i].GetComponent<Image>().sprite = null;
					buttons[i].GetComponent<Image>().CrossFadeAlpha(0.5f,0,true);
					buttons[i].GetComponent<Button>().interactable = false;
					
				}
				else{
					//Mettre a jour l'image du button
					buttons[i].GetComponent<Image>().sprite = spritesCat[i];


					//Si le joueur peut utiliser le bouton 
					if(available[i]){

						buttons[i].GetComponent<Image>().CrossFadeAlpha(1,0,true);

						Button button =  buttons[i].GetComponent<Button>();
						button.interactable = true;

						//Retire les listeners et ajoute le bon.
						button.onClick.RemoveAllListeners();
						button.onClick.AddListener(delegate {effectButton(button.gameObject);});	
					}
					else{
						buttons[i].GetComponent<Image>().CrossFadeAlpha(0.5f,0,true);
						buttons[i].GetComponent<Button>().interactable = false;
					}


			
				}
				
			}
		} else {
			gameObject.SetActive(false);
		}
	}
	void effectButton(GameObject gbj){
		categorie.getButton (ArrayUtility.IndexOf(buttons,gbj),player);
	}


}
