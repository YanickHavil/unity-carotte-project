using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AmbiantMusic : MonoBehaviour {
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		AudioClip audio = Resources.Load("Music/theme1") as AudioClip;
		audioSource.clip = audio;
		audioSource.Play ();
		audioSource.loop = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
