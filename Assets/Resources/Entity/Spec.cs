using UnityEngine;
using System.Collections;

public class Spec : MonoBehaviour {

	public bool dontEat;
	public bool vegan;
	public bool carniv;
	//public string[] TypeOfFood;
    public TypeResource[] foodTypes;

    public float Maxlife;
	public float MaxHunger;
	public int rayVision;


	public BehaviorEnum behavior;
	//L'attaque de base

	public int minimumDamage;
	public int maximumDamage;
	public bool ranged = false;
	public int cooldown;


	//Abilities
	public AbilityType[] abilitiesType;
	public int[] levelAbilities;
	public GameObject[] effectAbility;


    //Special Behavior Script 


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
