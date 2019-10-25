using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MenuCategorie{


	Player player;
	Sprite[] buttons;
	bool[] buttonAvailable;
	int buttonLength = 10;
	public TypeMenu type;

	public MenuCategorie(TypeMenu t){


		// Initialise les images des buttons

		player = GameObject.Find ("Player").GetComponent<Player>();
		buttons = new Sprite[buttonLength];
		buttonAvailable = new bool[buttonLength];
		type = t;
		switch (type) {
		case TypeMenu.BUILDING:

			for(int i = 0;i<buttons.Length;i++){
				buttons[i] = Resources.Load<Sprite>("UI/Buttons/ButtonBuilding" + i.ToString());
				buttonAvailable[i] = isAvailable(t,i);
			}
			break;

		case TypeMenu.POWER:
			
			for(int i = 0;i<buttons.Length;i++){
				buttons[i] = Resources.Load<Sprite>("UI/Buttons/ButtonPower" + i.ToString());
				buttonAvailable[i] = isAvailable(t,i);
			}
			break;
        case TypeMenu.CREATURE:

            for (int i = 0; i < buttons.Length; i++)
            {
                    List<GameObject> listemp = player.getCreatures();

                    if(player.haveCreature() && listemp.Count > i)
                    {
                        buttons[i] = listemp[i].GetComponentInChildren<SpriteRenderer>().sprite;
                    }

                //buttons[i] = Resources.Load<Sprite>("UI/Buttons/ButtonCreature" + i.ToString());
                buttonAvailable[i] = isAvailable(t, i);
            }
            break;

            default:
			break;

		}
	}

	bool isAvailable(TypeMenu t, int i){
		switch (t) {
		case TypeMenu.BUILDING:
			

			switch(i){

			case 0:
				if(player.haveBuilding("Temple")){
					return false;
				}
				else{
					return true;
				}
				break;
			case 1:
				if(player.haveBuilding("Temple")){
					return true;
				}
				else{
					return false;
				}
				break;
			default: 
				
				return false;
			}

			break;
			
		case TypeMenu.POWER:
			
			switch(i){
				
			case 0:
				if(player.haveBuilding("Temple")){
					return true;
				}
				else{
					return false;
				}
			case 1:
				if(player.haveBuilding("Temple")){
					return true;
				}
				else{
					return false;
				}
				
            case 2:
                if (player.haveBuilding("Temple") && player.haveMana(25))
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            case 3:
                if (player.haveBuilding("Temple") && player.haveMana(25))
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
                    default: 
				
				return false;
			}
        case TypeMenu.CREATURE:

            switch (i)
            {

                case 0:

                        return true;

                case 1:
                    if (player.haveBuilding("Temple"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case 2:
                    if (player.haveBuilding("Temple") && player.haveMana(25))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case 3:
                    if (player.haveBuilding("Temple") && player.haveMana(25))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                default:

                    return false;
            }




            default:
			return false;
			
		}
	}

	//Action des buttons par catégorie
	public void getButton(int i, Player player){
		switch (type) {
		case TypeMenu.BUILDING:
			switch(i){
			case 0:
				player.GetComponent<SelectionManager>().setBlueprint (Resources.Load("Buildings/Temple") as GameObject);
				break;
			case 1:
				player.GetComponent<SelectionManager>().setBlueprint (Resources.Load("Buildings/Rocktest") as GameObject);
				break;
			}

			break;
		case TypeMenu.POWER:

			switch(i){
			case 0:
				player.GetComponent<SelectionManager>().setBlueprint (Resources.Load("Power/Prefab/RandomMonster") as GameObject);
				break;
			case 1:
				player.GetComponent<SelectionManager>().setBlueprint(Resources.Load("Power/Prefab/AvatarPower") as GameObject);
				break;
            case 2:
                player.cursorPower(1);
                break;
            case 3:
                player.cursorPower(0);
                break;
                }
			break;
        case TypeMenu.CREATURE:
                
                break;

        default :
			break;
		}

	}

	public Sprite[] GetSprites(){
		return buttons;
	}

	public bool[] getAvailable(){
		refreshAvailable ();
		return buttonAvailable;
	}

	void refreshAvailable(){
		switch (type) {
		case TypeMenu.BUILDING:
			
			for(int i = 0;i<buttons.Length;i++){
				buttonAvailable[i] = isAvailable(type,i);
			}
			break;
			
		case TypeMenu.POWER:
			
			for(int i = 0;i<buttons.Length;i++){
				buttonAvailable[i] = isAvailable(type,i);
			}
			break;

        case TypeMenu.CREATURE:
            for (int i = 0; i < buttons.Length; i++)
            {
                buttonAvailable[i] = isAvailable(type, i);
            }
            break;

            default:
			break;
			
		}
	}





}
