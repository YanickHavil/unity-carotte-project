using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceBar : MonoBehaviour {


    InfluenceManager influM;
    float scale = 0.05f;
    Renderer child;

    public void init(InfluenceManager inf)
    {

        influM = inf;
        child = gameObject.GetComponent<Renderer>();
        child.transform.localRotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
        child.transform.localPosition = new Vector3(0.0f, 0.0f, 0.1f);
        child.transform.localScale = new Vector3 (0.1f, 0.001f, 0.01f);

        child.transform.localScale = new Vector3(0.115f, 1.0f, 0.015f);
        child.material = Resources.Load("UI/Purple") as Material;
        scale = child.transform.localScale.x;
        gameObject.layer = 5;

    }
    void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
        updateInfluence();
	}


    public void updateInfluence()
    {
        float originalValue = child.bounds.min.x;

        float size =  (influM.influence / influM.influenceMax);
        child.transform.localScale = new Vector3(scale * size, 1, child.transform.localScale.z);
        float newValue = child.bounds.min.x;

        float difference = (0.580f*size)-0.580f; //Taille si influence = 0 par rapport au conteneur de la barre (-0.580f)
        child.transform.localPosition = new Vector3(difference, 0f, 2f);
        /*
        float size = (influM.influence / influM.influenceMax);
        child.transform.localScale = new Vector3(1, scale * size, child.transform.localScale.z);
        float newValue = (50 * size) - 50;

        transform.localPosition = new Vector3(-2f, newValue, 0f);*/

    }
}
