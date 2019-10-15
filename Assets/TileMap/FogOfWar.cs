using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter) )]
[RequireComponent(typeof(MeshCollider) )]
[RequireComponent(typeof(MeshRenderer) )]

public class FogOfWar : MonoBehaviour {
	
	int size_x;
	int size_z;
	float tileSize;
	
	int tileResolution;
	
	Texture2D texture ;
	Color[][] tiles;
	FogOfWarData map;
	
	Collider col;
	// Use this for initialization
	void Start () {


		
	}

	public void init(int x, int z , float size, int resolution, Color[][] color, Texture2D text){
		size_x = x;
		size_z = z;
		tileSize = size;
		tileResolution = resolution;
		tiles = color;
		texture = text;

		col = GetComponent<Collider> ();
		map = new FogOfWarData (size_x, size_z);
		BuildMesh ();
	}


	public void BuildMesh(){
		

		int numTiles = size_x * size_z;
		int numTris = numTiles * 2;
		
		int vsize_x = size_x + 1;
		int vsize_z = size_z + 1;
		int numVerts = vsize_x * vsize_z;
		
		// Generate the Mesh Data
		
		Vector3[] vertices = new Vector3[numVerts];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];
		
		int[] triangles = new int[numTris * 3];
		
		int x,z;
		for (z=0;z <size_z;z++) {
			for (x=0;x <size_x;x++) {
				
				vertices[ z * vsize_x + x ] = new Vector3( x*tileSize  , 1, z*tileSize );
				normals[ z * vsize_x + x ] = Vector3.up;
				uv[ z * vsize_x + x ] = new Vector2( (float)x / size_x, (float)z / size_z );
				
				
			}
			
			
		}
		
		for(z=0; z < size_z; z++) {
			for(x=0; x < size_x; x++) {
				int squareIndex = z * size_x + x;
				int triOffset = squareIndex * 6;
				triangles[triOffset + 0] = z * vsize_x + x + 		   0;
				triangles[triOffset + 1] = z * vsize_x + x + vsize_x + 0;
				triangles[triOffset + 2] = z * vsize_x + x + vsize_x + 1;
				
				triangles[triOffset + 3] = z * vsize_x + x + 		   0;
				triangles[triOffset + 4] = z * vsize_x + x + vsize_x + 1;
				triangles[triOffset + 5] = z * vsize_x + x + 		   1;
			}
		}


		//Create a new Mesh and populate with data
		
		Mesh mesh = new Mesh ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;
		
		
		// Assign our mesh to filter/collider/renderer
		
		MeshFilter mf = GetComponent<MeshFilter> ();
		MeshCollider mc = GetComponent<MeshCollider> ();
		MeshRenderer mr = GetComponent<MeshRenderer> ();
		mf.mesh = mesh;
		mc.sharedMesh = mesh;
		
		BuildTexture ();
		
		
		
	}
	
	void BuildTexture(){
		
		int texWidth = size_x * tileResolution;
		int texHeight = size_z * tileResolution;
		texture = new Texture2D (texWidth, texHeight);

		
		for(int y = 0; y < size_z; y++){
			for(int x = 0; x < size_x; x++){
				Color[] p;
				p = tiles [13];

				texture.SetPixels(x* tileResolution, y*tileResolution, tileResolution, tileResolution,p);
			}
		}
		
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.Apply();
		
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		mesh_renderer.materials[0].mainTexture = texture;
		
		Debug.Log("Done Texture!");
		
	}

	public void showTile(int x, int y){
		Tile t = GetTileAt (x, y);
		if (!t.show) {
			t.show = true;

			Mesh mesh = GetComponent<MeshFilter>().mesh;

			//Vertices
			/*
			Vector3[] vertices = mesh.vertices;
			int vsize_x = size_x + 1;
			t.show = true;
			Vector3[] empty = new Vector3[vertices.Length];
			vertices[ y * vsize_x + x ] = empty[ y * vsize_x + x ];
			*/


			//Triangles
			int numTiles = size_x * size_z;
			int[] triangles = mesh.triangles;
			int squareIndex = y * size_x + x;
			int triOffset = squareIndex * 6;

			int[] emptyTri = new int[triangles.Length];
			triangles[triOffset + 0] = emptyTri[triOffset + 0];
			triangles[triOffset + 1] = emptyTri[triOffset + 1];
			triangles[triOffset + 2] = emptyTri[triOffset + 2];
			
			triangles[triOffset + 3] = emptyTri[triOffset + 3];
			triangles[triOffset + 4] = emptyTri[triOffset + 4];
			triangles[triOffset + 5] = emptyTri[triOffset + 5];


			//Normals
			/*
			Vector3[] normals = mesh.normals;
			Vector3[] emptyNor = new Vector3[normals.Length];
			normals[ y * vsize_x + x ] = emptyNor[ y * vsize_x + x ];
			*/

			//UV 
			/*
			Vector2[] uv = mesh.uv;
			Vector2[] emptyUV = new Vector2[uv.Length];
			uv[ y * vsize_x + x ] = emptyUV[ y * vsize_x + x ];
			*/


			//mesh.uv = uv;
			//mesh.normals = normals;
			//mesh.Clear();

			//mesh.vertices = vertices;
			mesh.triangles = triangles;
			mesh.RecalculateBounds();
			mesh.RecalculateNormals();

			//Debug.Log ("show");


		}

	}

	public Tile GetTileAt(int x, int y){
		return map.GetTileAt(x, y);
	}
	public FogOfWarData getMap(){
		return map;
	}
	// Update is called once per frame
	void Update () {
		
		
	}


	
}
