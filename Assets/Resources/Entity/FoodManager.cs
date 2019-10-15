using UnityEngine;
using System.Collections;

public class FoodManager : MonoBehaviour {

	public bool dontEat = false;
	public bool vegetarian = false;
	public bool carnivorous = false;
	bool tokenEat =true;
	//public string[] typeOfFood;
    public TypeResource[] foodTypes;
    Attributes att;

	GameObject Foodtarget; // Cible pour manger
	GameObject CollectTarget;

	TileMap map;
	MovementManager mvt;

	// Use this for initialization
	void Start () {
		att = GetComponent<Attributes> ();
		mvt = GetComponent<MovementManager> ();
		InvokeRepeating ("foodManager",1,5 );
		map = att.getMap ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void foodManager(){
		if (dontEat) {

		} else {
			att.changeHunger(-0.2f);
			Instantiate (Resources.Load("Icon/LooseFood"),new Vector3 (transform.position.x,transform.position.y+0.1f,transform.position.z),Quaternion.identity);

			if (att.getHunger() < 10) {
				//Debug.Log (gameObject.name + " a faim " + "  typeofFood : " + typeOfFood[0]);

				att.setHungry(true);

				if(!att.isWorking() && !att.isFighting()){
					if(att.nest != null){
						if(att.nest.GetComponent<Nest>().food > 0){

						}
					}
					else{
						Debug.Log ("try to find food");

						if (vegetarian) {

							findStaticFood (); // Recherche uniquement dans les staticEntity sur chaque Tile.
						} else if (carnivorous) {

							findFoodEntity (); // Recherche sur toutes les entités qui bougent.
						}
					}


				}

				//findFoodEntity("Pollen");
				//findFood();
			} else {
				att.setHungry(false);
			}
			/*
		if(stats.hunger <0){
			stats.starvation = true;
		}
		*/

		}

		
	}

	void findStaticFood(){
		/*
		if (stats.nest != null) {
			if(stats.nest.GetComponent<Nest>().foodInventory > 0){
				changeFoodTarget(stats.nest);
			}
		}*/
		if (Foodtarget == null) {
			
			Tile t;
			
			//Debug.Log ("pos x creature = " + Mathf.FloorToInt (transform.position.x));
			//Debug.Log ("pos y creature = " + Mathf.FloorToInt (transform.position.z));

 				//Taille du carré de vision en haut a droite
			int posxMax = Mathf.FloorToInt (transform.position.x) + att.rayVision;
			int posyMax = Mathf.FloorToInt (transform.position.z) + att.rayVision;
			
			for(int x = Mathf.FloorToInt (transform.position.x) - att.rayVision; x <= posxMax; x++){//Variable début de boucle, on commence par la Tile la plus en bas a gauche
				if (Foodtarget != null) break;
				for(int y = Mathf.FloorToInt (transform.position.z) - att.rayVision;y <= posyMax; y++){
					
					
					if((x < 0 || y < 0) || (x >= map.size_x || y >= map.size_z)){
						//Si on est en dehors de la map on ne cherche pas.
					}
					else{
						
						t = att.getTileAt(x,y);
						if(t.staticEntity != null){
							
							//foreach(string str in typeOfFood){ // on parcourt les repas possible : Plantes ou autres.
                            // Nouvel algo avec comme tag resource pour les static food. (toujours le meme pour le moment)
								if(t.staticEntity.tag == "Resource"){

                                    //On parcours les type de nourriture consommable par la créature
                                    foreach(TypeResource type in foodTypes)
                                {
                                    if(t.staticEntity.GetComponent<ResourceManager>().getTypeResource() == type)
                                    {
                                        //pour la meat on la mange direct
                                        if(type == TypeResource.MEAT)
                                        {
                                            Debug.Log("trouver nourriture");
                                            changeEatTarget(t.staticEntity);
                                            return;
                                        }
                                        else
                                        {
                                            Debug.Log("trouver nourriture");
                                            changeCollectTarget(t.staticEntity);
                                            return;
                                        }

                                    }

                                }

									/*
									if(str == "Tree"){
										if(t.staticEntity.GetComponent<Tree>().nbFruit > 0){
											changeCollectTarget(t.staticEntity);
											Debug.Log("trouver Collect");
											return;
										}
										
										
									}
									else{
										changeFoodTarget(t.staticEntity);
										Debug.Log("trouver");
										return;
									}*/
									
								}
								
							//}
							
							
						}
					}
				}
			}
			if(Foodtarget ==null && vegetarian){
				mvt.ForceRandomMove();		
				
			}
		}
		
		
	}

	void findFoodEntity(){
		
		findStaticFood ();
        string str = "Creature"; // Toute créature a le tag créature.

		if (Foodtarget == null) {
			
			//foreach(string str in typeOfFood){
				GameObject[] gobj;
				if(str != null){
					gobj = GameObject.FindGameObjectsWithTag(str);

					for(int i = 0 ; i < gobj.Length; i++){
						if (Foodtarget != null) break;
						if(Mathf.Abs(gobj[i].transform.position.x - transform.position.x) < att.rayVision){
							if(Mathf.Abs(gobj[i].transform.position.z - transform.position.z) < att.rayVision){
								if(gobj[i] != gameObject){
									Foodtarget = gobj[i];
									Debug.Log ("trouver enemy");
									GetComponent<Creature>().enemy = gobj[i];
									break;
								}

							}
							
						}
					}
				}

				
				

			//}
		}
		if(Foodtarget ==null){
			Debug.Log("jebouge");
			mvt.ForceRandomMove();		
			
		}
		Debug.Log("food target =" + Foodtarget);
	}

	void changeCollectTarget(GameObject tg){
		
		//CollectTarget = tg;
		//Foodtarget = tg;

		//mvt.moveTo (tg.transform.position.x, tg.transform.position.z);
		//Debug.Log (tg.transform.position.x + " // " +tg.transform.position.z );
		GetComponent<WorkManager>().attributeTask(new Task(tg,TaskEnum.COLLECTFOOD));
	}

    void changeEatTarget(GameObject g)
    {
        GetComponent<WorkManager>().attributeTask(new Task(g, TaskEnum.EAT));
    }

	/*
	IEnumerator collect() {
		//print(Time.time);
		yield return new WaitForSeconds(2);
		if ( att.isFighting() == false) {
			if(CollectTarget != null){
				att.eat(CollectTarget.GetComponent<ResourceManager>().collectResource());
				Instantiate (Resources.Load("Icon/IconFood"),new Vector3 (transform.position.x,transform.position.y,transform.position.z),Quaternion.identity);
				//Debug.Log ("Eat for " + CollectTarget.GetComponent<Resource>().collectResource);
				Debug.Log ("Eat who " + CollectTarget.name);
				//Destroy(Foodtarget);
				//poopTime = true;
				CollectTarget = null;
				Foodtarget = null;
			}
			else{
				Debug.Log("Mon repas a disparu");
				
			}
			
		}
		tokenEat = true;
		
	}*/


	void OnTriggerEnter(Collider other){
		if (CollectTarget != null && att.isFighting() == false) {
			if(other.gameObject == CollectTarget && tokenEat){
				tokenEat = false;
				mvt.stopEntity();
				
				//StartCoroutine(collect());
			}
		}
		
	}

}
