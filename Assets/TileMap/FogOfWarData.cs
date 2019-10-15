using UnityEngine;
using System.Collections;

public class FogOfWarData{


	int size_x;
	int size_y;

	Tile[,] map_data;

	public FogOfWarData(int size_x, int size_y){

		this.size_x = size_x;
		this.size_y = size_y;


		map_data = new Tile[size_x,size_y];

		for (int y = 0; y < size_y; y++) {
			for (int x = 0; x < size_x; x++) {
				map_data[x,y] = new Tile(13,EnumTypeTile.FOG,x,y);
				

			}
		}
	}



	public Tile GetTileAt(int x, int y){
		return map_data [x, y];
	}
}
