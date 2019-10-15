using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(TileMap))]
public class TileMapInspector : Editor {
	
	public override void OnInspectorGUI() {
		//base.OnInspectorGUI();
		DrawDefaultInspector();
		
		if(GUILayout.Button("Regenerate")) {
			TileMap tileMap = (TileMap)target;
			tileMap.BuildMesh();
		}
		else if(GUILayout.Button("Spawn")) {
			TileMap tileMap = (TileMap)target;
			tileMap.Spawn();
		}
		else if(GUILayout.Button("RefreshObstacles")) {
			TileMap tileMap = (TileMap)target;
			tileMap.MakeObstacle();
		}
	}
}
