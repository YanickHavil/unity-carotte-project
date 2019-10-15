using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class Creature : MonoBehaviour {



	Temple temple;
	Player player;
	public GameObject enemy = null;
	public GameObject sprite;
	Spec carac;
	UnityEngine.AI.NavMeshAgent agent;
	Attributes att;
	MovementManager movement;
	WorkManager work;
	GameObject spriteManager;
	FoodManager foodManager;
	CombatManager combatManager;
    InfluenceManager influM;

    // Use this for initialization
    void Start() {
        att = GetComponent<Attributes>();
        carac = GetComponent<Spec>();

        //On applique les stats
        att.life = carac.Maxlife;
        att.maxLife = carac.Maxlife;
        att.life = carac.Maxlife;
        att.hungerMax = carac.MaxHunger;
        att.hunger = carac.MaxHunger;
        att.rayVision = carac.rayVision;


        //Debug.Log("Entity/" + att.name.ToString() + "/" + att.name.ToString() + "Sprite");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        spriteManager = Instantiate(sprite, Vector3.zero, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        spriteManager.transform.parent = gameObject.transform;
        spriteManager.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
        spriteManager.transform.localPosition = new Vector3(0, 0, 0);//Vector3.zero;
        spriteManager.transform.localScale = new Vector3(1, 1, 1);

        GameObject lifebar = Instantiate(Resources.Load("UI/HealthBar"), Vector3.zero, Quaternion.Euler(new Vector3(-90f, 0, 0))) as GameObject;
        lifebar.transform.parent = transform;
        lifebar.transform.localPosition = new Vector3(0, 0, 0.22f);
        lifebar.transform.localRotation = Quaternion.Euler(new Vector3(-90.0f, 0.0f, 0.0f));
        lifebar.transform.localScale = new Vector3(0.3f, 0.1f, 0.5f);

        lifebar.GetComponentInChildren<LifeBarScript>().init();
        lifebar.GetComponentInChildren<LifeBarScript>().att = att;


        
        att.lifebar = lifebar;

        lifebar.SetActive(false);


        movement = gameObject.AddComponent<MovementManager>();
        work = gameObject.AddComponent<WorkManager>();
        influM = gameObject.AddComponent<InfluenceManager>();

        //Special Behavior algo
        String str = att.name + "Behavior";
        //Debug.Log(" behavior name = " + str);
        Type t = getType(str);
        //Debug.Log("type name = " + t);
        if (t != null)
        {
            gameObject.AddComponent(t);
            Debug.Log("add Behavior");
        }

		//Ici on va différencier les spécificités de chaque créature grace a un script qui contiendra toute les données de spécificité (Spec).
		foodManager = gameObject.AddComponent<FoodManager> (); 

		//Attribut de faim a définir pour chaque créature
		foodManager.dontEat = carac.dontEat;
		foodManager.vegetarian = carac.vegan;
		foodManager.carnivorous = carac.carniv;
        foodManager.foodTypes = carac.foodTypes;
		//foodManager.typeOfFood = carac.TypeOfFood;

		combatManager = gameObject.AddComponent<CombatManager> ();
		//Ajout de l'attaque de base celon spec
		combatManager.basicatk = new BasicAttack (carac.minimumDamage, carac.maximumDamage, carac.ranged, carac.cooldown);
		player = GameObject.Find ("Player").GetComponent<Player>();

        //Instanciation liste abilities
        att.setListAbilities();
        //Ajout des capacités        
        for (int i = 0; i < carac.abilitiesType.Length; i++) {
			if (carac.levelAbilities [i] != null
			    && carac.levelAbilities [i] > 0) {

				if (carac.effectAbility.Length > i) {
					if (carac.effectAbility [i] != null) {
						att.abilities.Add(new ActiveAbility(carac.abilitiesType[i],carac.levelAbilities[i],carac.effectAbility[i]));
					}
				}
				else{
					if (carac.abilitiesType.Length > i) {
						if (carac.abilitiesType [i] != null) {
                            PassiveAbility p = new PassiveAbility(carac.abilitiesType[i], carac.levelAbilities[i]);

                            att.abilities.Add (new PassiveAbility (carac.abilitiesType [i], carac.levelAbilities [i]));

						}
					} else {
						Debug.Log ("Probleme chargement ability");
					}

				}

			}
				
		}


	}

    //Find type of script attached for special behavior

    public static Type getType(string typeName)
    {
        var t = Type.GetType(typeName);

        if(t!= null)
        {
            return t;
        }
        if (typeName.Contains(".")){

            var assemblyname = typeName.Substring(0, typeName.IndexOf('.'));

            var assembly = Assembly.Load(assemblyname);
            if(assembly == null)
            {

                return null;
            }
            t = assembly.GetType(typeName);
            if(t != null)
            {
                return t;
            }
        }


        var currentAssembly = Assembly.GetExecutingAssembly();
        var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
        foreach(var assemblyName in referencedAssemblies)
        {
            var assembly = Assembly.Load(assemblyName);
            if(assembly != null)
            {

                t = assembly.GetType(typeName);
                if(t != null)
                {
                    return t;
                }
            }
        }
        return null;
    }

	// Update is called once per frame
	void Update () {

	}





	public void setPlayer(Player p){

		player = p;

	}






	public bool isWorking(){
		return work;
	}









}
