using UnityEngine;
using System.Collections.Generic;

public class Inventory {


	List<Resource> list;

	public Inventory(){
		list = new List<Resource> ();
	}

	public void addResource(Resource r){
		list.Add(r);
	}

	public Resource getResource(){
		
		if (list [0] != null) {
			Resource res = list [0];
			list.RemoveAt (0);
			return res;

		}
		return null;
	}


}
