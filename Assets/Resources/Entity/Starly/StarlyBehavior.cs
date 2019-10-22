using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarlyBehavior : MonoBehaviour {

    WorkManager workM;
    FoodManager foodM;
    Attributes att;
    GameObject  seedplanted;
    // Use this for initialization
    void Start () {
        workM = gameObject.GetComponent<WorkManager>();
        foodM = gameObject.GetComponent<FoodManager>();
        att = gameObject.GetComponent<Attributes>();
        InvokeRepeating("SpecialBehavior", 180f, 180f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpecialBehavior()
    {
        Tile t = att.getNearEmptyTile(EnumTypeTile.EARTH);
        Vector3 v3 = new Vector3(t.x + 1 / 2, 1.2f, t.y + 1 / 2);
        workM.t = new Task(v3, TaskEnum.SEED, AbilityType.SEED, seedplanted);

    }
}
