using UnityEngine;
using System.Collections;



public class InputManager : MonoBehaviour {

	GameObject tilemap;
	TileMap tl;
	Player player;
	public GameObject avatar;
	SelectionManager sm;
	UnityEngine.AI.NavMeshAgent agent;

	public bool avatarModeActive = false;

	// Use this for initialization
	void Start () {
	
		tilemap = GameObject.FindWithTag ("TileMap");
		tl = tilemap.GetComponent<TileMap> ();

		player = GameObject.Find ("Player").GetComponent<Player>();
		sm = player.GetComponent<SelectionManager> ();



	}
	
	// Update is called once per frame
	void Update () {
		if (!avatarModeActive) {
			if (sm.inSelection) {
				SelectionInput ();
			}


			if (Input.GetKeyDown (KeyCode.A)) {
				Debug.Log ("change Speed");
				if (Time.timeScale == 1) {
					Time.timeScale = 4;

				} else {
					Time.timeScale = 1;
				}
			}

			input ();
		} else {
			inputAvatar ();
		}
	}

	void input(){
		if (Input.GetMouseButtonDown (0)) {
			if (player.selectionObject) {
				player.selectionObject = false;
			} else {
				Tile t = tl.getTileAtMousePosition ();
				if (t != null) {
					if (t.staticEntity == null) {
						player.removeSelection ();

					}
				}
			}

		}
	}


	void inputAvatar(){
		if (Input.GetKey(KeyCode.Z)) {
			Vector3 v3 = new Vector3 (0,0,1);
			avatar.transform.Translate (v3*Time.deltaTime);

		}
		if (Input.GetKey(KeyCode.S)) {
			Vector3 v3 = new Vector3 (0,0,-1);
			avatar.transform.Translate (v3*Time.deltaTime);

		}
		if (Input.GetKey(KeyCode.Q)) {

			Vector3 v3 = new Vector3 (-1,0,0);
			avatar.transform.Translate (v3*Time.deltaTime);
			avatar.GetComponent<AvatarManager> ().direction (-1);
		}
		if (Input.GetKey(KeyCode.D)) {
			Vector3 v3 = new Vector3 (1,0,0);
			avatar.transform.Translate (v3*Time.deltaTime);
			avatar.GetComponent<AvatarManager> ().direction (1);

		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			disabledAvatarMode ();
		}
	}
	void SelectionInput(){
		if (Input.GetMouseButtonDown (0)) {
			if (sm.inBlueprintSelection) {
				Build ();
			}
            else if (sm.inSelectionPower)
            {
                //ICI POUR action sur power
            }
            else {
				sm.initSquareSelection ();

			}

		}
		if (Input.GetMouseButton (0)) {
			//Obligé de faire ce comparatif pour être sur que on est passer par initSquareSelection
			if (sm.begin != null) {
				sm.inSquareSelection = true;

			}
		}

		if (Input.GetMouseButtonUp (0)) {
			if (sm.tilesSelected ().Count > 0) {
				if (sm.inStockPileSelection) {
					player.makeStockpile (sm.tilesSelected ());
					sm.removeSelectionTile ();
				} else if (sm.inCancelTaskSelection) {
					removeTaskOnTile ();
					sm.removeSelectionTile ();
				} else if (sm.inRemoveStockPileSelection) {
					removeStockPile ();
					sm.removeSelectionTile ();
				}
				else if (!sm.inBlueprintSelection){
					makeTaskOnTile ();

				}


			}

			sm.inSquareSelection = false;



		}
			
		if(Input.GetMouseButtonDown(1)){
			sm.removeSelectionTile();
		}
	}

	void removeTaskOnTile(){
		foreach (Tile t in sm.tilesSelected()) {
			if (t.staticEntity != null) {
				player.removeTaskOnEntity (t.staticEntity);

			}
		}
	}

	void removeStockPile(){
		player.removeTileStock (sm.tilesSelected());
	}

	void makeTaskOnTile(){
		int cpt = 0;
		foreach (Tile tile in sm.tilesSelected()) {
			if (tile.staticEntity != null) {
				GetComponent<TaskManager> ().addTask(new Task (tile.staticEntity, sm.typeTask,AbilityType.COLLECT));
				cpt++;
			}

		}
		Debug.Log ("cpt = " + cpt);

		sm.removeSelectionTile ();

	}



	void Build(){

        Tile t = tl.getTileAtMousePosition();


        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1) && t.staticEntity == null) {
			//GoodColor
			tl.getTileAtMousePosition().staticEntity = sm.blueprint;
			Color c = sm.blueprint.GetComponent<SpriteRenderer>().color;
			c.a = 1.0f;
			sm.blueprint.GetComponent<SpriteRenderer>().color = c;

			//If bizarre sert uniquement a déterminé si on prend le temple ou l'invocation A CHANGER 
			if (sm.blueprint.GetComponent<Attributes> () != null) {
				sm.blueprint.transform.position = new Vector3(tl.getTileAtMousePosition().x + tl.tileSize/2,1.1f,tl.getTileAtMousePosition().y+ tl.tileSize/2);
				sm.blueprint.GetComponent<Attributes> ().setPlayer (player);

				//Afin de lancer le script du même nom (ex : Temple => script "Temple" va être activé
				sm.blueprint.name = sm.blueprint.GetComponent<Attributes>().name;
				sm.blueprint.GetComponent<Attributes>().build();


				player.addBuilding(sm.blueprint);
				sm.blueprint.GetComponent<Collider> ().enabled = true;
				//tl.getMap().GetTileAt(tl.getTileAtMousePosition().x,tl.getTileAtMousePosition().y).staticEntity = sm.blueprint;

			}
			else{
				if (sm.blueprint.tag == "Power") {
					sm.blueprint.transform.position = new Vector3(tl.getTileAtMousePosition().x+ tl.tileSize/2,1.1f,tl.getTileAtMousePosition().y+ tl.tileSize/2);
					string namePower = sm.blueprint.GetComponent<Power> ().powerName;
					System.Type MyScriptType = System.Type.GetType (namePower + ",Assembly-CSharp");

					sm.blueprint.AddComponent(MyScriptType);
				}


			}


			sm.blueprint = null;
			Debug.Log ("build");
			sm.removeSelectionTile ();


		}


		//mainUI.GetComponent<MainPanel> ().refeshAvailable ();

	}

	public void avatarMode(){
		avatarModeActive = true;
		Vector3 v3 = new Vector3 (avatar.transform.position.x, Camera.main.transform.position.y, avatar.transform.position.z);
		Camera.main.transform.position = v3;
		Camera.main.transform.parent = avatar.transform;


	}

	void disabledAvatarMode(){
		avatarModeActive = false;
		Camera.main.transform.parent = null;
		GameObject g = avatar;
		avatar = null;
		Destroy (g);
	}
}
