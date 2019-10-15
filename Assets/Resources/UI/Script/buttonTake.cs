using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class buttonTake : MonoBehaviour {


	Player player;
	Button button;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<Player>();
		button =  GetComponent<Button>();
		button.onClick.AddListener(delegate {Click();});
	}

	// Update is called once per frame
	void Update () {

	}

	void Click(){
		player.GetComponent<SelectionManager>().setSelectionTile (TaskEnum.TAKE);

	}
}
