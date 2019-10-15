using UnityEngine;
using System.Collections;

public class Temple : MonoBehaviour {
	
	float stone = 0;
	float fruit = 0;
	float meat = 0;
	float twig = 0;
	float grass = 0;
	float influence = 0;


	// Use this for initialization
	void Start () {

		//InvokeRepeating("",1,1);
		GetComponent<Attributes>().addInfo(info());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void initialize(Player p){
		if (p == null) {

		} else {

		}
	}
	public string info(){
		string text = "";
		text += "\nStone : " + stone;
		text += " | Fruit : " + fruit;
		text += "\nMeat : " + meat;
		text += " | Twig : " + twig;
		text += "\nInfluence : " + influence;
		return text;
	}
	void refreshInfo(){
		GetComponent<Attributes> ().InitializeInfo ();
		string text = "";
		text += "\nStone : " + stone;
		text += " | Fruit : " + fruit;
		text += "\nMeat : " + meat;
		text += " | Twig : " + twig;
		text += "\nInfluence : " + influence;
		GetComponent<Attributes> ().addInfo (text);
	}

	public void putResource(TypeResource type , int n){
		switch (type) {
		case TypeResource.FRUIT:
				fruit += n;
			refreshInfo();
			break;
		case TypeResource.MEAT:
				meat += n;
			refreshInfo();

			break;

		case TypeResource.STONE:
				stone += n;
			refreshInfo();

			break;

		case TypeResource.WOOD:
				twig += n;
			refreshInfo();

			break;
		case TypeResource.GRASS:
			grass += n;
			refreshInfo();
			break;

		}
	}

}
