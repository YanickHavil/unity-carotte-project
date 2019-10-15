using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Task{
	public GameObject creature;
	public GameObject cibled;
	public GameObject gobjconstruct;
    public GameObject icon;
	public TaskEnum type;
	public Vector3 destination;
	public List<AbilityType> requierement;


	public Task(GameObject b, TaskEnum t, AbilityType require){
		creature = null;
		cibled = b;
		type = t;
		requierement = new List<AbilityType> ();
		requierement.Add(require);
	}
    public Task(Vector3 v3 , TaskEnum t, AbilityType require,GameObject obj)
    {
        creature = null;
        cibled = null;
        type = t;
        requierement = new List<AbilityType>();
        requierement.Add(require);
        destination = v3;
        gobjconstruct = obj;
    }
	public Task(GameObject b,TaskEnum t,Vector3 position){
		creature = null;
		cibled = b;
		type = t;
		destination = position;
	}
	public Task(GameObject a, GameObject b,TaskEnum t){
		creature = a;
		cibled = b;
		type = t;
	}
	public Task(GameObject a,TaskEnum t){
		creature = null;
		cibled = a;
		type = t;
	}
	public bool isSame(Task t){
		if (t.cibled == cibled && type == t.type) {
			return true;
		} else {
			return false;
		}
	}



}
