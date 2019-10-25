using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StockpileScript : MonoBehaviour{


	public List<Tile> tiles;
	List<GameObject> tilesGame;
	TileMap tl; // en double
	GameObject tilemap;



	public bool init(List<Tile> list,List<GameObject> stockpiles){
		bool empty = true;
		tilemap = GameObject.FindWithTag ("TileMap");
		tl = tilemap.GetComponent<TileMap> ();

		tiles = new List<Tile> ();
		tilesGame = new List<GameObject> ();

		foreach (Tile t in list) {
			if (t.staticEntity == null && verifTile(stockpiles,t)) {
				tiles.Add (t);
				empty = false;
				GameObject square = Instantiate (Resources.Load ("Buildings/StockPilePlane") as GameObject, Vector3.zero, Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject;
				Vector3 v3 = new Vector3();
				v3 = new Vector3(t.x+ tl.tileSize/2,1.005f,t.y+ tl.tileSize/2);
				square.transform.position = v3;
				//Debug.Log ("Tile ajouté : " + t.x + "|" + t.y);
				tilesGame.Add (square);
			}
		}
		if (empty) {
			stockpiles.Remove (gameObject);
			Destroy (gameObject);
			return false;
		}
		return true;

	}

	public Tile getStockTile (GameObject gbj){
		foreach (Tile t in tiles) {
			if (t.staticEntity != null) {
				if (t.staticEntity.GetComponent<ResourceManager> ().type == gbj.GetComponent<ResourceManager> ().type
					&& t.staticEntity.GetComponent<ResourceManager>().nbResource < t.staticEntity.GetComponent<ResourceManager>().maxStockPile) {
					return t;
				}
			}

		}
		return null;
	}

	public Tile getEmptyTile(){
		foreach (Tile t in tiles) {
			if (t.staticEntity == null) {
				return t;
			}
		}
		return null;
	}

	bool verifTile(List<GameObject> stockpiles, Tile t){
		for (int i = 0; i < stockpiles.Count; i++) {
			foreach (Tile tile in stockpiles[i].GetComponent<StockpileScript>().tiles) {
				if (tile.x == t.x && t.y == tile.y) {
					return false;
				}
			}
		}
		return true;
	}

	public void removeTileStock(List<Tile> tilesCheck){
		List<int> listIndex = new List<int> ();
		foreach (Tile t in tilesCheck) {
			if (tiles.Contains (t)) {
				listIndex.Add (tiles.IndexOf (t));
			}
		}

		listIndex = listIndex.OrderByDescending(v =>v).ToList();
		for (int i = 0; i < listIndex.Count; i++) {
			tiles.RemoveAt (listIndex [i]);
			GameObject gbj = tilesGame [listIndex[i]];
			tilesGame.Remove (gbj);
			Destroy (gbj);
		}


	}

	public int nbRessourceInStockPile(TypeResource type){
		int res = 0;
		foreach (Tile t in tiles) {
			if (t.staticEntity != null) {
				if (t.staticEntity.GetComponent<ResourceManager> ().type == type) {
					res += t.staticEntity.GetComponent<ResourceManager> ().nbResource;
				}
			}

		}

		return res;
	}

    public GameObject getFood(TypeResource type)
    {
        foreach (Tile t in tiles)
        {
            if (t.staticEntity != null)
            {
                if (t.staticEntity.GetComponent<ResourceManager>().type == type)
                {
                    return t.staticEntity;
                }
            }

        }
        return null;
    }

}
