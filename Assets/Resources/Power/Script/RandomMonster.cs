using UnityEngine;
using System.Collections;

public class RandomMonster : MonoBehaviour {


		// Use this for initialization
	void Start () {
		RandomMonsterSpell ();


	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void RandomMonsterSpell(){
		Vector3 v3 = transform.position;
		GameObject g = Instantiate(Resources.Load ("Entity/Goblin/Goblin") , v3, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
		v3 = new Vector3(transform.position.x , 1.0f,transform.position.z);
		Instantiate(Resources.Load ("Power/EffectTerra") , v3, Quaternion.Euler(new Vector3(0,0,0)));


		//g.transform.position = v3;
		//g.AddComponent<Creature>();

		GameObject.Find ("Player").GetComponent<Player> ().addWorker (g);
		Destroy (gameObject);

		/*
		 * Le premier sort d'invocation n'a qu'un faible taux de créature rare et très rare
		 * 
		 * 1. Selection aléatoire de monstre parmis tous
		 * 2. La créature sera instanciée au meme endroit que le sort
		 * 3. Attribution au joueur.
		 * 
		*/



	}

	                        
}
