using UnityEngine;
using System.Collections;

public class InputTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (Input.GetKeyDown(KeyCode.Space));
		if (Input.GetKeyDown("space")) {
			TileMap map = 	GameObject.Find("TileMap").GetComponent<TileMap>();
			for (int y = 0; y < map.size_z; y++) {
				for (int x = 0; x < map.size_x; x++) {
					if(map.getMap().GetTileAt(x,y).staticEntity != null){
						Debug.Log ("Il y a : " + map.getMap().GetTileAt(x,y).staticEntity.name);
					}
				}
			}
		}
		if (Input.GetKey (KeyCode.Escape)) {
			if(Time.timeScale == 1){
				Time.timeScale = 0;
			}
			else{
				Time.timeScale = 1;

			}
		}

		//print("space key was pressed");
	}

}
