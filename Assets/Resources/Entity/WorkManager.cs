using UnityEngine;
using System.Collections;

public class WorkManager : MonoBehaviour {


	//AntiStuck Var
	bool coroutine = false;
	int cptStuck = 0;
	public Task t;

	Attributes att;
	MovementManager movement;


	TileMap tl;


	Player player;

	public GameObject progressBar;

	bool tokenWork = true;
	bool phase2 = false;
	bool phase3 = false;

	//Inventory
	private GameObject transportedGbj;

	// Use this for initialization
	void Start () {
		tl = GameObject.FindWithTag ("TileMap").GetComponent<TileMap> ();

		movement = GetComponent<MovementManager> ();
		att = GetComponent<Attributes> ();
		movement = GetComponent<MovementManager> ();
		player = GameObject.Find ("Player").GetComponent<Player>();
		InvokeRepeating ("workManager",0.5f,0.5f);


		progressBar = Instantiate(Resources.Load("UI/ProgressBar"), Vector3.zero, Quaternion.Euler(new Vector3(-90f,0,0))) as GameObject;
		progressBar.transform.parent = transform;
		progressBar.transform.localPosition = new Vector3 (0, 0, 0.27f);
		progressBar.transform.localRotation = Quaternion.Euler(new Vector3(-90.0f,0.0f,0.0f));
		progressBar.transform.localScale = new Vector3 (0.3f, 0.1f, 0.7f);

		progressBar.GetComponent<ProgressBarScript> ().init ();

		progressBar.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void workManager(){

		if (!att.isFighting ()) {
			if (t != null && tokenWork) {
				tokenWork = false;
				att.setWork(true);

				makeTask ();
			}
		} else {
			if(att.nest != null){
				removeTaskNest(att.nest);
			}
		}

		if (t != null) {
			if (t.type == TaskEnum.CONSTRUCTION) {
				
				if(movement.isHere(t.destination)){
					//IA 
					if(att.nest != null){
						GameObject farmtemp = Instantiate (t.cibled,
						                                 new Vector3 (t.destination.x,1.5f,t.destination.z),
						                                   Quaternion.Euler(new Vector3(90,0,0))) as GameObject;

						att.nest.GetComponent<Nest>().build(farmtemp,t);
						removeTaskNest(att.nest);

					}
				}
				else{

					//Debug.Log("aps arriver");
				}
				 

			}
            else if(t.type == TaskEnum.SEED)
            {
                if(t.destination != null)
                {
                    if (movement.isHere(t.destination) && coroutine != true)
                    {
                        StartCoroutine(Seed(t.gobjconstruct));
                    }
                }
            }
			else if (t.type == TaskEnum.TAKE) {
				if (t.destination != null && phase2){
					if (movement.isHere (t.destination)) {
						//Debug.Log ("destination = " + t.destination);
						if (putOnTile (t.destination)) {
							removeTask ();

						} else {
							if (transportedGbj != null) {

							} else {
								phase2 = false;
								bringToStock (transportedGbj);
							}

						}
					}
				}
			}

            

		}
		antiStuck ();

	}

	void makeTask(){
		switch(t.type){

		case TaskEnum.CONSTRUCTION:
			movement.moveTo(t.destination);
			break;
		case TaskEnum.COLLECTFOOD:
			movement.moveTo(t.cibled.transform.position);
			break;
		case TaskEnum.TAKE:
			movement.moveTo (t.cibled.transform.position);
			break;

		case TaskEnum.CUT:
			movement.moveTo (t.cibled.transform.position);
			break;

        case TaskEnum.SEED:
            movement.moveTo(t.destination);
            break;
        case TaskEnum.EAT:
            Debug.Log("launch movement eat");
            movement.moveTo(t.cibled.transform.position);
            break;
            default :
			tokenWork = true;
			break;
		}

	}


	void antiStuck(){
		if (!att.isMoving () && !coroutine && t!=null && att.isWorking()) {
			cptStuck++;
			if (cptStuck >= 10) {
				cptStuck = 0;
				refreshWork ();
			}
		} else {
			cptStuck = 0;
		}
	}
	public void cancelTask(){
		StopAllCoroutines ();
		progressBar.SetActive(false);

		refreshWork ();
	}

	void refreshWork(){
		phase2 = false; 
		phase3 = false;
		att.setWork(false);
		tokenWork = true;
		t.creature = null;
		t = null;

	}
	void removeTask(){
		player.removeTask (t);
		phase2 = false; 
		phase3 = false;
		att.setWork(false);
		tokenWork = true;
		t = null;
	}

	void removeTaskNest(GameObject nest){
		nest.GetComponent<Nest> ().removeTask (t);
		phase2 = false;
		phase3 = false;
		att.setWork (false);
		tokenWork = true;
		t = null;
	}

	IEnumerator Collect(GameObject g) {
		coroutine = true;
		//Force de collect la plus basse 5, on suppose poru le moment que les resource ont 100 pv.
		progressBar.SetActive(true);

		Attributes attCibled = g.GetComponent<Attributes> ();
		float time = 100; // Référence pour le temps que ça prend
		float multiplicator = 1.0f; //Le multiplicateur qui fera varié le temps

		Ability collectAb = att.getAbility (AbilityType.COLLECT);
		int lvlCollect = 1;
		if (collectAb != null) {
			lvlCollect = collectAb.lvl;
		}
		else{
			Debug.Log("bug IENUMERATOR COLLECT : pas d'abilité collect");
		}

		float lvlDifficulty = g.GetComponent<ResourceManager> ().difficultyCollect;
		multiplicator = lvlDifficulty / lvlCollect;
		time = time * multiplicator;


		int cpt = 1;
		for(; cpt < time;){
			int percent = (int)(((cpt/time)*100));
			//Debug.Log ("étape collect : " + percent);

			progressBar.GetComponent<ProgressBarScript>().refresh(percent);
			cpt++;
			yield return new WaitForSeconds(0.1f);

		}


		//yield return new WaitForSeconds(2);
		if (g != null) {

            if (att.numPlayer != -1)
            {
                g.GetComponent<ResourceManager>().collectResource(null);
                
            }
            else
            {
                g.GetComponent<ResourceManager>().collectResource(gameObject);

            }
            

			/*
			int nb = g.GetComponent<ResourceManager>().collectResource();
			TypeResource t = g.GetComponent<ResourceManager>().type;
			att.invent.addResource(new Resource(nb,t));
			*/
		}

		//Fin de la collect
		progressBar.SetActive(false);
		if (att.numPlayer != 1) {
			GameObject t = att.nest;
			if (t != null) {
				removeTaskNest (t);
			}
		} else {
            Debug.Log("remove task algo collect");
			removeTask ();
		}

		coroutine = false;
		//bringToBase();
	}

	IEnumerator Cut(GameObject g){
		coroutine = true;

		progressBar.SetActive(true);
		Attributes attCibled = g.GetComponent<Attributes> ();
		for(; g!=null >0;){
			int percent = (int)(((attCibled.maxLife - attCibled.life)*100)/attCibled.maxLife);
			//Debug.Log ("étape collect : " + percent);

			progressBar.GetComponent<ProgressBarScript>().refresh(percent);
			attCibled.receivedDamage (1, gameObject);
			if ((attCibled.life - 1) <= 0) {
				attCibled.autoTaskDrop = true;
			}
			yield return new WaitForSeconds(0.2f);

		}
		//Fin
		progressBar.SetActive(false);
		if (att.numPlayer != 1) {
			GameObject t = att.nest;
			if (t != null) {
				removeTaskNest (t);
			}
		} else {
			removeTask ();
		}

		coroutine = false;


	}

    IEnumerator Eat(GameObject g)
    {
        coroutine = true;
        progressBar.SetActive(true);
        Debug.Log("Seed making");
        int max = 100;
        int progression = 100; // La progression va décrémenté a 0 c'est fini
        int percent = 0; // pourcentage

        for (; percent < 100;)
        {

            percent = (int)(max - progression);

            progressBar.GetComponent<ProgressBarScript>().refresh(percent);

            progression -=  10; // arbitraire



            yield return new WaitForSeconds(0.5f);

        }

        //Va manger la quantité de resource : 1 fruit = 1 hunger pour le moment
        Debug.Log("eat miam");
        att.eat(g.GetComponent<ResourceManager>().nbResource, g.GetComponent<ResourceManager>().ratioHungerFood);
        DestroyImmediate(g);

        //Fin
        progressBar.SetActive(false);

        removeTask();

        coroutine = false;



    }
    //Plantation de graine
    //Gameobject est la plante en paramètre
    IEnumerator Seed(GameObject g)
    {
        coroutine = true;
        progressBar.SetActive(true);
        Debug.Log("Seed making");
        int max = 100;
        int progression = 100; // La progression va décrémenté a 0 c'est fini
        int percent = 0; // pourcentage
        
        for (; percent < 100;)
        {
            
            percent = (int)(max - progression);
            //Debug.Log ("étape collect : " + percent);
            //Debug.Log("percent : " + percent);
            progressBar.GetComponent<ProgressBarScript>().refresh(percent);

            Ability ab = att.getAbility(AbilityType.SEED);
            progression -= (ab.lvl*2) ;
            //Debug.Log("progression " + progression);


            yield return new WaitForSeconds(0.5f);

        }
        //Fin
        progressBar.SetActive(false);
        GameObject plant = Instantiate(t.gobjconstruct,
            new Vector3(t.destination.x+0.5f, 1.5f, t.destination.z+0.5f),
            Quaternion.Euler(new Vector3(90, 0, 0))) as GameObject;
        Tile tile = tl.getMap().GetTileAt((int)t.destination.x, (int)t.destination.z);
        tile.staticEntity = plant;




        removeTask();

        coroutine = false;


    }

    bool bringToStock(GameObject gbjtransport){
		if (att.numPlayer != 1) {
			GameObject t = att.nest;
			if (t != null) {
				movement.moveTo (t.transform.position);
				//return true;
			}
		} else {
			Tile tile = player.getStockTile (gbjtransport);
			if (tile != null) {
				Vector3 v3 = new Vector3 (tile.x, 1.0f, tile.y);
				//Debug.Log ("v3 = " + v3);

				movement.moveTo(v3);
				t.destination = v3;
				phase2 = true;

				return true;
			} else {
				return false;
			}
		}
		return false;
	}
		
	bool putOnTile(Vector3 pos){
		Tile tile = tl.getMap ().GetTileAt ((int)pos.x, (int)pos.z);
		if (tile.staticEntity != null )  {

			if (!tile.staticEntity.GetComponent<ResourceManager> ().addStackToStock (t.cibled)) {
				bringToStock (t.cibled);
				return false;
			} else {
				GameObject gbj = transportedGbj;
				transportedGbj = null;
				Destroy (gbj);
				return true;
			}

		} else {

			t.cibled.transform.parent = null;
			t.cibled.transform.position = new Vector3 (pos.x + 0.5f , 1.2f, pos.z +0.5f);
			tile.staticEntity = t.cibled;
			return true;
		}

	}


	void leaveOnNearlyTile(Vector3 v3,GameObject obj){
		obj.transform.position = v3;

	}
	void leaveOnNearlyRandomTile(GameObject obj){
		Tile t = att.getNearEmptyTile ();
		Vector3 v3 = new Vector3 (t.x + tl.tileSize / 2, 1.2f, t.y + tl.tileSize / 2);
		obj.transform.position = v3;
		t.staticEntity = obj;
	}


	void OnTriggerEnter(Collider other) {
		if (t != null) {
			//Pour la collecte
			if(other.gameObject == t.cibled && !phase2 && t.type == TaskEnum.COLLECTFOOD){
				movement.stopEntity();
				Debug.Log ("colision Collect of Resource");
				phase2 = true;
				StartCoroutine(Collect(other.gameObject));
			}
			if(other.gameObject == t.cibled && !phase2 && t.type == TaskEnum.CUT){
				movement.stopEntity();
				Debug.Log ("colision Cut/Mine of Resource");
				phase2 = true;
				StartCoroutine(Cut(other.gameObject));
			}
            if (other.gameObject == t.cibled && !phase2 && t.type == TaskEnum.EAT)
            {
                movement.stopEntity();
                Debug.Log("Eat colision");
                phase2 = true;
                StartCoroutine(Eat(other.gameObject));
            }
            //Pour rapporter des resources aux stockpiles
            if (other.gameObject == t.cibled && !phase2 && t.type == TaskEnum.TAKE) {
				movement.stopEntity();
				Debug.Log ("colision TAKE");
				Vector3 posOrigin = other.gameObject.transform.position;
				other.gameObject.transform.SetParent (transform);
				other.gameObject.transform.localPosition = new Vector3 (0, 0, 0);
				t.icon.GetComponent<Renderer> ().enabled = false;


				transportedGbj = other.gameObject;
				/*
				ResourceManager rm = other.gameObject.GetComponent<ResourceManager> ();
				TypeResource type = rm.type;
				int nb = rm.nbResource;
				att.invent.addResource (new Resource (nb, type));
				*/
				tl.getMap ().GetTileAt ((int)posOrigin.x, (int)posOrigin.z).staticEntity = null;

				if (!bringToStock (transportedGbj)) {

					other.gameObject.transform.SetParent (null);
					leaveOnNearlyRandomTile(other.gameObject);
					removeTask ();
				} 

			}
			if(att.nest ==null){
				if(player.getTemple() != null && phase3){
					if(other.gameObject == player.getTemple().gameObject){
						if(t.type == TaskEnum.COLLECTFOOD){
							player.getTemple().GetComponent<Temple>().putResource(TypeResource.GRASS, 2);
							movement.stopEntity();
							removeTask();
						}
					}
				}
			}
			else{
				/*
				if(phase3){
					if(other.gameObject == att.nest){
						if(t.type == TaskEnum.COLLECTFOOD){
							att.nest.GetComponent<Nest>().putFood(att.invent.getResource().nb);
							movement.stopEntity();
							removeTaskNest(att.nest);
						}
					}
				}*/
			}

			
			
		}
	}

	void OnTriggerStay(Collider other) {
		if (t != null) {
			/*

			if (other.gameObject == t.cibled && t.type == TaskEnum.TAKE) {
				Debug.Log ("collision take sans phase avec " + other.gameObject);
			}*/
			if(other.gameObject == t.cibled && !phase2 && t.type == TaskEnum.COLLECTFOOD ){
				movement.stopEntity();
				Debug.Log ("collision");
				phase2 = true;
				StartCoroutine(Collect(other.gameObject));
			}
			if(other.gameObject == t.cibled && !phase2 && t.type == TaskEnum.CUT){
				movement.stopEntity();
				Debug.Log ("Cut/Mine of Resource");
				phase2 = true;
				StartCoroutine(Cut(other.gameObject));
			}
            if (other.gameObject == t.cibled && !phase2 && t.type == TaskEnum.EAT)
            {
                movement.stopEntity();
                Debug.Log("Eat colision");
                phase2 = true;
                StartCoroutine(Eat(other.gameObject));
            }
            if (other.gameObject == t.cibled && !phase2 && t.type == TaskEnum.TAKE) {
				movement.stopEntity();
				Debug.Log ("colision TAKE");
				Vector3 posOrigin = other.gameObject.transform.position;
				other.gameObject.transform.SetParent (transform);
				other.gameObject.transform.localPosition = new Vector3 (0, 0, 0);
				t.icon.GetComponent<Renderer> ().enabled = false;
				transportedGbj = other.gameObject;

				/*
				ResourceManager rm = other.gameObject.GetComponent<ResourceManager> ();
				TypeResource type = rm.type;
				int nb = rm.nbResource;
				att.invent.addResource (new Resource (nb, type));
				*/

				tl.getMap ().GetTileAt ((int)posOrigin.x, (int)posOrigin.z).staticEntity = null;

				if (!bringToStock (transportedGbj)) {

					other.gameObject.transform.SetParent (null);
					leaveOnNearlyRandomTile(other.gameObject);
					removeTask ();
				} 

			}
			if(att.nest ==null){
				if(player.getTemple() != null && phase3){
					if(other.gameObject == player.getTemple().gameObject){
						if(t.type == TaskEnum.COLLECTFOOD){
							player.getTemple().GetComponent<Temple>().putResource(TypeResource.GRASS, 2);
							movement.stopEntity();
							removeTask();
						}
					}
				}
			}
			else{
				/*
				if(phase3){
					if(other.gameObject == att.nest){
						if(t.type == TaskEnum.COLLECTFOOD){
							att.nest.GetComponent<Nest>().putFood(att.invent.getResource().nb);
							movement.stopEntity();
							removeTaskNest(att.nest);						
						}
					}
				}*/
			}
			
			
		}
	}
	public bool attributeTask(Task ta){
		//Verification
		ta.creature = gameObject;
		t = ta;
        tokenWork = true;
        phase2 = false;
		return true;
	}

	void refreshProgressBar(float f){
		
	}
}
