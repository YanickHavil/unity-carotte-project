using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceManager : MonoBehaviour {

    public float influence = 0;
    public float influenceMax = 100;

    public float influenceDecreased = 0.1f; // toutes les secondes l'influence est réduite de ce montant

    public GameObject influenceBar;
    Attributes att;
	public float difficulty= 1;

    public float factorSun = 1;
    public float factorRain = 1;

	// Use this for initialization
	void Start () {
		att = gameObject.GetComponent<Attributes> ();



        influenceBar = Instantiate(Resources.Load("UI/InfluenceBar"), Vector3.zero, Quaternion.Euler(new Vector3(-90f, -90f, 0))) as GameObject;
        influenceBar.transform.parent = transform;
        influenceBar.transform.localPosition = new Vector3(0.3f, 0, 0f);
        influenceBar.transform.localRotation = Quaternion.Euler(new Vector3(-90.0f, -90.0f, 0.0f));
        influenceBar.transform.localScale = new Vector3(0.3f, 0.1f, 0.5f);
        influenceBar.GetComponentInChildren<InfluenceBar>().init(GetComponent<InfluenceManager>());

        influenceBar.SetActive(false);
        InvokeRepeating("influenceManager", 1, 1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void influenceManager()
    {
        if (influence <= 0)
        {
            influenceBar.SetActive(false);
            influence = 0;
        }
        else
        {
            influence -= influenceDecreased;
        }
    }

	public void tryInfluence(InfluenceType type){


        float baseTestInfluence = 10f; // chiffre de base d'augmentation a revoir

		int random = Random.Range (0, 101);

		float luckRate = 1;

		//Echec critique
		if (random < 5) {
			luckRate = -1;
		}
		//Succes critique
		else if (random > 95) {

			luckRate = 2;
		}

        switch (type)
        {
            case InfluenceType.RAIN:
                influence += baseTestInfluence * luckRate * factorRain;
                break;
            case InfluenceType.SUN:
                influence += baseTestInfluence * luckRate * factorSun;
                break;
            default:
                break;
        }
        if(influence >0)
        {
            influenceBar.SetActive(true);
        }

		if(influence >= influenceMax && att.numPlayer == -1){
			att.numPlayer = 1;
			Debug.Log ("Une créature est maintenant sous votre influence !  : " + att.name);
            influence = 0;
            influenceBar.SetActive(false);
            //Mettre en place le système de possession

            GameObject.Find("Player").GetComponent<Player>().addCreature(gameObject);
        }
	}
}
