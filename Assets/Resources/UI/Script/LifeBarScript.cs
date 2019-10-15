using UnityEngine;
using System.Collections;

public class LifeBarScript : MonoBehaviour {

	public Attributes att;
	float scale = 0.05f;
	Renderer child;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		updateLife (); 
		
	}

	
	public void init(){
		att = transform.parent.GetComponent<Attributes> ();
		child = gameObject.GetComponent<Renderer> ();
		child.transform.localRotation = Quaternion.Euler(new Vector3(90.0f,0.0f,0.0f));
		child.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.1f);
		//child.transform.localScale = new Vector3 (0.1f, 0.001f, 0.01f);

		child.transform.localScale = new Vector3(0.115f,1.0f,0.015f);
		child.material = Resources.Load ("UI/Green") as Material;
		scale = child.transform.localScale.x;
		gameObject.layer = 5;

	}

	public void updateLife(){
		float originalValue = child.bounds.min.x;

		float size = ((float)att.life / (float)att.maxLife);
		child.transform.localScale = new Vector3 (scale * size ,1,child.transform.localScale.z);
		float newValue = child.bounds.min.x;

		float difference = newValue - originalValue;
		transform.Translate(new Vector3(-difference, 0f, 0f));
		if (att.life <= att.maxLife / 4) {
			child.material = Resources.Load ("UI/Red") as Material;

		} else if (att.life <= att.maxLife / 2) {
			child.material = Resources.Load ("UI/Orange") as Material;
		} else {
			child.material = Resources.Load ("UI/Green") as Material;
			
		}
	}

}
