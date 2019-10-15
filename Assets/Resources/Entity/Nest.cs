using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Nest : MonoBehaviour {


	public int food = 0;
	public GameObject queen;
	public GameObject worker;
	public GameObject warrior;
	public GameObject farm;
	public List<GameObject> listWorker;
	public List<GameObject> listWarrior;

	public List<Tile> territory;
	public List<Task> tasks;
	public List<GameObject> buildinglist;
	public int respawntime = 60;
	public int compteur;
	List<GameObject> selectionTerritory;
	Attributes att;
	TileMap map;


	// Use this for initialization
	void Start () {
		compteur = respawntime;

		att = GetComponent<Attributes> ();
		map = GameObject.Find ("TileMap").GetComponent<TileMap> ();
		tasks = new List<Task> ();
		territory = new List<Tile> ();
		selectionTerritory = new List<GameObject> ();
		buildinglist = new List<GameObject> ();
		setTerritory ();
		listWorker = new List<GameObject>();
		listWarrior = new List<GameObject>();
		if (queen != null) {
			//Debug.Log ("Queen");
			GameObject queenTemp = Instantiate(queen,gameObject.transform.position,Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
			queenTemp.GetComponent<Attributes>().nest = gameObject;
			queen = queenTemp;
		}

		InvokeRepeating ("nestManager", 2, 2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void nestManager(){
		checkFoodTerritory ();
		if(att.experienceLevel == 1)		gainPassiveFood ();
		//Palier 1
		if (food > 50 && att.experienceLevel == 1) {

			att.experienceLevel += 1;
		} else if (food > 150 && att.experienceLevel == 2) {
			att.experienceLevel += 1;
		
		}else if (food > 500 && att.experienceLevel == 3) {
			att.experienceLevel += 1;
		}

		if (listWorker.Count < att.experienceLevel-1 && compteur >= respawntime) {
			if (worker != null) {
				GameObject workertemp = Instantiate(worker,gameObject.transform.position,Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
				workertemp.GetComponent<Attributes>().nest = gameObject;
				listWorker.Add(workertemp);
				compteur = 0;

			}
		}
		if (listWarrior.Count < att.experienceLevel - 2 && compteur >= respawntime) {
			if(warrior != null)
			{
				GameObject warriortemp = Instantiate(warrior,gameObject.transform.position,Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
				warriortemp.GetComponent<Attributes>().nest = gameObject;
				listWarrior.Add(warriortemp);
				compteur = 0;
			}
		}


		if (tasks.Count > 0) {
			foreach (Task t in tasks) {
				if (listWorker.Count > 0 && t.creature == null) {
					foreach (GameObject g in listWorker) {
						if (g != null) {
							if (!g.GetComponent<Attributes> ().work && !g.GetComponent<Attributes> ().fight) {
								g.GetComponent<WorkManager> ().attributeTask (t);
								Debug.Log ("tache attribué !");
								
								break;
							}
						}

					}
					break;

				}



			}
		}

		if (listWorker.Count == 0) {
				if (tasks.Count > 0) {
					foreach(Task t in tasks){
						if(t.creature == null && t.type == TaskEnum.COLLECTFOOD){
							if(queen != null){
								if(!(queen.GetComponent<Attributes>().work) && !(queen.GetComponent<Attributes>().fight)){
									queen.GetComponent<WorkManager>().attributeTask(t);
									Debug.Log ("tache attribué a la reine!");
									
									break;
								}
							}
						}
					}

				}


		}
		compteur++;
		if (queen == null) {
			DestroyImmediate(gameObject);
		}
	}

	void gainPassiveFood(){
		food += 1;

	}

	void checkFoodTerritory(){
		//Debug.Log ("On cherche de la bouffe");
		foreach (Tile t in territory) {

			if(t.staticEntity != null && t.staticEntity.tag == "Resource"){
				if(t.staticEntity.GetComponent<ResourceManager>().type == TypeResource.FRUIT
				   && !t.staticEntity.GetComponent<ResourceManager>().isEmpty())
				{
					//Debug.Log ("bouffe trouvé pour le nid : " + t.staticEntity);

					addTaskCollect(t.staticEntity);
					//Ajouter une tache pour collecté.
				}
			}
		}
		if (buildinglist.Count < att.experienceLevel-1) {
			addTaskBuildFarm();

		}
	}
	public void removeTask(Task t){
		tasks.Remove (t);
	}
	public void addWorkerToNest(GameObject g){
		listWorker.Add (g);
	}
	public void putFood(int n){
		
		food += n;
		Debug.Log ("Nourriture posé : " + n);
	}
	public int collectFood(int n){
		int res;
		if (n > food) {
			res = food;
			food = 0;
			return res;
		} else {
			food -= n;
			return n;
		}
	}
	public void hideTerritory(){
		if (selectionTerritory.Count > 0) {
			foreach (GameObject g in selectionTerritory) {
				DestroyImmediate(g);
			}
		}

	}
	public void showTerritory(){


		foreach (Tile t in territory) {
			Vector3 v3 = new Vector3 (t.x + map.tileSize / 2, 1.005f, t.y + map.tileSize / 2);

			GameObject selection = Instantiate(Resources.Load("UI/SelectionPanel") as GameObject,v3, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
			selectionTerritory.Add (selection);

		}
	}
	public void helpForCombatInTerritory(GameObject g){
		foreach (GameObject ga in listWarrior) {
			if (ga != null && g != null) {
				ga.GetComponent<Creature> ().enemy = g;
				
			}
		}
		foreach (GameObject ga in listWorker) {
			if (ga != null && g != null) {
				ga.GetComponent<Creature> ().enemy = g;
				
			}
		}
	}
	public void helpForCombat(GameObject g){

		if (listWarrior.Count > 0) {
			foreach (GameObject ga in listWarrior) {
				if (ga != null && g != null) {
					ga.GetComponent<Creature> ().enemy = g;
					
				}
			}
		} else {
			foreach (GameObject ga in listWorker) {
				if (ga != null && g != null) {
					ga.GetComponent<Creature> ().enemy = g;
					
				}
			}
		}
	}

	public void build(GameObject g,Task t){
		map.getMap().GetTileAt((int)t.destination.x,(int)t.destination.z).staticEntity = g;
		g.GetComponent<Attributes> ().nest = gameObject;
		buildinglist.Add (g);
	}

	public bool isInTerritory(Vector3 v3){

		if (v3.x > transform.position.x - 4 && v3.z > transform.position.z - 4) {
			if(v3.x <transform.position.x +4 && v3.z < transform.position.z + 4){
				return true;
			}
		}
		return false;
	}
	void setTerritory(){

		for(int i = -4;i <5;i++){
			for(int j= -4; j <5; j++){
				if(transform.position.x+i > 0 && transform.position.z+j >0
				   && transform.position.x+i< map.size_x-2
				   && transform.position.z+j< map.size_z-2){
					//Debug.Log("Tile at " +((int)transform.position.x+i) + " z = " +((int)transform.position.z+j) );
					
					territory.Add(map.getMap().GetTileAt((int)transform.position.x+i,((int)transform.position.z+j)));
				}
			}
		}
	}

	Tile getEmptyTile(){
		int rand1 = Random.Range(0,territory.Count);

		while (territory [rand1].staticEntity != null) {
			rand1 = Random.Range(0,territory.Count);
		}
		return territory [rand1];



	}
	void addTaskCollect(GameObject cible){
		bool duplicateProtector = false;
		Task t = new Task (cible, TaskEnum.COLLECTFOOD);
		foreach(Task ta in tasks){
			if(t.isSame(ta)){
				duplicateProtector = true;
			}
		}
		if (!duplicateProtector) {
			if(!cible.GetComponent<ResourceManager>().isEmpty()){
				tasks.Add(t);

			}
		}

	}

	void addTaskBuildFarm(){
		Tile tile = getEmptyTile ();


		bool duplicateProtector = false;
		Task t = new Task (farm,TaskEnum.CONSTRUCTION,new Vector3(tile.x +0.5f,0f,tile.y+0.5f));
		foreach(Task ta in tasks){
			if(t.isSame(ta)){
				duplicateProtector = true;
				Debug.Log("meme tache");
			}
		}
		if (!duplicateProtector) {
			Debug.Log ("tache construction");
			
			tasks.Add(t);


		}

	}
}
