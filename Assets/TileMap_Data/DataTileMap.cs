using UnityEngine;

public class DataTileMap{

	int size_x;
	int size_y;

	Tile[,] map_data;
	//Tile[,] map_fog;



	public DataTileMap(int size_x, int size_y, bool show){
		this.size_x = size_x;
		this.size_y = size_y;

		map_data = new Tile[size_x,size_y];
		//map_fog = new Tile[size_x,size_y];

		int cptMountain = 0;
		int cptPrairie = 0;

		for (int y = 0; y < size_y; y++) {
			for (int x = 0; x < size_x; x++) {
				//map_fog[x,y] = new Tile(13,EnumTypeTile.FOG);

				if (map_data [x, y] == null) {
					int rdm = Random.Range (0, 5);

					if(y == 0 || x == 0 || x == size_x-1 || y == size_y-1 || x == 1 || y== 1 || x == size_x-2 || y == size_y-2){
						map_data[x,y] = new Tile(269, EnumTypeTile.GRASS,x,y,show);
					}
					else if (rdm> 3 ) {
						rdm = Random.Range (0, 4);
						if (rdm == 0) {
							if (cptPrairie < 3) {
								Debug.Log ("prairie");
								prairie (x, y, show);
								cptPrairie++;
							} else {
								map_data [x, y] = new Tile (0, EnumTypeTile.GRASS, x, y, show);

							}

						} else {
							map_data [x, y] = new Tile (0, EnumTypeTile.EARTH, x, y, show);
						}
					}
					else{
						map_data[x,y] = new Tile(0, EnumTypeTile.EARTH,x,y,show);

						//map_data[x,y] = new Tile(512, EnumTypeTile.STONE,x,y,show);


					}

				}
			}
		}


		//Texture plus épuré en fonction des voisines.
		//setNumTexture();

		///map_data[18,5] = new Tile(33, EnumTypeTile.GRASS);
		//	map_data [5, 5].type 54;

		waterPool (20, 20,show);

		//Prairie 
		prairie(20,20,show);
		mountain (50, 50,show);
		setNumTexture (show);
	}

	void setNumTexture(bool show){

		//bool vug = false, vdg = false, vrg = false, vlg = false,
		//vurg = false, vulg = false, vdrg = false, vdlg = false;

		bool vue = false, vde = false, vre = false, vle = false,
		vure = false, vule = false, vdre = false, vdle = false;

		bool border = false;
		for (int y = 0; y < size_y; y++) {
			for (int x = 0; x < size_x; x++) {
				border = false;
				//Si c'est au bord du monde 
				if(y == 0 || x == 0 || x == size_x-1 || y == size_y-1 ){
					border = true;
				}
				if(!border){

					//Fonction setTexture
					setNumeroToTile(map_data[x,y].type,x,y);

					/*
					switch(map_data[x,y].type){
						
					case EnumTypeTile.EARTH:
						vue = false; vde = false; vre = false; vle = false;
						vure = false; vule = false; vdre = false; vdle = false;


						if(map_data[x+1,y].type != EnumTypeTile.EARTH){
							vre = true;
						}
						if(map_data[x+1,y+1].type != EnumTypeTile.EARTH){
							vure = true;
						}
						if(map_data[x+1,y-1].type != EnumTypeTile.EARTH){
							vdre = true;
						}
						if(map_data[x,y+1].type != EnumTypeTile.EARTH){
							vue = true;
						}

						if(map_data[x-1,y+1].type != EnumTypeTile.EARTH){
							vule = true;
						}
						if(map_data[x,y-1].type != EnumTypeTile.EARTH){
							vde = true;
						}
						if(map_data[x-1,y-1].type != EnumTypeTile.EARTH){
							vdle = true;
						}
						if(map_data[x-1,y].type != EnumTypeTile.EARTH){
							vle = true;
						}


						int num = 13;
						if(vle){
							num =7;
							if (vue) {
								num = 34;

								if (vde) {
									num = 66;
									if (vre) {
										num = 228;
									}
								} else if (vre) {
									num = 67;
								} else if (vdre) {
									num = 224;
								}
							} else if (vde) {
								num = 33;
								if (vre) {
									num = 65;
								} else if (vure) {
									num = 227;
								}
							} else if (vre) {
								num = 37;
							} else if (vure) {
								num = 196;
								if (vdre) {
									num = 195;
								}
							} else if (vdre) {
								num = 197;
							}

						}
						else if(vde){
							num = 1;
							if (vre) {
								num = 32;
								if (vule) {
									num = 226;
								}
							} else if (vue) {
								num = 36;
							} else if (vure) {
								num = 162;
								if (vule) {
									num = 160;
								}
							} else if (vule) {
								num = 161;
							}
							
							
						}
						else if(vue){
							num = 5;
							if (vre) {
								num = 35;
								if (vule) {
									num = 225;
								}
							} else if (vdle) {
								num = 193;
								if (vdre) {
									num = 192;
								}
							} else if (vdre) {
								num = 194;
							}
							
							
						}
						else if(vre){
							num = 3;
							if (vule) {
								num = 164;
								if (vdle) {
									num = 163;
								}
							} else if (vdle) {
								num = 165;
							}
						}
						else if(vdle){
							num = 2;
							if (vure) {
								num = 101;
								if (vule) {
									num = 130;
									if (vdre) {
										num = 132;
									}
								} else if (vdre) {
									num = 131;
								}
							} else if (vule) {
								num = 99;
								if (vdre) {
									num = 128;
								}
							} else if (vdre) {
								num = 96;
							}

						}
						else if(vdre){
							num = 0;
							if (vule) {
								num = 98;
								if (vure) {
									num = 129;
								}
							} else if (vure) {
								num = 97;
							}
						}
						else if(vule){
							num = 6;
							if (vure) {
								num = 100;
							}
						}
						else if(vure){
							num = 4;
						}
						else{
							int randVar = Random.Range (0, 5);
							int randTile = 0;
							if (randVar == 0) {
								randTile = Random.Range (0, 4);
							}
							num = 13 + randTile;
						}
						map_data[x,y].setNum(num);




						break;
					case EnumTypeTile.GRASS:
						
						vug = false;
						vdg = false;
						vrg = false;
						vlg = false;
						vurg = false;
						vulg = false;
						vdrg = false;
						vdlg = false;

						vue = false;
						vde = false;
						vre = false;
						vle = false;
						vure = false;
						vule = false;
						vdre = false;
						vdle = false;

						if (map_data [x + 1, y].type == EnumTypeTile.GRASS) {
							vrg = true;
						}
						if (map_data [x + 1, y + 1].type == EnumTypeTile.GRASS) {
							vurg = true;
						}
						if (map_data [x + 1, y - 1].type == EnumTypeTile.GRASS) {
							vdrg = true;
						}
						if (map_data [x, y + 1].type == EnumTypeTile.GRASS) {
							vug = true;
						}
						
						if (map_data [x - 1, y + 1].type == EnumTypeTile.GRASS) {
							vulg = true;
						}
						if (map_data [x, y - 1].type == EnumTypeTile.GRASS) {
							vdg = true;
						}
						if (map_data [x - 1, y - 1].type == EnumTypeTile.GRASS) {
							vdlg = true;
						}
						if (map_data [x - 1, y].type == EnumTypeTile.GRASS) {
							vlg = true;
						}

						if (map_data [x + 1, y].type != EnumTypeTile.GRASS) {
							vre = true;
						}
						if (map_data [x + 1, y + 1].type != EnumTypeTile.GRASS) {
							vure = true;
						}
						if (map_data [x + 1, y - 1].type != EnumTypeTile.GRASS) {
							vdre = true;
						}
						if (map_data [x, y + 1].type != EnumTypeTile.GRASS) {
							vue = true;
						}
						
						if (map_data [x - 1, y + 1].type != EnumTypeTile.GRASS) {
							vule = true;
						}
						if (map_data [x, y - 1].type != EnumTypeTile.GRASS) {
							vde = true;
						}
						if (map_data [x - 1, y - 1].type != EnumTypeTile.GRASS) {
							vdle = true;
						}
						if (map_data [x - 1, y].type != EnumTypeTile.GRASS) {
							vle = true;
						}

						num = 13;
						if(vle){
							num =7;
							if (vue) {
								num = 34;

								if (vde) {
									num = 66;
									if (vre) {
										num = 228;
									}
								} else if (vre) {
									num = 67;
								} else if (vdre) {
									num = 224;
								}
							} else if (vde) {
								num = 33;
								if (vre) {
									num = 65;
								} else if (vure) {
									num = 227;
								}
							} else if (vre) {
								num = 37;
							} else if (vure) {
								num = 196;
								if (vdre) {
									num = 195;
								}
							} else if (vdre) {
								num = 197;
							}

						}
						else if(vde){
							num = 1;
							if (vre) {
								num = 32;
								if (vule) {
									num = 226;
								}
							} else if (vue) {
								num = 36;
							} else if (vure) {
								num = 162;
								if (vule) {
									num = 160;
								}
							} else if (vule) {
								num = 161;
							}


						}
						else if(vue){
							num = 5;
							if (vre) {
								num = 35;
								if (vule) {
									num = 225;
								}
							} else if (vdle) {
								num = 193;
								if (vdre) {
									num = 192;
								}
							} else if (vdre) {
								num = 194;
							}


						}
						else if(vre){
							num = 3;
							if (vule) {
								num = 164;
								if (vdle) {
									num = 163;
								}
							} else if (vdle) {
								num = 165;
							}
						}
						else if(vdle){
							num = 2;
							if (vure) {
								num = 101;
								if (vule) {
									num = 130;
									if (vdre) {
										num = 132;
									}
								} else if (vdre) {
									num = 131;
								}
							} else if (vule) {
								num = 99;
								if (vdre) {
									num = 128;
								}
							} else if (vdre) {
								num = 96;
							}

						}
						else if(vdre){
							num = 0;
							if (vule) {
								num = 98;
								if (vure) {
									num = 129;
								}
							} else if (vure) {
								num = 97;
							}
						}
						else if(vule){
							num = 6;
							if (vure) {
								num = 100;
							}
						}
						else if(vure){
							num = 4;
						}
						else{
							int randVar = Random.Range (0, 5);
							int randTile = 0;
							if (randVar == 0) {
								randTile = Random.Range (0, 4);
							}
							num = 13 + randTile;
						}
						num += 256; 
						map_data[x,y].setNum(num);




						break;

					case EnumTypeTile.STONE:
						
						vug = false;
						vdg = false;
						vrg = false;
						vlg = false;
						vurg = false;
						vulg = false;
						vdrg = false;
						vdlg = false;

						vue = false;
						vde = false;
						vre = false;
						vle = false;
						vure = false;
						vule = false;
						vdre = false;
						vdle = false;

						if (map_data [x + 1, y].type != EnumTypeTile.STONE) {
							vre = true;
						}
						if (map_data [x + 1, y + 1].type != EnumTypeTile.STONE) {
							vure = true;
						}
						if (map_data [x + 1, y - 1].type != EnumTypeTile.STONE) {
							vdre = true;
						}
						if (map_data [x, y + 1].type != EnumTypeTile.STONE) {
							vue = true;
						}

						if (map_data [x - 1, y + 1].type != EnumTypeTile.STONE) {
							vule = true;
						}
						if (map_data [x, y - 1].type != EnumTypeTile.STONE) {
							vde = true;
						}
						if (map_data [x - 1, y - 1].type != EnumTypeTile.STONE) {
							vdle = true;
						}
						if (map_data [x - 1, y].type != EnumTypeTile.STONE) {
							vle = true;
						}

						num = 13;
						if(vle){
							num =7;
							if (vue) {
								num = 34;

								if (vde) {
									num = 66;
									if (vre) {
										num = 228;
									}
								} else if (vre) {
									num = 67;
								} else if (vdre) {
									num = 224;
								}
							} else if (vde) {
								num = 33;
								if (vre) {
									num = 65;
								} else if (vure) {
									num = 227;
								}
							} else if (vre) {
								num = 37;
							} else if (vure) {
								num = 196;
								if (vdre) {
									num = 195;
								}
							} else if (vdre) {
								num = 197;
							}

						}
						else if(vde){
							num = 1;
							if (vre) {
								num = 32;
								if (vue) {
									num = 64;
								}
								else if (vule) {
									num = 226;
								}
							} else if (vue) {
								num = 36;
							} else if (vure) {
								num = 162;
								if (vule) {
									num = 160;
								}
							} else if (vule) {
								num = 161;
							}


						}
						else if(vue){
							num = 5;
							if (vre) {
								num = 35;
								if (vdle) {
									num = 225;
								}
							} else if (vdle) {
								num = 193;
								if (vdre) {
									num = 192;
								}
							} else if (vdre) {
								num = 194;
							}


						}
						else if(vre){
							num = 3;
							if (vule) {
								num = 164;
								if (vdle) {
									num = 163;
								}
							} else if (vdle) {
								num = 165;
							}
						}
						else if(vdle){
							num = 2;
							if (vure) {
								num = 101;
								if (vule) {
									num = 130;
									if (vdre) {
										num = 132;
									}
								} else if (vdre) {
									num = 131;
								}
							} else if (vule) {
								num = 99;
								if (vdre) {
									num = 128;
								}
							} else if (vdre) {
								num = 96;
							}

						}
						else if(vdre){
							num = 0;
							if (vule) {
								num = 98;
								if (vure) {
									num = 129;
								}
							} else if (vure) {
								num = 97;
							}
						}
						else if(vule){
							num = 6;
							if (vure) {
								num = 100;
							}
						}
						else if(vure){
							num = 4;
						}
						else{
							num = 13;
						}
						num += 512; 
						map_data[x,y].setNum(num);




						break;
					default:
					break;
					}
					*/
				}
			}
					           
		}

	}


	public void setNumeroToTile(EnumTypeTile type,int x , int y){


		bool vue = false, vde = false, vre = false, vle = false,
		vure = false, vule = false, vdre = false, vdle = false;


		if(map_data[x+1,y].type != type){
			vre = true;
		}
		if(map_data[x+1,y+1].type != type){
			vure = true;
		}
		if(map_data[x+1,y-1].type != type){
			vdre = true;
		}
		if(map_data[x,y+1].type != type){
			vue = true;
		}

		if(map_data[x-1,y+1].type != type){
			vule = true;
		}
		if(map_data[x,y-1].type != type){
			vde = true;
		}
		if(map_data[x-1,y-1].type != type){
			vdle = true;
		}
		if(map_data[x-1,y].type != type){
			vle = true;
		}


		int num = 13;
		if(vle){
			num =7;
			if (vue) {
				num = 34;

				if (vde) {
					num = 66;
					if (vre) {
						num = 228;
					}
				} else if (vre) {
					num = 67;
				} else if (vdre) {
					num = 224;
				}
			} else if (vde) {
				num = 33;
				if (vre) {
					num = 65;
				} else if (vure) {
					num = 227;
				}
			} else if (vre) {
				num = 37;
			} else if (vure) {
				num = 196;
				if (vdre) {
					num = 195;
				}
			} else if (vdre) {
				num = 197;
			}

		}
		else if(vde){
			num = 1;
			if (vre) {
				num = 32;
				if (vule) {
					num = 226;
				}
			} else if (vue) {
				num = 36;
			} else if (vure) {
				num = 162;
				if (vule) {
					num = 160;
				}
			} else if (vule) {
				num = 161;
			}


		}
		else if(vue){
			num = 5;
			if (vre) {
				num = 35;
				if (vule) {
					num = 225;
				}
			} else if (vdle) {
				num = 193;
				if (vdre) {
					num = 192;
				}
			} else if (vdre) {
				num = 194;
			}


		}
		else if(vre){
			num = 3;
			if (vule) {
				num = 164;
				if (vdle) {
					num = 163;
				}
			} else if (vdle) {
				num = 165;
			}
		}
		else if(vdle){
			num = 2;
			if (vure) {
				num = 101;
				if (vule) {
					num = 130;
					if (vdre) {
						num = 132;
					}
				} else if (vdre) {
					num = 131;
				}
			} else if (vule) {
				num = 99;
				if (vdre) {
					num = 128;
				}
			} else if (vdre) {
				num = 96;
			}

		}
		else if(vdre){
			num = 0;
			if (vule) {
				num = 98;
				if (vure) {
					num = 129;
				}
			} else if (vure) {
				num = 97;
			}
		}
		else if(vule){
			num = 6;
			if (vure) {
				num = 100;
			}
		}
		else if(vure){
			num = 4;
		}
		else{
			int randVar = Random.Range (0, 5);
			int randTile = 0;
			if (randVar == 0) {
				randTile = Random.Range (0, 4);
			}
			num = 13 + randTile;
		}
		switch (type) {

		case EnumTypeTile.EARTH:
			
			break;

		case EnumTypeTile.GRASS:
			num += 256;
			break;

		case EnumTypeTile.STONE:
			num += 512;
			break;
		default:
			break;
		}

		map_data[x,y].setNum(num);

	}
	                                             
	public DataTileMap(){
		this.size_x = 20;
		this.size_y = 20;

		map_data = new Tile[size_x,size_y];

		
	}

	public void mountain(int x, int y,bool show){

		if (x + 2 >= size_x || y + 2 >= size_y) {
			return;
		}

		int rand = Random.Range (30, 50);
		int rand2 = Random.Range (30, 50);
		int i = 0;
		int j = 0;



		for (; i < rand && x + i < size_x; i++) {
			for (j =0; j < rand2 && y + j < size_y; j++) {
				
				map_data [x + i, y + j] = new Tile (13, EnumTypeTile.STONE, x + i, y + j, show);

			}
		}


		//Bordure de la montagne plus "réelle"  A TRAVAILLER
		if (y - 1 >= 0) {
			for (i = 0; i < rand; i++) {
				int randBord = Random.Range (0, 2);
				if (randBord == 1 && x + i < size_x) {
					map_data [x +i, y-1] = new Tile (13, EnumTypeTile.STONE, x +i, y-1, show);

				}
			}
		}
		if (x - 1 >= 0) {
			for (i = 0; i < rand2; i++) {
				int randBord = Random.Range (0, 2);
				if (randBord == 1 && y + i < size_x) {
					map_data [x -1, y+i] = new Tile (13, EnumTypeTile.STONE, x -1, y+i, show);

				}
			}
		}

		if (y + rand2< size_y) {
			for (i = 0; i < rand; i++) {
				int randBord = Random.Range (0, 2);
				if (randBord == 1 && x + i < size_x) {
					map_data [x +i, y+rand2] = new Tile (13, EnumTypeTile.STONE, x +i, y+rand2, show);

				}
			}
		}
		if (x + rand< size_x) {
			for (i = 0; i < rand2; i++) {
				int randBord = Random.Range (0, 2);
				if (randBord == 1 && y + i < size_x) {
					map_data [x +rand, y+i] = new Tile (13, EnumTypeTile.STONE,x +rand, y+i, show);

				}
			}
		}
	}
	public void prairie(int x, int y,bool show){
		if (x + 2 >= size_x || y + 2 >= size_y) {
			return;
		}

		int rand = Random.Range (5, 25);
		int rand2 = Random.Range (5, 25);
		int i = 0;
		int j = 0;


		for (; i < rand && x + i < size_x; i++) {
			for (j =0; j < rand2 && y + j < size_y; j++) {

				map_data [x + i, y + j] = new Tile (13, EnumTypeTile.GRASS, x + i, y + j, show);

			}
		}


		//Bordure de la prairie plus "réelle"  A TRAVAILLER
		if (y - 1 >= 0) {
			for (i = 0; i < rand; i++) {
				int randBord = Random.Range (0, 2);
				if (randBord == 1 && x + i < size_x) {
					map_data [x +i, y-1] = new Tile (13, EnumTypeTile.GRASS, x +i, y-1, show);

				}
			}
		}
		if (x - 1 >= 0) {
			for (i = 0; i < rand2; i++) {
				int randBord = Random.Range (0, 2);
				if (randBord == 1 && y + i < size_x) {
					map_data [x -1, y+i] = new Tile (13, EnumTypeTile.GRASS, x -1, y+i, show);

				}
			}
		}

		if (y + rand2< size_y) {
			for (i = 0; i < rand; i++) {
				int randBord = Random.Range (0, 2);
				if (randBord == 1 && x + i < size_x) {
					map_data [x +i, y+rand2] = new Tile (13, EnumTypeTile.GRASS, x +i, y+rand2, show);

				}
			}
		}
		if (x + rand< size_x) {
			for (i = 0; i < rand2; i++) {
				int randBord = Random.Range (0, 2);
				if (randBord == 1 && y + i < size_x) {
					map_data [x +rand, y+i] = new Tile (13, EnumTypeTile.GRASS,x +rand, y+i, show);

				}
			}
		}
	}

	public void waterPool(int x, int y,bool show){

		if (x + 2 >= size_x || y + 2 >= size_y) {
			return;
		}

		map_data [x, y] = new Tile(15, EnumTypeTile.WATER,x,y,show);
		map_data [x, y+1] = new Tile(15, EnumTypeTile.WATER,x,y,show);
		map_data [x, y+2] = new Tile(15, EnumTypeTile.WATER,x,y,show);
		map_data [x+1, y] = new Tile(15, EnumTypeTile.WATER,x,y,show);
		map_data [x+1, y+1] = new Tile(15, EnumTypeTile.WATER,x,y,show);
		map_data [x+1, y+2] = new Tile(15, EnumTypeTile.WATER,x,y,show);
		map_data [x+2, y] = new Tile(15, EnumTypeTile.WATER,x,y,show);
		map_data [x+2, y+1] = new Tile(15, EnumTypeTile.WATER,x,y,show);
		map_data [x+2, y+2] = new Tile(15, EnumTypeTile.WATER,x,y,show);







	}

	public void SpawnPlant(){



	}
	public Tile GetTileAt(int x, int y){

		return map_data [x, y];
	}

}
