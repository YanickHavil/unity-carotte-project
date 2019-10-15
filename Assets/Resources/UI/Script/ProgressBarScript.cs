using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarScript : MonoBehaviour {


	public Sprite empty;
	public Sprite quart;
	public Sprite half;
	public Sprite quartAndHalf;
	public Sprite full;


	SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void init(){
		sprite = GetComponent<SpriteRenderer> ();

	}

	public void refresh(int n){
		if (sprite == null) {
			Debug.Log ("PROBLEMM");
		}
		if (n < 20) {
			sprite.sprite = empty;
		} else if (n < 40) {
			sprite.sprite = quart;
		} else if (n < 60) {
			sprite.sprite = half;
		} else if (n < 80) {
			sprite.sprite = quartAndHalf;
		} else {
			sprite.sprite = full;
		}
	}
}
