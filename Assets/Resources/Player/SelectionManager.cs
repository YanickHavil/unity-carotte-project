using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SelectionManager : MonoBehaviour {


	//Construction
	public GameObject blueprint;

	//Selection
	GameObject selectionCircle;
	GameObject mainUI;
	GameObject selectedGameobject;

	List<GameObject> selectionTerrain;
	List<Tile> listTile;
	//Tile beginSelection;
	//Tile endSelection;
	public bool inSelection = false;
    public bool inSelectionPower = false;
	public bool inStockPileSelection = false;
	public bool inSquareSelection = false;
	public bool inCancelTaskSelection = false;
	public bool inBlueprintSelection = false;
	public bool inRemoveStockPileSelection = false;
	//Attribut pour la selection en carré
	public Tile begin = null;
    //Tile end = null;

    //Le type du pouvoir d'influence
    public InfluenceType influType;
    //Cursor
    public Texture2D normalCursor;
    public Texture2D rainCursor;
    public Texture2D sunCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    GameObject tilemap; // en double dans player
	TileMap tl; // en double dans player
	public Tile tileSelected = null;

	//Use for SelectionTile
	GameObject selectionTile;


	//Type of Task
	public TaskEnum typeTask = TaskEnum.NULL;

	// Use this for initialization
	void Start () {
		mainUI = GameObject.Find ("MainButtonPanel");
		selectionTerrain = new List<GameObject> ();

		tilemap = GameObject.FindWithTag ("TileMap");
		tl = tilemap.GetComponent<TileMap> ();

		listTile = new List<Tile> ();
        normalCursor  = Instantiate(Resources.Load("UI/Cursor/glove3"),Vector3.zero, Quaternion.Euler(new Vector3(0, 0, 0))) as Texture2D;
        rainCursor = Instantiate(Resources.Load("UI/Cursor/RainCursor"), Vector3.zero, Quaternion.Euler(new Vector3(0, 0, 0))) as Texture2D;
        sunCursor = Instantiate(Resources.Load("UI/Cursor/SunCursor"), Vector3.zero, Quaternion.Euler(new Vector3(0, 0, 0))) as Texture2D;
        //InvokeRepeating ("selectionManager", 0, 0.1f);
        //InvokeRepeating ("infoTile", 0, 1.0f);

    }
	
	// Update is called once per frame
	void Update () {
		refreshSelectionTile ();

	}

	void selectionManager(){

	}

    

	void infoTile(){
		if (inSelection) {
			Tile t = tl.getTileAtMousePosition ();
			if ( t != null) {
				//Debug.Log ("Tile x/y   = " + t.x +  "|" + t.y);
				//Debug.Log ("staticEntity = " + t.staticEntity);
			}
				
		}
	}
    
    public void changeEnabledCursor()
    {
        if (inSelectionPower)
        {
            if (influType == InfluenceType.RAIN)
            {
                Cursor.SetCursor(rainCursor, hotSpot, cursorMode);
            }
            else if (influType == InfluenceType.SUN)
            {
                Cursor.SetCursor(sunCursor, hotSpot, cursorMode);
            }
        }




    }
    public void setPowerSelection(InfluenceType type)
    {
        inSelection = true;
        inSelectionPower = true;
        influType = type;
    }

	public void setBlueprint(GameObject gb){
		if (blueprint != null) {
			Destroy(blueprint);
		}
		inSelection = true;
		inBlueprintSelection = true;
		blueprint = Instantiate(gb,Vector3.zero, Quaternion.Euler(new Vector3(90,0,0))) as GameObject;


		//Change Color of blueprint and alpha
		Color c = blueprint.GetComponent<SpriteRenderer>().color;
		c.a = 0.5f;
		blueprint.GetComponent<SpriteRenderer>().color = c;
		blueprint.GetComponent<Collider> ().enabled = false;


	}


    //Rename setTASKonTile
	public void setSelectionTile(TaskEnum t){

		removeSelectionTile ();
		inSelection = true;
		selectionTile = Instantiate(Resources.Load("UI/SelectionPanel") as GameObject,Vector3.zero, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;

		typeTask = t;
		/*
		Tile t = tl.getTileAtMousePosition ();
		if (t != null) {
			Debug.Log ("tile pos x =  " + t.x);
			Debug.Log ("tile pos z =  " + t.y);
			Vector3 v3 = new Vector3();
			v3 = new Vector3(t.x+ tl.tileSize/2,1.005f,t.y+ tl.tileSize/2);
			GameObject selectionP = Instantiate(Resources.Load("UI/SelectionPanel") as GameObject,v3, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;

			//selectionTerrain.Add(selectionP);

		}
		*/

	}
	public void setSelectionRemoveStockPile(){
		removeSelectionTile ();
		inSelection = true;
		inRemoveStockPileSelection = true;
		selectionTile = Instantiate(Resources.Load("UI/SelectionPanel") as GameObject,Vector3.zero, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;


	}

	public void setSelectionCancelTask(){
		removeSelectionTile ();
		inSelection = true;
		inCancelTaskSelection = true;
		selectionTile = Instantiate(Resources.Load("UI/SelectionPanel") as GameObject,Vector3.zero, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
	}
	public void setSelectionStockpile(){
		removeSelectionTile ();
		inSelection = true;
		inStockPileSelection = true;
		selectionTile = Instantiate(Resources.Load("UI/SelectionPanel") as GameObject,Vector3.zero, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;

		//typeTask = t;


	}
	void refreshSelectionTile(){
		
		if (inSquareSelection) {
			tileSelected = tl.getTileAtMousePosition ();
			if (tileSelected != null) {
				Vector3 v3 = new Vector3();
				v3 = new Vector3(tileSelected.x+ tl.tileSize/2,1.005f,tileSelected.y+ tl.tileSize/2);
				selectionTile.transform.position = v3;

				if (inSquareSelection) {
					refreshSquareSelection (tileSelected);
				}




			}


		}
		if (inSelection) {
			tileSelected = tl.getTileAtMousePosition ();
            if (inBlueprintSelection) {
                if (tileSelected != null) {
                    Vector3 v3 = new Vector3();
                    v3 = new Vector3(tileSelected.x + tl.tileSize / 2, 1.005f, tileSelected.y + tl.tileSize / 2);
                    blueprint.transform.position = v3;
                    if (tileSelected.staticEntity != null)
                    {
                        Color c = blueprint.GetComponent<SpriteRenderer>().color;
                        c = Color.red;
                        c.a = 0.5f;
                        blueprint.GetComponent<SpriteRenderer>().color = c;
                    }
                    else
                    {
                        Color c = blueprint.GetComponent<SpriteRenderer>().color;
                        c = Color.white;
                        c.a = 0.5f;
                        blueprint.GetComponent<SpriteRenderer>().color = c;
                    }

                }
            }
            else if (inSelectionPower){
                //Rien ?
            }

            else {

                if (tileSelected != null) {
                    //Debug.Log ("tile pos x =  " + tileSelected.x + "  ||  tile pos z =  " + tileSelected.y);
                    Vector3 v3 = new Vector3();
                    v3 = new Vector3(tileSelected.x + tl.tileSize / 2, 1.005f, tileSelected.y + tl.tileSize / 2);
                    selectionTile.transform.position = v3;

                }
            }

		}



	}

	void refreshSquareSelection(Tile t){

		if (begin.x > t.x) {
			if (begin.y > t.y) {
				for (int i = t.x; i <= begin.x; i++) {
					for (int j = t.y; j <= begin.y; j++) {
						Tile tile = tl.getMap ().GetTileAt (i, j);
						if (!listTile.Contains (tile)) {
							listTile.Add (tile);

							Vector3 v3 = new Vector3();
							v3 = new Vector3(tile.x+ tl.tileSize/2,1.005f,tile.y+ tl.tileSize/2);
							selectionTile.transform.position = v3;

							selectionTerrain.Add (selectionTile);
							selectionTile = Instantiate (Resources.Load ("UI/SelectionPanel") as GameObject, Vector3.zero, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
						}
					}
				}
			} else {
				for (int i = t.x; i <= begin.x; i++) {
					for (int j = begin.y; j <= t.y; j++) {
						Tile tile = tl.getMap ().GetTileAt (i, j);
						if (!listTile.Contains (tile)) {
							listTile.Add (tile);

							Vector3 v3 = new Vector3();
							v3 = new Vector3(tile.x+ tl.tileSize/2,1.005f,tile.y+ tl.tileSize/2);
							selectionTile.transform.position = v3;

							selectionTerrain.Add (selectionTile);
							selectionTile = Instantiate (Resources.Load ("UI/SelectionPanel") as GameObject, Vector3.zero, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
						}
					}
				}
			}
		} else {
			if (begin.y > t.y) {
				for (int i = begin.x; i <= t.x; i++) {
					for (int j = t.y; j <= begin.y; j++) {
						Tile tile = tl.getMap ().GetTileAt (i, j);
						if (!listTile.Contains (tile)) {
							listTile.Add (tile);

							Vector3 v3 = new Vector3();
							v3 = new Vector3(tile.x+ tl.tileSize/2,1.005f,tile.y+ tl.tileSize/2);
							selectionTile.transform.position = v3;

							selectionTerrain.Add (selectionTile);
							selectionTile = Instantiate (Resources.Load ("UI/SelectionPanel") as GameObject, Vector3.zero, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
						}
					}
				}
			} else {
				for (int i = begin.x; i <= t.x; i++) {
					for (int j = begin.y; j <= t.y; j++) {
						Tile tile = tl.getMap ().GetTileAt (i, j);
						if (!listTile.Contains (tile)) {
							listTile.Add (tile);

							Vector3 v3 = new Vector3();
							v3 = new Vector3(tile.x+ tl.tileSize/2,1.005f,tile.y+ tl.tileSize/2);
							selectionTile.transform.position = v3;

							selectionTerrain.Add (selectionTile);
							selectionTile = Instantiate (Resources.Load ("UI/SelectionPanel") as GameObject, Vector3.zero, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
						}
					}
				}
			}
		}
		removeTileSquare (t);
		/*
		if(!listTile.Contains(t)){

			listTile.Add (t);
			selectionTerrain.Add (selectionTile);
			selectionTile = Instantiate(Resources.Load("UI/SelectionPanel") as GameObject,Vector3.zero, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
		}
		*/
	}

	void removeTileSquare(Tile t){
		List<int> listIndex = new List<int> ();
		for (int i = 0; i < listTile.Count; i++) {
			if (listTile [i].x < begin.x && listTile [i].x < t.x) {
				listIndex.Add (i);
			} else if (listTile [i].x > begin.x && listTile [i].x > t.x) {
				listIndex.Add (i);
			} else if (listTile [i].y < begin.y && listTile [i].y < t.y) {
				listIndex.Add (i);
			} else if (listTile [i].y > begin.y && listTile [i].y > t.y) {
				listIndex.Add (i);
			}
		}
		//Debug.Log ("List count Tile = " + listTile.Count);
		//Debug.Log ("List count Terrain = " + selectionTerrain.Count);

		listIndex = listIndex.OrderByDescending(v =>v).ToList();

		for (int i = 0; i < listIndex.Count; i++) {
			listTile.RemoveAt (listIndex[i]);
			GameObject gbj = selectionTerrain [listIndex[i]];
			selectionTerrain.RemoveAt (listIndex[i]);
			Destroy (gbj);
		}
		//foreach (int index in listIndex.OrderByDescending(v =>v)) {
			//Debug.Log ("Index = " + index);

		//}
	}

	public void initSquareSelection(){
		begin = tl.getTileAtMousePosition();
		if (begin != null) {
			listTile.Add (begin);
			selectionTerrain.Add (selectionTile);
			selectionTile = Instantiate (Resources.Load ("UI/SelectionPanel") as GameObject, Vector3.zero, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;

		}

	}

	public Tile getSelectedTile(){
		if (tl != null) {
			return tl.getTileAtMousePosition ();

		}
		return null;
	}
	public void removeSelectionTile(){
		inSelection = false;
		inStockPileSelection = false;
		inCancelTaskSelection = false;
		inSquareSelection = false;
        inSelectionPower = false;
		inRemoveStockPileSelection = false;
		inBlueprintSelection = false;
		begin = null;
		if (blueprint != null) {
			Destroy (blueprint);
		}
		foreach (GameObject t in selectionTerrain) {
			Destroy(t);

		}
		listTile = new List<Tile> ();
		selectionTerrain = new List<GameObject> ();
		Destroy(selectionTile);
        Cursor.SetCursor(normalCursor, hotSpot, cursorMode);
        typeTask = TaskEnum.NULL;

        

		/*
		if (selectionTerrain.Count > 0) {
			for (int i = 0; i< selectionTerrain.Count; i++) {
				DestroyImmediate(selectionTerrain[i]);
			}

			selectionTerrain = new List<GameObject> ();
			inSelection = false;

			beginSelection = null;
			endSelection = null;
		}*/
	}

	public List<Tile> tilesSelected(){
		return listTile;
	}
	void SelectionTerrainPositionRefresh(){



		/*
		if (inSelection) {
			if(Mathf.FloorToInt(selectionTerrain[0].transform.position.x) != Mathf.FloorToInt(Input.mousePosition.x)
				&& Mathf.FloorToInt(selectionTerrain[0].transform.position.z) != Mathf.FloorToInt(Input.mousePosition.z))
			{
				if(Mathf.FloorToInt(Input.mousePosition.x) >(Mathf.FloorToInt(selectionTerrain[selectionTerrain.Count-1].transform.position.x)+tl.tileSize*2)
					||Mathf.FloorToInt(Input.mousePosition.x) <(Mathf.FloorToInt(selectionTerrain[selectionTerrain.Count-1].transform.position.x))
					|| Mathf.FloorToInt(Input.mousePosition.z) >(Mathf.FloorToInt(selectionTerrain[selectionTerrain.Count-1].transform.position.z)+tl.tileSize*2)
					|| Mathf.FloorToInt(Input.mousePosition.z) <(Mathf.FloorToInt(selectionTerrain[selectionTerrain.Count-1].transform.position.z)))

				{
					Tile t = tl.getMap ().GetTileAt (Mathf.FloorToInt(tl.GetMousePosition ().x), Mathf.FloorToInt(tl.GetMousePosition ().z));
					if( t.x%2 == 0 && t.y %2 == 0){
						if(!isInSelectionTerrain(t.x,t.y)){
							Vector3 v3 = new Vector3();
							v3 = new Vector3(t.x + tl.tileSize/2 , 1.005f, t.y+ tl.tileSize/2);

							if(t.type != EnumTypeTile.GRASS){
								selectionTerrain.Add(Instantiate(Resources.Load("UI/SelectionPanelBlue") as GameObject,v3, Quaternion.Euler(new Vector3(0,0,0))) as GameObject);


							}
							else{
								selectionTerrain.Add(Instantiate(Resources.Load("UI/SelectionPanel") as GameObject,v3, Quaternion.Euler(new Vector3(0,0,0))) as GameObject);

							}
						}

					}

				}
			}
		}
		if (selectionTerrain.Count > 0 && !inSelection) {

			selectionTerrain[0].transform.position = new Vector3(Camera.main.WorldToScreenPoint (tl.GetMousePosition ()).x+ tl.tileSize, 1.005f, Camera.main.WorldToScreenPoint (tl.GetMousePosition ()).z+ tl.tileSize);

			if(Input.GetMouseButtonDown (0)){
				setSelectionSize();
			}
		}
		if (Input.GetMouseButtonDown (1)) {

			removeSelectionTerrain();
		}
		/*
		 * 
		if (selectionTerrain.Count > 0 && !inSelection) {

			selectionTerrain[0].transform.position = new Vector3(Camera.main.WorldToScreenPoint (tl.GetMousePosition ()).x+ tl.tileSize/2, 1.005f, Camera.main.WorldToScreenPoint (tl.GetMousePosition ()).z+ tl.tileSize/2);

			if(Input.GetMouseButtonDown (0)){
				setSelectionSize();
			}
		}
		if(inSelection){
			refreshSelectionTile();
		}
		if (Input.GetMouseButtonDown (1)) {

			removeSelectionTerrain();
		}
		*/
	}

}
