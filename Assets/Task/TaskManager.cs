using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TaskManager : MonoBehaviour {


	List<Task> tasks;

	Player player;
	// Use this for initialization
	void Start () {
		tasks = new List<Task> ();
		player = GetComponent<Player> ();
		InvokeRepeating ("taskManager",1,1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void taskManager(){
		if (tasks.Count > 0) {
            if (player.haveWorker())
            {
                List<GameObject> worker = player.getWorkers();
                List<GameObject> creatures = player.getCreatures();
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (tasks[i].creature == null)
                    {
                        for (int j = 0; j < worker.Count; j++)
                        {
                            if (worker[j] != null)
                            {
                                if (worker[j].GetComponent<WorkManager>().t == null
                                    && checkRequierement(tasks[i], worker[j]))
                                {
                                    if (worker[j].GetComponent<WorkManager>().attributeTask(tasks[i]))
                                    {
                                        tasks[i].creature = worker[j];
                                        Debug.Log("Task attributed !");
                                        break;

                                    }

                                }
                            }

                        }
                    }


                }
            }
            else if(player.haveCreature())
            {

                List<GameObject> creatures = player.getCreatures();
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (tasks[i].creature == null)
                    {
                        for (int j = 0; j < creatures.Count; j++)
                        {
                            if (creatures[j] != null)
                            {
                                if (creatures[j].GetComponent<WorkManager>().t == null
                                    && checkRequierement(tasks[i], creatures[j]))
                                {
                                    if (creatures[j].GetComponent<WorkManager>().attributeTask(tasks[i]))
                                    {
                                        tasks[i].creature = creatures[j];
                                        Debug.Log("Task attributed !");
                                        break;

                                    }

                                }
                            }

                        }
                    }


                }
            }

		}
		removeObsoleteTask();
        
	}

	void removeObsoleteTask(){

		//A revoir
		for (int i = 0; i< tasks.Count;i++) {
			/*
			if(!player.haveCreature()){
				tasks.Remove(t);

			}*/
			if(tasks[i].cibled == null){
				/*
				if (tasks[i].creature.GetComponent<WorkManager> ().t == tasks[i]) {
					tasks.RemoveAt(i);
				}*/
				removeTask (i);
				Debug.Log ("tache enlevée");
			}
		}

	}

	bool checkRequierement(Task t,GameObject worker){
		if (t.requierement != null) {
			foreach (Ability pa in worker.GetComponent<Attributes>().abilities) {
				if (pa.GetType () == typeof(PassiveAbility)) {
					if (pa.type == t.requierement[0]) {
						Debug.Log("Abilité trouvé Tâche possible ");
						return true;
					}
				}
			}
			Debug.Log("Pas l'abilité nécessaire Tâche possible ");

			return false;
		}
		Debug.Log("Pas de prérequis");

		return true;
	}
    /*
	public void addTask(GameObject a , GameObject b, TaskEnum t){
		tasks.Add (new Task (a, b,t));

	}
	public void addTask(GameObject b, TaskEnum t){
		tasks.Add (new Task (b, t));
	}*/
    public void addTask(Task t) {

		if (verifTask (t)) {
			tasks.Add (t);
			if (t.type == TaskEnum.CUT) {
				GameObject obj = Instantiate (Resources.Load ("UI/TaskIcon/CutMineIcon") as GameObject,
					                 new Vector3 (t.cibled.transform.position.x, 1.3f, t.cibled.transform.position.z),
					                 Quaternion.Euler (new Vector3 (90, 0, 0))) as GameObject;
				t.icon = obj;
			} else if (t.type == TaskEnum.TAKE) {
				GameObject obj = Instantiate (Resources.Load ("UI/TaskIcon/TakeIcon") as GameObject,
					                 new Vector3 (t.cibled.transform.position.x, 1.3f, t.cibled.transform.position.z),
					                 Quaternion.Euler (new Vector3 (90, 0, 0))) as GameObject;
				t.icon = obj;
			} else if (t.type == TaskEnum.COLLECTFOOD) {
				GameObject obj = Instantiate (Resources.Load ("UI/TaskIcon/CollectIcon") as GameObject,
					                 new Vector3 (t.cibled.transform.position.x, 1.3f, t.cibled.transform.position.z),
					                 Quaternion.Euler (new Vector3 (90, 0, 0))) as GameObject;
				t.icon = obj;
			}

		}


	}

	public void removeTask(GameObject gbj){
		List<int> listIndex = new List<int> ();
		for(int i = 0 ; i<tasks.Count;i++){
			if (tasks[i].cibled == gbj) {
				if (tasks[i].creature != null) {
					tasks[i].creature.GetComponent<WorkManager> ().cancelTask ();
				}
				listIndex.Add (i);
			}
		}
		int[] tabIndex = listIndex.OrderByDescending (v => v).ToArray ();

		for (int i = 0; i < tabIndex.Length; i++) {
			//Debug.Log ("index = " + tabIndex [i]);
			removeTask (tabIndex[i]);

		}
	}

	public void removeTask(Task t){
		if (t.icon != null) {
			Destroy(t.icon);

		}

		tasks.Remove (t);
	}

	public void removeTask(int i){

		Destroy (tasks [i].icon);
		tasks.Remove (tasks [i]);

	}

	//Pas de double tâche sur un élément.
	bool verifTask(Task t){
		
		foreach(Task task in tasks){
			if (task.cibled == t.cibled) {
				Debug.Log ("Supprimer deja une tache sur " + t.cibled);
				return false;
			}

		}
		if (!t.cibled.GetComponent<ResourceManager> ().regrow && t.type == TaskEnum.COLLECTFOOD) {
			return false;
		}
		if (!t.cibled.GetComponent<ResourceManager> ().spoilage && t.type == TaskEnum.TAKE) {
			return false;
		}
		if (t.cibled.GetComponent<ResourceManager> ().spoilage &&( t.type == TaskEnum.CUT ||t.type == TaskEnum.COLLECTFOOD)) {
			return false;
		}
		return true;
	}
}



