using UnityEngine;
using System.Collections;

public class NestManager : MonoBehaviour {

    public GameObject entity;
	public GameObject nest;
    ArrayList entitylist = new ArrayList();

    int compteur = 0;
    // Use this for initialization
    void Start () {
        InvokeRepeating("Spawning", 1f, 1f);

    }

    // Update is called once per frame
    void Update () {
	
	}

    void Spawning()
    {
        int nbentity = entitylist.Count;

        switch (nbentity)
        {
            case 0:
                spawn();
                break;
            case 1:
                compteur++;
                if(compteur == 600)
                {
                    compteur = 0;
                    spawn();
                }
                break;
            case 2:
                compteur++;
                if (compteur == 1200)
                {
                    compteur = 0;
                    spawn();
                }
                break;
            case 3:
                compteur++;
                if (compteur == 1800)
                {
                    compteur = 0;
                    spawn();
                }
                break;

        }
    }


    void spawn()
    {

       GameObject entityTemp = Instantiate(entity, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        entitylist.Add(entityTemp);
        entityTemp.GetComponent<Attributes>().nest = gameObject;
    }
}
