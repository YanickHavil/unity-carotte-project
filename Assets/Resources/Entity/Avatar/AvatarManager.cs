using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : MonoBehaviour {

	public GameObject sprite;
	GameObject spriteManager;
	InputManager inputM;
	Attributes att;


	UnityEngine.AI.NavMeshAgent agent;
	Transform spritema;
	// Use this for initialization
	void Start () {
		inputM = GameObject.Find ("Player").GetComponent<InputManager>();
		inputM.avatar = gameObject;
		inputM.avatarMode ();
		Debug.Log ("MODE AVATAR");
		att = GetComponent<Attributes> ();


		spriteManager = Instantiate(sprite , Vector3.zero, Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
		spriteManager.transform.parent = gameObject.transform;
		spriteManager.transform.localRotation = Quaternion.Euler(new Vector3 (90, 0, 0));
		spriteManager.transform.localPosition = new Vector3 (0, 0, 0);//Vector3.zero;
		spriteManager.transform.localScale = new Vector3(1,1,1);


		spritema = transform.GetChild (1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void direction(int i){
		if (i < 0) {
			spritema.localRotation = Quaternion.Euler (new Vector3 (270, 180, 0));

		} else{
			spritema.localRotation = Quaternion.Euler(new Vector3(90,0,0));

		}
	}

}
