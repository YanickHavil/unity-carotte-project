using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class ProceduralSound : MonoBehaviour {

	List<AudioClip> audios;
	AudioSource audioSource;
	bool sound = true;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		audios = new List<AudioClip> ();

		audios.Add (Resources.Load("Music/Ab") as AudioClip);
		audios.Add (Resources.Load("Music/AbMaj7") as AudioClip);
		audios.Add (Resources.Load("Music/AbMaj7-Short") as AudioClip);
		audios.Add (Resources.Load("Music/Bb") as AudioClip);
		audios.Add (Resources.Load("Music/Bb-short") as AudioClip);
		audios.Add (Resources.Load("Music/Cm") as AudioClip);
		audios.Add (Resources.Load("Music/Cm-Short") as AudioClip);
		audios.Add (Resources.Load("Music/Eb") as AudioClip);
		audios.Add (Resources.Load("Music/Eb-Short") as AudioClip);
		audios.Add (Resources.Load("Music/EbMaj7") as AudioClip);
		audios.Add (Resources.Load("Music/EbMaj7-Short") as AudioClip);
		audios.Add (Resources.Load("Music/Fm") as AudioClip);
		audios.Add (Resources.Load("Music/Fm-Short") as AudioClip);
		audios.Add (Resources.Load("Music/Gm") as AudioClip);
		audios.Add (Resources.Load("Music/Gm-Short") as AudioClip);

		StartCoroutine (clipManager());

		
	}


	IEnumerator clipManager(){
		while (sound) {
			int rand = Random.Range (0, 15);
			audioSource.clip = audios [rand];
			audioSource.Play ();
			yield return new WaitForSeconds(audioSource.clip.length-3);

		}
	}
	/*
	void clipManager(){
		Debug.Log ("change music");
		int rand = Random.Range (0, 16);
		audioSource.clip = audios [rand];
		audioSource.Play ();
	}
	*/
}
