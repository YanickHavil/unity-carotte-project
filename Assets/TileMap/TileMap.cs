using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter) )]
[RequireComponent(typeof(MeshCollider) )]
[RequireComponent(typeof(MeshRenderer) )]

public class TileMap : MonoBehaviour {

	public int size_x = 100;
	public int size_z = 50;
	public float tileSize = 1.0f;

	public Texture2D terrainTiles;
	public int tileResolution;

	public bool fogOfWar;
	Texture2D texture ;
	Color[][] tiles;
	DataTileMap map;

	GameObject player;
	GameObject gbfog;
	FogOfWar fog;
	FogOfWarData datafog;

	Collider col;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		col = GetComponent<Collider> ();
		map = new DataTileMap (size_x, size_z,!fogOfWar);
		BuildMesh ();
		if (fogOfWar) {
			gbfog = Instantiate(Resources.Load("FogOfWar/FogOfWar"), 
			                    new Vector3 (0  ,0.1f,0),
								Quaternion.identity) as GameObject;
			fog = gbfog.GetComponent<FogOfWar> ();
			fog.init (size_x,size_z,tileSize,tileResolution,tiles,terrainTiles);
			datafog = fog.getMap ();
		}

		SpawnAvatar ();
		addInputManager ();
		//makeBlockingTile ();
		Spawn ();

		MakeObstacle ();

	}

	void addInputManager(){
		if (player.GetComponent<InputManager> () != null) {
			DestroyImmediate(player.GetComponent<InputManager> ());
		}
		if (player.GetComponent<SelectionManager> () != null) {
			DestroyImmediate(player.GetComponent<SelectionManager> ());

		}
		player.AddComponent<InputManager> ();
		player.AddComponent<SelectionManager> ();

	}
	void SpawnAvatar(){
		GameObject avatar = Instantiate (Resources.Load("Player/AvatarPlayer"),
		                                 new Vector3(7,1.2f,7),
		                                 Quaternion.Euler(new Vector3(0,40,0))) as GameObject;

		Attributes pl = avatar.GetComponent<Attributes> ();
		pl.setPlayer (player.GetComponent<Player>());
		//avatar.GetComponent<Attributes> ().setPlayer (1);
	}


	public void MakeObstacle(){
		//deleteGameObject ("obstacle");
		Debug.Log ("obstacles");
		for (int i = 0; i < size_x; i++) {
			for(int j = 0; j<size_z;j++){
				Tile t = map.GetTileAt (i, j);
				if (t.type == EnumTypeTile.STONE) {
					t.staticEntity = Instantiate(Resources.Load("Obstacle/StoneObstacle",typeof(GameObject)),
						new Vector3 (t.x + tileSize/2 ,1.2f,t.y + tileSize/2),
						Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
				}
			}

		}
	}

	Color[][] ChopUpTiles(){
		int numTilesPerRow = terrainTiles.width / tileResolution;
		int numRows = terrainTiles.height / tileResolution;
		Color[][] tiles = new Color[numTilesPerRow*numRows][];

		for (int y=0; y<numRows; y++) {
			for (int x=0;x<numTilesPerRow; x++) {
				tiles[y*numTilesPerRow + x] = terrainTiles.GetPixels(x*tileResolution, y * tileResolution , tileResolution, tileResolution);
			}
		}
		return tiles;
		//Color[] p = terrainTiles.GetPixels(16,16,tileResolution, tileResolution);

	}

	void removeSelectionEntity(){
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hitInfo;
		
		if (GetComponent<Collider> ().Raycast (ray, out hitInfo, Mathf.Infinity)) {
			if( Input.GetMouseButtonDown(0)){
				//sinon bug
				if (!player.GetComponent<SelectionManager> ().inSelection) {
					GameObject.Find("Player").GetComponent<Player>().removeSelection();
				}

				
			}
		}
	}
	void BuildTexture(){

		int texWidth = size_x * tileResolution;
		int texHeight = size_z * tileResolution;
		texture = new Texture2D (texWidth, texHeight);

		tiles = ChopUpTiles ();

		for(int y = 0; y < size_z; y++){
			for(int x = 0; x < size_x; x++){
				//int terrainOffset = Random.Range(0,2) * tileResolution;
				//int terrainOffsetY = Random.Range(0,2) * tileResolution;
				Color[] p;
				//Debug.Log ("num = " + map.GetTileAt(x,y).num); 
				p = tiles [map.GetTileAt(x,y).num];



				texture.SetPixels(x* tileResolution, y*tileResolution, tileResolution, tileResolution,p);
			}
		}

		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.Apply();

		MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		mesh_renderer.sharedMaterials[0].mainTexture = texture;

		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		Color[] colors = new Color[mesh.vertices.Length];
		int i = 0;
		while (i < mesh.vertices.Length) {
			colors[i] = Color.Lerp(Color.white, Color.black, mesh.vertices[i].y);
			i++;
		}
		mesh.colors = colors;

		Debug.Log("Done Texture!");
			
	}




	public void changeTile(int x , int y  , EnumTypeTile type){
		Tile t = map.GetTileAt (x, y);
		t.type = type;
		map.setNumeroToTile (type, x, y);


		//changeTileTexture (x, y,t.num);

		//attention bordure de map

		for (int i = x - 1; i <= x + 1; i++) {
			for (int j = y - 1; j <= y + 1; j++) {
				map.setNumeroToTile (map.GetTileAt (i, j).type, i, j);

				changeTileTexture (i, j, map.GetTileAt (i, j).num);
			}
		}

	}

	public void changeTileTexture(int x, int y, int num){
		//int texWidth = size_x * tileResolution;
		//int texHeight = size_z * tileResolution;

		Color[] p = tiles [num];
		texture.SetPixels(x* tileResolution, y*tileResolution, tileResolution, tileResolution,p);


		//texture.filterMode = FilterMode.Point;
		//texture.wrapMode = TextureWrapMode.Clamp;
		texture.Apply();
		
		//MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		//mesh_renderer.sharedMaterials[0].mainTexture = texture;
		
		Debug.Log("Done change Texture!");
	}


	public void showTile (int x , int y){

		if (fogOfWar) {
			map.GetTileAt(x,y).show = true;
			fog.showTile (x,y);

		}


	}

	public void BuildMesh(){


		deleteLeakGameobject ();

		map = new DataTileMap (size_x, size_z,!fogOfWar);

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

				vertices[ z * vsize_x + x ] = new Vector3( x*tileSize, 1, z*tileSize );
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

		/*
		verticles [0] = new Vector3 (0, 0, 0);
		verticles [1] = new Vector3 (1, 0, 0);
		verticles [2] = new Vector3 (0, 0, -1);
		verticles [3] = new Vector3 (1, 0, -1);

		triangles [0] = 0;
		triangles [1] = 3;
		triangles [2] = 2;

		triangles [3] = 0;
		triangles [4] = 1;
		triangles [5] = 3;

		normals [0] = Vector3.up;
		normals [1] = Vector3.up;
		normals [2] = Vector3.up;;
		normals [3] = Vector3.up;

		uv [0] = new Vector2 (0,1);
		uv [1] = new Vector2 (1,1);
		uv [2] = new Vector2 (0,0);
		uv [3] = new Vector2 (1,0);

		*/
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

	void deleteLeakGameobject(){
		deleteGameObject ("FogOfWar");
		deleteGameObject ("Avatar");
		Destroy(GameObject.Find("Player").GetComponent<InputManager>());

	}
	public void BuildFogMesh(){

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
				
				vertices[ z * vsize_x + x ] = new Vector3( x*tileSize, 2, z*tileSize );
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
	public void Spawn(){
		deleteGameObject ("Resource");
		//deleteGameObject ("Tree");

		Tile t;
		/*
		t.staticEntity = Instantiate(Resources.Load("Plant/Tree", typeof(GameObject)),
		                             new Vector3 (18 + tileSize/2 ,0.6f,5 + tileSize/2),



		int numtest = 3;
		GameObject bigObject2 = Instantiate(Resources.Load("InGameResources/MysticTree", typeof(GameObject)),
		                                    new Vector3 (numtest   ,1.2f,numtest ),
		                                   Quaternion.Euler(new Vector3(90,0,0))) as GameObject;
		map.GetTileAt(numtest,numtest).staticEntity = bigObject2;
		map.GetTileAt(numtest+1,numtest+1).staticEntity = bigObject2;
		map.GetTileAt(numtest+1,numtest).staticEntity = bigObject2;
		map.GetTileAt(numtest,numtest+1).staticEntity = bigObject2;
		*/
	
		for (int i = 0;i<100;i++) {
			int rand = Random.Range (2, size_x - 4);
			int rand2 = Random.Range (2, size_z - 4);
			t = map.GetTileAt(rand,rand2);
			if(t.staticEntity == null 
				&& map.GetTileAt(rand,rand2).type != EnumTypeTile.WATER 
				&& map.GetTileAt(rand,rand2).type != EnumTypeTile.STONE){

				int randType = Random.Range(0,5);
				if(map.GetTileAt(rand,rand2).staticEntity == null
				   && randType > 2){

					forest (rand,rand2);
				}

				else if(randType == 0){
					t.staticEntity = Instantiate(Resources.Load("InGameResources/FlowerT", typeof(GameObject)),
					                             new Vector3 (rand + tileSize/2 ,1.1f,rand2 + tileSize/2),
					                             Quaternion.Euler(new Vector3(90,0,0))) as GameObject;	

				}
				else{
					t.staticEntity = Instantiate(Resources.Load("InGameResources/FlowerT", typeof(GameObject)),
					                             new Vector3 (rand + tileSize/2 ,1.1f,rand2 + tileSize/2),
					                             Quaternion.Euler(new Vector3(90,0,0))) as GameObject;	
				}
				/*
				else{
					int token = Random.Range(0,2);
					if(token == 0){
						t.staticEntity = Instantiate(Resources.Load("Plant/Flower", typeof(GameObject)),
						                             new Vector3 (rand + tileSize/2 ,0.2f,rand2 + tileSize/2),
						                             Quaternion.Euler(new Vector3(0,30,0))) as GameObject;	
					}
					else{
						t.staticEntity = Instantiate(Resources.Load("Plant/Tree", typeof(GameObject)),
						                             new Vector3 (rand + tileSize/2 ,0.6f,rand2 + tileSize/2),
						                             Quaternion.Euler(new Vector3(0,30,0))) as GameObject;	
					}


				}
				*/
				//Debug.Log ("rand : " + rand);
				//Debug.Log ("rand2 : " + rand2);
				//Debug.Log ("rand" + rand);
			

			}
			else{
				//Debug.Log("Ya deja quelqu'un !");
			}
		}

	
		Debug.Log ("Spawn Done");

	}

	public void forest(int x , int y){



		int size = Random.Range (20, 50);
		int incX = x;
		int incY = y;
		int type = Random.Range(0,2);
		ArrayList sizeForest = new ArrayList();
		int indextab = 0;

		for (int i = 0; i <size; i++) {

			if(incX > 2 && incX <size_x -4 && incY >2 && incY < size_z-4){

				Tile t = map.GetTileAt (incX, incY);

				if(t.staticEntity == null){

					sizeForest.Add (t);


					indextab++;
				}
			}

			int randChange = Random.Range(0,4);
			if(randChange == 0){
				incX -=1;
				
			}
			else if(randChange == 1){
				incY+=1;
			}
			else{
				incX+=1;
			}

		}


		ArrayList tileSort = new ArrayList ();

		int xMax = size_x;
		foreach(Tile t in sizeForest){
			if(t.x >xMax){
				xMax = t.x;
				
			}
		}

		for(int i = 0 ; sizeForest.Count> tileSort.Count;){

			bool find = false;
			Tile temp = null;
			foreach(Tile t in sizeForest){

				if(t.x == xMax){
					tileSort.Add(t);
					temp = t;
					find = true;
				}

			}
			if(!find){
				xMax-=1;
			}
			else{
				sizeForest.Remove(temp);
			}

		}
		foreach(Tile t in tileSort) {
			if (t.type != EnumTypeTile.WATER
			    && t.type != EnumTypeTile.STONE) {
				if (type == 0 && t.staticEntity == null) {
					GameObject tree = Instantiate(Resources.Load("InGameResources/MysticTree", typeof(GameObject)),
						new Vector3 (t.x + tileSize/2  ,1.1f,t.y + tileSize/2),
						Quaternion.Euler(new Vector3(90,0,0))) as GameObject;
					t.staticEntity = tree;
				} else if (t.staticEntity == null){
					GameObject tree = Instantiate(Resources.Load("InGameResources/Tree2", typeof(GameObject)),
						new Vector3 (t.x + tileSize/2  ,1.1f,t.y + tileSize/2),
						Quaternion.Euler(new Vector3(90,0,0))) as GameObject;
					t.staticEntity = tree;
				}

			}


			
		}
	}
	
	public void setTextureTile(int x, int y, int num,EnumTypeTile type){
		//changeTile (x, y, num, type);
	}

	public void setTextureTile(int x, int y){
		//changeTile (x, y, 8, EnumTypeTile.UNKNOWN);
	}

	void deleteGameObject(string tag){
		GameObject[] collec = GameObject.FindGameObjectsWithTag (tag);
		foreach (GameObject gb in collec) {
			DestroyImmediate(gb);
		}
	}
	public DataTileMap getMap(){
		return map;
	}
	// Update is called once per frame
	void Update () {
	
		//removeSelectionEntity ();
	}
	public Vector3 GetMousePosition() { 
			RaycastHit hitInfo;
			Vector3 pos = Input.mousePosition;
			pos.y = 0;
			pos = Camera.main.ScreenToWorldPoint(pos);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			//Debug.Log(pos);
			if (Physics.Raycast (ray, out hitInfo, Mathf.Infinity))
			{
				//float x = Mathf.Floor ((hitInfo.point.x / tileSize));
				//float z = Mathf.Floor ((hitInfo.point.z / tileSize));
				float x = hitInfo.point.x / tileSize;
				float z = hitInfo.point.z / tileSize;
				float y = 0;
				//Debug.Log ("x = " + x + "  y = " + y + " z = " + z ) ;
				
				return new Vector3 (x, y, z);
		} else {

			//a revoir
			return Vector3.zero;
		}

	}

	public Tile getTileAtMousePosition(){
		RaycastHit hitInfo;
		Vector3 pos = Input.mousePosition;
		pos.y = 0;
		pos = Camera.main.ScreenToWorldPoint(pos);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hitInfo, Mathf.Infinity)) {

			int x = Mathf.FloorToInt(hitInfo.point.x);
			int z = Mathf.FloorToInt(hitInfo.point.z);
			//Debug.Log("pos x = " + x);
			//Debug.Log("pos z = " + z);

			if ((x < 0 || z < 0) || (x >= size_x || z >= size_z)) {
				return null;
			}
			return map.GetTileAt (x,z);


		} else {
			return null;
		}
	}

}
