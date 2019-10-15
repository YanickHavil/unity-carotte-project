using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	TileMap map;

	// Use this for initialization
	void Start () {
	
		map = GameObject.Find ("TileMap").GetComponent<TileMap> ();

		InvokeRepeating ("Spawn", 10, 10);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Spawn(){
		int token = Random.Range (0, 11);
		Debug.Log ("Try Spawn");
		if (token == 5) {
			float xMax = map.size_x;
			float yMax = map.size_z;

			float x = Random.Range(0,xMax);
			float y = Random.Range(0,yMax);

			token = Random.Range(0,5);

			if(token <=3){

				Instantiate (Resources.Load("Entity/Terra/Moki/Moki"),
				             new Vector3 (x,0.3f,y),
				             Quaternion.Euler(new Vector3(0,30,0)));
				Debug.Log("Spawn Moki");
			}
			else{
				Instantiate (Resources.Load("Entity/Terra/Plonnen/Plonnen"),
				             new Vector3 (x,0.3f,y),
				             Quaternion.Euler(new Vector3(0,30,0)));
				Debug.Log("Spawn Plonnen");
			}

		}
	


	}
}
