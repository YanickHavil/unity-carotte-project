using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

	TileMap map;
	int cmpt = 0;
	// Use this for initialization
	void Start () {
		map = GameObject.Find ("TileMap").GetComponent<TileMap>();
		//InvokeRepeating ("spawnerManager", 1, 1);
	}
	
	void spawnerManager(){
		//Debug.Log (cmpt);
		cmpt++;
		if(cmpt == 5){
			bool good = false;
			while(!good){
				//Debug.Log("Cherche tile");
				int rand = Random.Range (2, map.size_x - 4);
				int rand2 = Random.Range (2, map.size_z - 4);
				Tile t =map.getMap().GetTileAt(rand,rand2);
				if(t.staticEntity ==null){
					GameObject nest = Instantiate(Resources.Load("Entity/Slugen/SlugenNest", typeof(GameObject)),
					                              new Vector3 (7 + map.tileSize/2  ,1.2f,6 + map.tileSize/2),
					                              Quaternion.Euler(new Vector3(90,0,0))) as GameObject;
					t.staticEntity = nest;

					GameObject notif = GameObject.Find("Notification");
					notif.GetComponent<Text>().text = "Apparition d'un nid de Slugen";
					good=true;
					//Debug.Log("Invocation ! ");

				}
			}

		}
	}
}
