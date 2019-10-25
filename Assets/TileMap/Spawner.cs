using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Spawner : MonoBehaviour {

	TileMap map;
	int cmpt = 0;
	// Use this for initialization
	void Start () {
		map = GameObject.Find ("TileMap").GetComponent<TileMap>();
		InvokeRepeating ("spawnerManager", 1, 1);
	}
	
	void spawnerManager(){
		//Debug.Log (cmpt);
		cmpt++;
		if(cmpt == 5){
			bool good = false;
			while(!good){
				//Debug.Log("Cherche tile");
				int rand = Random.Range (2, map.size_x - 4);
				int rand2 = Random.Range (2, map.size_z - 4);
				Tile t =map.getMap().GetTileAt(rand,rand2);
				if(t.staticEntity ==null){

                    int randtailletroupeau = Random.Range(2, 5);
                    Debug.Log("troupeau Domo de " + randtailletroupeau + "   en position x = " + rand + " et y = " + rand2);
                    //Creation de troupeau
                    List<GameObject> listTempHerd = new List<GameObject>();
                    GameObject Gbjtemp = null;
                    for (int j = 1; j <= randtailletroupeau; j++)
                    {

                        Gbjtemp = Instantiate(Resources.Load("Entity/Domo/Domo", typeof(GameObject)),
                              new Vector3(rand, 1.2f, rand2),
                              Quaternion.Euler(new Vector3(90, 0, 0))) as GameObject;

                        listTempHerd.Add(Gbjtemp);
                        Gbjtemp.GetComponent<Attributes>().herdList = listTempHerd;

                    }

                    Debug.Log("valeur liste =  " + listTempHerd.Count);
                    Debug.Log("valeur liste entity =  " + Gbjtemp.GetComponent<Attributes>().herdList[0]);
                    GameObject notif = GameObject.Find("Notification");
					notif.GetComponent<Text>().text = "Apparition d'un troupeau de Domo";
					good=true;
					//Debug.Log("Invocation ! ");

				}
			}

		}
	}
}
