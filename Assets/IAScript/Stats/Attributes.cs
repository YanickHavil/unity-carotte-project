using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Attributes : MonoBehaviour {

	//The map
	TileMap map;
	public float Yoffset;

	//Drop
	public GameObject[] drops;
	public bool autoTaskDrop = false;
	//Ability
	public List<Ability> abilities;
	
	//The player
	Player player;
	public int numPlayer = -1;

	//name of Gameobject
	public string name;

	// Info Selection
	string info;

	//Attributes
	public float life = 5;
	public float maxLife = 10;
	public int rayVision = 7;
	public float hunger = 10;
	public float hungerMax = 20;
	public int experienceLevel = 1;

	public float influencePoint = 0;

	//Inventory
	public Inventory invent;

	//Nest
	public GameObject nest;

	//Statuts IA
	public bool fight = false;
	public bool hungry = false;
	public bool work = false;
	public bool idle = true;
	public bool move = false;

	public GameObject lifebar = null;






	// Use this for initialization
	void Start () {
		map = GameObject.Find ("TileMap").GetComponent<TileMap> ();
		invent = new Inventory ();

		if(gameObject.tag != "Resource"){
			Debug.Log ("vision sur " + gameObject);
			//A changer de place dans VisionManager
			InvokeRepeating ("visionManager",0.3f,0.3f );
		}
		InvokeRepeating ("DeadManager", 0.2f, 0.2f);
		player = null;
		InitializeInfo();
	}
	
	// Update is called once per frame
	void Update () {
	

		//selectionEntity ();

	}

    public void setListAbilities()
    {
        abilities = new List<Ability>();
    }
	void DeadManager(){
		if (life <= 0) {
			Entitydie();
		}
	}

	void Entitydie(){
		//Drop
		drop();
		Destroy (gameObject);

		//Placer le drop

	}
	void visionManager(){
		// Si la vision est partagé avec le joueur
		if (numPlayer != -1) {
			int xPos = Mathf.FloorToInt (transform.position.x);
			int yPos = Mathf.FloorToInt (transform.position.z);
			
			int xMax = xPos + rayVision;
			int yMax = yPos + rayVision;
			
			for (int x = xPos - rayVision; x <= xMax; x++) {
				for (int y = yPos - rayVision; y <= yMax; y++) {
					if ((x < 0 || y < 0) || (x >= map.size_x || y >= map.size_z)) {
						
					} else {
						if ((Mathf.Abs (x - xPos) + Mathf.Abs (y - yPos)) <= rayVision) {
							map.showTile (x, y);
						}
					}
				}
			}

		} else {
			if(GameObject.Find("Player").GetComponent<Player>().isIconBuilding(gameObject)){

			}
			else{
				if(GetComponentInChildren<Renderer>() != null){
					if(map.getMap().GetTileAt(Mathf.FloorToInt (transform.position.x),Mathf.FloorToInt (transform.position.z)).show){
						GetComponentInChildren<Renderer>().enabled = true;
					}
					else{
						GetComponentInChildren<Renderer>().enabled = false;
					}
				}
                
			}

		}
		//Tile t = map.getMap ().GetTileAt (Mathf.FloorToInt (transform.position.x), Mathf.FloorToInt (transform.position.z));


		//Rien a voir mais pour régler le bug du transport
		/*
		if (transform.parent != null) {
			transform.localPosition = Vector3.zero;
		}
		*/
	}

	void selectionEntity(){
		if (!EventSystem.current.IsPointerOverGameObject ()) {
			if (Input.GetMouseButtonDown (0)) {

			}
		}

			
	}
    
	void OnMouseEnter() {
        if (GameObject.Find("Player").GetComponent<SelectionManager>().inSelectionPower && tag == "Creature")
        {
            GameObject.Find("Player").GetComponent<SelectionManager>().changeEnabledCursor();
        }
        if (!EventSystem.current.IsPointerOverGameObject ()) {
			//GameObject.Find ("Player").GetComponent<Player> ().changeCursor (tag);

		}
        
	}
	void OnMouseExit() {
        //GameObject.Find ("Player").GetComponent<Player> ().changeCursor ("");
        if (GameObject.Find("Player").GetComponent<SelectionManager>().inSelectionPower && tag == "Creature")
        {
            GameObject.Find("Player").GetComponent<Player>().cursorDisabledPower();
        }

    }
    void OnMouseDown(){
        GameObject player = GameObject.Find("Player");

        if (player.GetComponent<SelectionManager>().inSelectionPower && player.GetComponent<ManaManager>().mana > 25)
        {
            GameObject effect = null;
            if (player.GetComponent<SelectionManager>().influType == InfluenceType.SUN)
            {
                effect = Instantiate(Resources.Load("Power/Prefab/SunPower") as GameObject, Vector3.zero, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            }
            else
            {
                effect = Instantiate(Resources.Load("Power/Prefab/RainPower") as GameObject, Vector3.zero, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                
                effect.transform.localPosition = new Vector3(0f,01f,0.7f);

            }
            effect.transform.parent = gameObject.transform;

            if (player.GetComponent<SelectionManager>().influType == InfluenceType.SUN)
            {
                effect.transform.localPosition = Vector3.zero;
            }
            else if(player.GetComponent<SelectionManager>().influType == InfluenceType.RAIN)
            {
                effect.transform.localPosition = new Vector3(0f, 01f, 0.7f);
            }

            effect.transform.localRotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
            GetComponent<InfluenceManager>().tryInfluence(GameObject.Find("Player").GetComponent<SelectionManager>().influType);
            player.GetComponent<ManaManager>().mana -= 25;
            //Instancier l'animation

        }
		else if (!EventSystem.current.IsPointerOverGameObject ()) {

			clickOn ();

		}
	}
	void clickOn(){
		/*
		if (tag == "Resource") {
			// ajoute la tache collecté
			GameObject.Find("Player").GetComponent<Player>().addTask(gameObject,TaskEnum.COLLECTFOOD);
		} else {
			GameObject.Find("Player").GetComponent<Player>().setSelection(gameObject);

		}*/

		refreshInfo ();
		Tile t = getCurrentTile ();
		//Vector3 v3 = new Vector3 (t.x + map.tileSize / 2, 1.005f, t.y + map.tileSize / 2);
		//GameObject selectionP = Instantiate(Resources.Load("UI/SelectionPanel") as GameObject,v3, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;

		GameObject.Find("Player").GetComponent<Player>().setSelection(gameObject);
        /*
        if (tag == "Nest") {
			GetComponent<Nest>().showTerritory();
		}
        */
	}

	public void receivedDamage(int damage, GameObject gameObject){

		life -= damage;
		if (lifebar != null) {
			showLifeBar ();

		}
		if (tag != "Resource") {
			GetComponentInChildren<Renderer> ().material.color = Color.red;

		}
		if (GetComponent<Creature> () != null) {
			GetComponent<Creature> ().enemy = gameObject;

		}
		if (nest != null) {
			if(nest.GetComponent<Nest>().isInTerritory(transform.position)){
				nest.GetComponent<Nest>().helpForCombatInTerritory(gameObject);

			}else{
				nest.GetComponent<Nest>().helpForCombat(gameObject);
			}				

		}

	}

	public void eat(int food, float ratio){

        //Calcul ratio/food

        float calcul = food * ratio;

		changeHunger(calcul);
		if (hunger > hungerMax) {
			hunger = hungerMax;
		}
	}

	void refreshStatuts(){
		if (!fight && !hungry && !work) {
			idle = true;
		} else {
			idle = false;
		}
	}

	public void InitializeInfo(){
		if (tag == "Resource") {
			string text = "";
			text += name;
			text += "\nPlayer : " + numPlayer;
			text += "\n Resource : " + GetComponent<ResourceManager>().type;
			text += "\n Nb : " + GetComponent<ResourceManager>().nbResource;
			info = text;
		} else {
			string text = "";
			text += name;
			text += "\nPlayer : " + numPlayer;
			text += "\n HP : " + life + "/" + maxLife;
			text += "\n Hunger : " + (int)hunger + "/" + hungerMax;
			info = text;
		}

	}
	public void refreshInfo(){
		if (tag == "Resource") {
			string text = "";
			text += name;
			text += "\nPlayer : " + numPlayer;
			text += "\n Resource : " + GetComponent<ResourceManager>().type;
			text += "\n Nb : " + GetComponent<ResourceManager>().nbResource;
			info = text;
		} else {
			string text = "";
			text += name;
			text += "\nPlayer : " + numPlayer;
			text += "\n HP : " + life + "/" + maxLife;
			text += "\n Hunger : " + (int)hunger + "/" + hungerMax;
			info = text;
		}

	}
	public void setPlayer(Player p){
		player = p;
		numPlayer = player.numPlayer;
		Creature c = GetComponent<Creature> ();
		if (c != null) {
			c.setPlayer(p);
		}
	}
	public float getYOffset(){
		return Yoffset;
	}

	public string getInfo(){
		return info;
	}
	public float getHunger(){
		return hunger;
	}
	public void addInfo(string i){
		info +=i;
	}

	public void build(){
		MonoBehaviour[] monos;
		monos = GetComponents<MonoBehaviour> ();
		foreach (MonoBehaviour m in monos) {
			if(m.name == name){
				m.enabled = true;
			}
		}


	}
	public void showLifeBar(){

		lifebar.SetActive (true);
	
	}
	public TileMap getMap(){
		return map;
	}
	public Player getPlayer(){
		return player;
	}
	public bool isFighting(){
		return fight;
	}
	public bool isHungry(){
		return hungry;
	}

	public bool isWorking(){
		return work;
	}
	public bool isMoving(){
		return move;
	}
	public bool isIdle(){
		return idle;
	}

	public void setWork(bool b){
		work = b;
		refreshStatuts ();
	}
	public void setHungry(bool b){
		hungry = b;
		refreshStatuts ();
	}
	public void setFight(bool b){
		fight = b;
		refreshStatuts ();
	}
	public void setMove(bool b){
		move = b;
		refreshStatuts ();
	}
	public Tile getCurrentTile(){
		return map.getMap ().GetTileAt (Mathf.FloorToInt (transform.position.x), Mathf.FloorToInt (transform.position.z));
	}
	public Tile getTileAt(int x, int y){
		return map.getMap ().GetTileAt (x,y);

	}
	public void changeHunger(float i)
	{
		hunger += i;
	}

	public Ability getAbility(AbilityType type){
		foreach (Ability a in abilities) {
			if (a.type == type) {
				return a;
			}
		}
		return null;

	}


	public Tile getNearEmptyTile(){
		Tile t;
		for (int i = 0;; i++) {
			for (int j = 0;; j++) {
                if ((transform.position.x + i < 0 || transform.position.z+j < 0) || (transform.position.x + i >= map.size_x || transform.position.z +j>= map.size_z))
                {

                }
                else
                {
                    t = map.getMap().GetTileAt(Mathf.FloorToInt(transform.position.x + i), Mathf.FloorToInt(transform.position.z + j));
                    if (t.staticEntity == null)
                    {

                        return t;
                    }
                }
                if ((transform.position.x - i < 0 || transform.position.z - j < 0) || (transform.position.x - i >= map.size_x || transform.position.z - j >= map.size_z))
                {

                }
                else
                {
                    t = map.getMap().GetTileAt(Mathf.FloorToInt(transform.position.x - i), Mathf.FloorToInt(transform.position.z - j));
                    if (t.staticEntity == null)
                    {

                        return t;
                    }
                }


			}
		}
	}

    public Tile getNearEmptyTile(EnumTypeTile type)
    {
        Tile t;
        for (int i = 0; ; i++)
        {
            for (int j = 0; ; j++)
            {
                t = map.getMap().GetTileAt(Mathf.FloorToInt(transform.position.x + i), Mathf.FloorToInt(transform.position.z + j));
                if (t.staticEntity == null && t.type == type)
                {

                    return t;
                }
                t = map.getMap().GetTileAt(Mathf.FloorToInt(transform.position.x - i), Mathf.FloorToInt(transform.position.z - j));
                if (t.staticEntity == null && t.type == type)
                {

                    return t;
                }
            }
        }
    }

    void drop(){
		Tile t;
		foreach (GameObject gbj in drops) {
			t = getNearEmptyTile ();
			if (t != null) {
				GameObject newGbj;
				Vector3 v3 = new Vector3 (t.x + map.tileSize / 2, 1.2f, t.y + map.tileSize / 2);
				newGbj = Instantiate (gbj as GameObject, v3 , Quaternion.Euler (new Vector3 (90, 0, 0))) as GameObject;
				newGbj.transform.position = v3;
				t.staticEntity = newGbj;
				if (autoTaskDrop) {
					newGbj.GetComponent<ResourceManager> ().autoTaskTake = true;
				}

			}


		}
		if (name == "Stone") {
			refreshNearbyTile ();
		}
	}


	void refreshNearbyTile(){
		Tile t = getCurrentTile ();
		map.changeTile (t.x, t.y, EnumTypeTile.EARTH);
	}
	/*
	public void setIdle(bool b){
		idle = b;
		refreshStatuts ();
	}
	*/
	                 
}
