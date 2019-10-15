using UnityEngine;
using System.Collections;

public class Tile{
	
	public GameObject staticEntity;
	public EnumTypeTile type;
	public int num;
	public int x,y;
	public bool show = false;

	
	public Tile(){
		
		staticEntity = null;
		
	}

	public Tile(EnumTypeTile eTile){
		type = eTile;
	}
	public Tile(int _num , EnumTypeTile eTile){
		num = _num;
		type = eTile;
		staticEntity = null;

	}

	public Tile(int _num , EnumTypeTile eTile,int px, int py){
		num = _num;
		type = eTile;
		x = px;
		y = py;
		staticEntity = null;
		
	}

	public Tile(int _num , EnumTypeTile eTile,int px, int py, bool sh){
		num = _num;
		type = eTile;
		x = px;
		y = py;
		staticEntity = null;
		show = sh;
		
	}

	public Tile(EnumTypeTile eTile, int px , int py){
		type = eTile;
		x = px;
		y = py;
		staticEntity = null;
		num = 8;
	}

	public void setEntity(GameObject gobj){
		staticEntity = gobj;
	}
	

	public void setNum(int n){
		num = n;
	}
	                        
	
	
}

