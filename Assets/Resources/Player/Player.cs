using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class Player : MonoBehaviour
{

    //A supprimer
    public GameObject textSelector;
    public GameObject selectionImage;
    //a Supprimer
    GameObject iconBuilding;
    List<GameObject> selectionTerrain;
    public GameObject selectionSquare;
    public GameObject identifierCreature;
    GameObject selectionCircle;
    GameObject selectedGameobject;
    public bool selectionObject = false;
    TaskManager task;
    //List of Building
    List<GameObject> buildings;
    List<GameObject> stockpiles;
    GameObject temple;

    GameObject tilemap;
    TileMap tl;
    //Numéro Joueur
    public int numPlayer;

    //Créatures du joueur : 
    List<GameObject> creatures;

    //Ouvrier Goblin
    List<GameObject> workers;
    //Cursor
    public Texture2D cursorTexture;
    public Texture2D collectTexture;
    public Texture2D sunPower;
    public Texture2D rainPower;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;



    //UI REFRESH
    public Text nbFruit;
    public Text nbWood;
    public Text nbPollen;
    public Text nbStone;


    // Use this for initialization
    void Start()
    {
        creatures = new List<GameObject>();
        workers = new List<GameObject>();
        numPlayer = 1;
        buildings = new List<GameObject>();
        stockpiles = new List<GameObject>();
        tilemap = GameObject.FindWithTag("TileMap");
        tl = tilemap.GetComponent<TileMap>();
        task = gameObject.AddComponent<TaskManager>();
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);


        InvokeRepeating("refreshInfoUI", 0f, 0.2f);
        //Ajout des ouvriers


    }

    // Update is called once per frame
    void Update()
    {

        //SelectionBuildingPositionRefresh ();
        //SelectionTerrainPositionRefresh ();
    }

    public void setIconBuilding(GameObject tr)
    {

        /*
		if (iconBuilding != null) {
			DestroyImmediate(iconBuilding);
		}
		Vector3 v3 = new Vector3();

		iconBuilding = Instantiate(tr,v3, Quaternion.Euler(new Vector3(90,0,0))) as GameObject;
		if (iconBuilding.GetComponent<Attributes> () != null) {
			v3 = new Vector3 (Input.mousePosition.x, iconBuilding.GetComponent<Attributes> ().getYOffset (), Input.mousePosition.z);

		} else {
			v3 = new Vector3(Input.mousePosition.x,1.2f,Input.mousePosition.z);
		}
		iconBuilding.transform.position = v3;
		Color c = iconBuilding.GetComponent<SpriteRenderer>().color;
		c.a = 0.5f;
		iconBuilding.GetComponent<SpriteRenderer>().color = c;
		iconBuilding.GetComponent<Collider> ().enabled = false;
*/
    }

    void SelectionBuildingPositionRefresh()
    {
        if (iconBuilding != null)
        {
            if (iconBuilding.GetComponent<Attributes>() != null)
            {
                iconBuilding.transform.position = new Vector3(Mathf.FloorToInt(tl.GetMousePosition().x) + tl.tileSize / 2, iconBuilding.GetComponent<Attributes>().getYOffset(), Mathf.FloorToInt(tl.GetMousePosition().z) + tl.tileSize / 2);

            }
            else
            {
                iconBuilding.transform.position = new Vector3(Mathf.FloorToInt(tl.GetMousePosition().x) + tl.tileSize / 2, 1.0f, Mathf.FloorToInt(tl.GetMousePosition().z) + tl.tileSize / 2);

            }
            Tile t = tl.getTileAtMousePosition();
            Tile t1 = tl.getMap().GetTileAt(t.x + 1, t.y + 1);
            Tile t2 = tl.getMap().GetTileAt(t.x, t.y + 1);
            Tile t3 = tl.getMap().GetTileAt(t.x + 1, t.y);
            Color c = iconBuilding.GetComponent<SpriteRenderer>().color;
            if (t != null && t1 != null && t2 != null && t3 != null)
            {
                if (t.staticEntity != null || !t.show || t.type != EnumTypeTile.EARTH
                   || t1.staticEntity != null || !t1.show || t1.type != EnumTypeTile.EARTH
                   || t2.staticEntity != null || !t2.show || t2.type != EnumTypeTile.EARTH
                   || t3.staticEntity != null || !t3.show || t3.type != EnumTypeTile.EARTH)
                {

                    c = Color.red;
                    c.a = 0.5f;
                    iconBuilding.GetComponent<SpriteRenderer>().color = c;
                    if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                    {
                        DestroyImmediate(iconBuilding);
                    }
                }
                else
                {
                    c = Color.white;
                    c.a = 0.5f;
                    iconBuilding.GetComponent<SpriteRenderer>().color = c;
                    if (Input.GetMouseButtonDown(0))
                    {
                        Build();
                    }
                }
            }
        }
    }
    /*
	public void addTask(GameObject g, TaskEnum t){
		task.addTask (g,t);
	}*/
    public void addCreature(GameObject g)
    {
        creatures.Add(g);
        g.GetComponent<Attributes>().setPlayer(this);
        foreach (GameObject c in creatures)
        {
            Debug.Log(c.name);
        }
        GameObject identifier = Instantiate(identifierCreature, Vector3.zero, Quaternion.identity) as GameObject;
        identifier.transform.parent = g.transform;
        identifier.transform.localPosition = Vector3.zero;
    }

    public void addWorker(GameObject g)
    {
        workers.Add(g);
        g.GetComponent<Attributes>().setPlayer(this);
        foreach (GameObject c in creatures)
        {
            Debug.Log(c.name);
        }
    }


    /*
	void setSelectionSize(){
		beginSelection = tl.getMap ().GetTileAt (Mathf.FloorToInt(tl.GetMousePosition ().x), Mathf.FloorToInt(tl.GetMousePosition ().z));
		selectionTerrain[0].transform.position = new Vector3(beginSelection.x+ tl.tileSize/2, 1.005f, beginSelection.y + tl.tileSize/2);
		inSelection = true;
	}
	*/

    /*
	void refreshSelectionTile(){
		int x = Mathf.FloorToInt(tl.GetMousePosition ().x);
		int y = Mathf.FloorToInt(tl.GetMousePosition ().z);
		if ((x < 0 || y < 0) || (x >= 50 || y >= 50)) { // CHANGER par MAP.size_x et size_y
			
		} else {
			endSelection = tl.getMap ().GetTileAt (Mathf.FloorToInt (tl.GetMousePosition ().x), Mathf.FloorToInt (tl.GetMousePosition ().z));
		}
		if (beginSelection.x != endSelection.x || beginSelection.y != endSelection.y) {

			if(beginSelection.x > endSelection.x && beginSelection.y > endSelection.y){
				for(int i = endSelection.x; i <= beginSelection.x;i++){
					for(int j = endSelection.y; j <= beginSelection.y;j++){
						if(!isInSelectionTerrain(i,j)){
							Vector3 v3 = new Vector3();
							v3 = new Vector3(i + tl.tileSize/2 , 1.005f, j + tl.tileSize/2);
							selectionTerrain.Add(Instantiate(Resources.Load("UI/SelectionPanel") as GameObject,v3, Quaternion.Euler(new Vector3(0,0,0))) as GameObject);
						}
					}

				}
				removeOutSelectionTile(endSelection.x,endSelection.y,beginSelection.x,beginSelection.y);
			}

			if(beginSelection.x >= endSelection.x && beginSelection.y <= endSelection.y){
				for(int i = endSelection.x; i <= beginSelection.x;i++){
					for(int j = beginSelection.y; j <= endSelection.y;j++){
						if(!isInSelectionTerrain(i,j)){
							Vector3 v3 = new Vector3();
							v3 = new Vector3(i + tl.tileSize/2 , 1.005f, j + tl.tileSize/2);
							selectionTerrain.Add(Instantiate(Resources.Load("UI/SelectionPanel") as GameObject,v3, Quaternion.Euler(new Vector3(0,0,0))) as GameObject);
						}
					}
					
				}
				removeOutSelectionTile(endSelection.x,beginSelection.y,beginSelection.x,endSelection.y);

			}

			if(beginSelection.x <= endSelection.x && beginSelection.y >= endSelection.y){
				for(int i = beginSelection.x; i <= endSelection.x;i++){
					for(int j = endSelection.y; j <= beginSelection.y;j++){
						if(!isInSelectionTerrain(i,j)){
							Vector3 v3 = new Vector3();
							v3 = new Vector3(i + tl.tileSize/2 , 1.005f, j + tl.tileSize/2);
							selectionTerrain.Add(Instantiate(Resources.Load("UI/SelectionPanel") as GameObject,v3, Quaternion.Euler(new Vector3(0,0,0))) as GameObject);
						}
					}
					
				}
				removeOutSelectionTile(beginSelection.x,endSelection.y,endSelection.x,beginSelection.y);

			}

			if(beginSelection.x < endSelection.x && beginSelection.y < endSelection.y){
				for(int i = beginSelection.x; i <= endSelection.x;i++){
					for(int j = beginSelection.y; j <= endSelection.y;j++){
						if(!isInSelectionTerrain(i,j)){
							Vector3 v3 = new Vector3();
							v3 = new Vector3(i + tl.tileSize/2 , 1.005f, j + tl.tileSize/2);
							selectionTerrain.Add(Instantiate(Resources.Load("UI/SelectionPanel") as GameObject,v3, Quaternion.Euler(new Vector3(0,0,0))) as GameObject);
						}
					}
					
				}
				removeOutSelectionTile(beginSelection.x,beginSelection.y,endSelection.x,endSelection.y);
			}



		}
	}*/

    void removeOutSelectionTile(int xMini, int yMini, int xMax, int yMax)
    {
        for (int i = 0; i < selectionTerrain.Count; i++)
        {
            Tile test = tl.getMap().GetTileAt((int)selectionTerrain[i].transform.position.x, (int)selectionTerrain[i].transform.position.z);
            if (test.x < xMini || test.y < yMini || test.x > xMax || test.y > yMax)
            {
                GameObject caca = selectionTerrain[i];
                selectionTerrain.Remove(caca);
                DestroyImmediate(caca);
            }

        }

    }
    bool isInSelectionTerrain(int x, int y)
    {

        for (int i = 0; i < selectionTerrain.Count; i++)
        {
            Tile test = tl.getMap().GetTileAt(Mathf.FloorToInt(selectionTerrain[i].transform.position.x), Mathf.FloorToInt(selectionTerrain[i].transform.position.z));
            if (test.x == x && test.y == y)
            {
                return true;
            }


        }
        Debug.Log("Effacer");
        return false;


    }

    public void makeStockpile(List<Tile> tiles)
    {

        GameObject st = Instantiate(Resources.Load("Buildings/Stockpile") as GameObject, Vector3.zero, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;

        if (st.GetComponent<StockpileScript>().init(tiles, stockpiles))
        {
            stockpiles.Add(st);
            Debug.Log("stockpile count = " + stockpiles.Count);
        }



    }

    void Build()
    {

        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1))
        {
            tl.getTileAtMousePosition().staticEntity = iconBuilding;
            Color c = iconBuilding.GetComponent<SpriteRenderer>().color;
            c.a = 1.0f;
            iconBuilding.GetComponent<SpriteRenderer>().color = c;
            if (iconBuilding.GetComponent<Attributes>() != null)
            {
                iconBuilding.transform.position = new Vector3(tl.getTileAtMousePosition().x + tl.tileSize / 2, 1.0f, tl.getTileAtMousePosition().y + tl.tileSize / 2);
                iconBuilding.GetComponent<Attributes>().setPlayer(this);
                iconBuilding.name = iconBuilding.GetComponent<Attributes>().name;
                iconBuilding.GetComponent<Attributes>().build();
                addBuilding(iconBuilding);
                iconBuilding.GetComponent<Collider>().enabled = true;
                tl.getMap().GetTileAt(tl.getTileAtMousePosition().x, tl.getTileAtMousePosition().y).staticEntity = iconBuilding;

            }
            else
            {
                iconBuilding.transform.position = new Vector3(tl.getTileAtMousePosition().x + tl.tileSize, 1.2f, tl.getTileAtMousePosition().y + tl.tileSize);
                iconBuilding.AddComponent<RandomMonster>();

            }


            iconBuilding = null;

        }
        foreach (GameObject go in buildings)
        {
            Debug.Log("building : " + go.name);
        }

        //mainUI.GetComponent<MainPanel> ().refeshAvailable ();

    }

    public void setSelection(GameObject go)
    {
        if (!GetComponent<SelectionManager>().inSelection)
        {
            removeSelection();
            selectedGameobject = go;
            if (selectedGameobject.GetComponentInChildren<SpriteRenderer>() != null)
            {
                Sprite sp = selectedGameobject.GetComponentInChildren<SpriteRenderer>().sprite;
                selectionImage.GetComponent<Image>().sprite = sp;
                selectionImage.GetComponent<Image>().CrossFadeAlpha(1.0f, 0.0f, true);

            }
            string text = setTextSelection(selectedGameobject);
            textSelector.GetComponent<Text>().text = text;

            Tile t = tl.getTileAtMousePosition();
            Vector3 v3 = Vector3.zero;
            if (t != null)
            {
                v3 = new Vector3(t.x + tl.tileSize / 2, 1.2f, t.y + tl.tileSize / 2);

            }
            if (selectedGameobject.tag == "Creature")
            {
                selectionCircle = Instantiate(selectionSquare, Vector3.zero, Quaternion.identity) as GameObject;

                selectionCircle.transform.parent = selectedGameobject.transform;
                selectionCircle.transform.localPosition = Vector3.zero;

            }
            else
            {
                selectionCircle = Instantiate(selectionSquare, v3, Quaternion.identity) as GameObject;

            }
            selectionObject = true;
        }

    }


    string setTextSelection(GameObject go)
    {
        string text;

        if (go.GetComponent<Attributes>() == null)
        {
            text = go.GetComponent<InfoManager>().getInfo();
        }
        else
        {
            text = go.GetComponent<Attributes>().getInfo();

        }

        /*
		text += "\n Player : ";
		if (go.GetComponent<Attributes> ().numPlayer == 1) {
			text += "oui";
		} else {
			text += "non";
			
		}
		*/
        return text;
    }
    public void removeSelection()
    {
        if (selectedGameobject != null)
        {
            if (selectedGameobject.tag == "Nest")
            {
                selectedGameobject.GetComponent<Nest>().hideTerritory();

            }
        }
        selectedGameobject = null;
        Destroy(selectionCircle);

        if (GameObject.Find("ImageSelection").GetComponent<Image>().sprite != null)
        {
            GameObject.Find("ImageSelection").GetComponent<Image>().sprite = null;
            GameObject.Find("ImageSelection").GetComponent<Image>().CrossFadeAlpha(0.0f, 0.0f, true);
        }
        if (GameObject.Find("TextSelection").GetComponent<Text>().text != string.Empty)
        {
            GameObject.Find("TextSelection").GetComponent<Text>().text = string.Empty;
        }


    }
    public void cursorDisabledPower()
    {

        if (GetComponent<SelectionManager>().influType == InfluenceType.RAIN)
        {
            Cursor.SetCursor(rainPower, hotSpot, cursorMode);
        }
        else if (GetComponent<SelectionManager>().influType == InfluenceType.SUN)
        {
            Cursor.SetCursor(sunPower, hotSpot, cursorMode);
        }
    }
    public void cursorPower(int i)
    {
        if (i == 1)
        {
            Cursor.SetCursor(rainPower, hotSpot, cursorMode);
            GetComponent<SelectionManager>().setPowerSelection(InfluenceType.RAIN);

        }
        else
        {
            Cursor.SetCursor(sunPower, hotSpot, cursorMode);
            GetComponent<SelectionManager>().setPowerSelection(InfluenceType.SUN);
        }

    }


    public void changeCursor(string tag)
    {
        if (tag == "Resource")
        {
            Cursor.SetCursor(collectTexture, hotSpot, cursorMode);

        }
        else
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

        }

    }
    public void addBuilding(GameObject gb)
    {
        buildings.Add(gb);
        foreach (GameObject gbj in buildings)
        {
            Debug.Log("building = " + gbj);
        }
    }

    public bool isIconBuilding(GameObject gb)
    {
        if (gb == iconBuilding)
        {
            return true;
        }
        return false;
    }

    public void setPower()
    {


    }

    public bool haveBuilding(string name)
    {
        foreach (GameObject g in buildings)
        {
            if (g.name == name)
            {
                return true;
            }
        }
        return false;
    }
    public bool haveSelection()
    {
        if (iconBuilding != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool haveWorker()
    {
        if (workers.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool haveCreature()
    {
        if (creatures.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void removeTaskOnEntity(GameObject gbj)
    {
        task.removeTask(gbj);

    }

    public void removeTask(Task t)
    {
        task.removeTask(t);
    }


    public List<GameObject> getCreatures()
    {
        return creatures;
    }
    public List<GameObject> getWorkers()
    {
        return workers;
    }
    public GameObject getTemple()
    {
        foreach (GameObject g in buildings)
        {
            if (g.name == "Temple")
            {
                return g;
            }
        }
        return null;
    }

    public Tile getStockTile(GameObject gbj)
    {

        foreach (GameObject obj in stockpiles)
        {
            Tile t = obj.GetComponent<StockpileScript>().getStockTile(gbj);
            if (t != null)
            {
                return t;
            }
        }


        foreach (GameObject obj in stockpiles)
        {
            Tile t = obj.GetComponent<StockpileScript>().getEmptyTile();
            if (t != null)
                return t;

        }
        return null;
    }

    public bool isStockTile(Tile t)
    {
        foreach (GameObject obj in stockpiles)
        {

            foreach (Tile tile in obj.GetComponent<StockpileScript>().tiles)
            {
                if (tile == t)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void removeTileStock(List<Tile> tiles)
    {
        List<int> listIndex = new List<int>();
        int cpt = 0;
        foreach (GameObject obj in stockpiles)
        {
            Debug.Log("stockpile nb " + cpt);
            obj.GetComponent<StockpileScript>().removeTileStock(tiles);
            if (obj.GetComponent<StockpileScript>().tiles.Count < 1)
            {
                listIndex.Add(cpt);
            }
            cpt++;
        }

        listIndex = listIndex.OrderByDescending(v => v).ToList();

        for (int i = 0; i < listIndex.Count; i++)
        {
            GameObject gbj = stockpiles[listIndex[i]];

            stockpiles.Remove(gbj);
            Destroy(gbj);
        }
    }

    public bool haveMana(int i)
    {
        return (GetComponent<ManaManager>().mana >= i);
    }

    void refreshInfoUI()
    {
        int nbW = 0;
        int nbS = 0;
        int nbP = 0;
        int nbF = 0;

        foreach (GameObject gbj in stockpiles)
        {
            nbW += gbj.GetComponent<StockpileScript>().nbRessourceInStockPile(TypeResource.WOOD);
            nbS += gbj.GetComponent<StockpileScript>().nbRessourceInStockPile(TypeResource.STONE);
            nbP += gbj.GetComponent<StockpileScript>().nbRessourceInStockPile(TypeResource.POLLEN);
            nbF += gbj.GetComponent<StockpileScript>().nbRessourceInStockPile(TypeResource.FRUIT);

        }

        nbWood.text = nbW.ToString();
        nbStone.text = nbS.ToString();
        nbPollen.text = nbP.ToString();
        nbFruit.text = nbF.ToString();


    }

    public GameObject getFoodinStock(TypeResource type)
    {
        foreach (GameObject gbj in stockpiles)
        {
            return gbj.GetComponent<StockpileScript>().getFood(type);

        }
        return null;
    }
}
