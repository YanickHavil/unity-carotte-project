using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileMap))]
public class Mouse : MonoBehaviour {

	TileMap _tileMap;
	
	Vector3 currentTileCoord;
	
	public Transform selectionCube;


	// Use this for initialization
	void Start () {
		//_tileMap = GetComponent<TileMap>();
	}
	
	// Update is called once per frame
	void Update () {
/*
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hitInfo;

		if( GetComponent<Collider>().Raycast( ray, out hitInfo, Mathf.Infinity ) ) {
			int x = Mathf.FloorToInt( hitInfo.point.x / _tileMap.tileSize);
			int z = Mathf.FloorToInt( hitInfo.point.z / _tileMap.tileSize);
			//Debug.Log ("Tile: " + x + ", " + z);
			
			currentTileCoord.x = x;
			currentTileCoord.z = z;
			
			selectionCube.transform.position = currentTileCoord;
		}
		else {
			// Hide selection cube?
		}


		if(Input.GetMouseButtonDown(1)) {

			GameObject.Find("ChampTest").GetComponent<NavMeshAgent>().destination = new Vector3(hitInfo.point.x,0,hitInfo.point.z);




		}



*/
	}
}
