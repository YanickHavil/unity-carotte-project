using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {
	
	
	public string[] animations; 
	// 0 : Idle, 
	//1 : Move, 
	//2 : Attack
	
	Attributes att;
	public bool animationFight = false;
	// Use this for initialization
	void Start () {
		
		att = GetComponent<Attributes> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (animations.Length > 1) {
			if(!animationFight){
				if (att.isMoving()) {
					GetComponentInChildren<Animator> ().CrossFade (animations[1], 0);
					
				} else {
					
					GetComponentInChildren<Animator> ().CrossFade (animations[0], 0);
					
				}
			}
			else{
				if (animations.Length >= 3) {
					GetComponentInChildren<Animator> ().CrossFade (animations[2], 0);
					
				}
			}
			
			
		}
		
	}
	
	public void changeAnimationFight(){
		animationFight = false;
		if (animations.Length >= 3) {
			GetComponentInChildren<Animator> ().CrossFade (animations[2], 0);
			
		}
		
	}
}
