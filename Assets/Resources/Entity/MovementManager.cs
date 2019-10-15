using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MovementManager : MonoBehaviour {

	UnityEngine.AI.NavMeshAgent agent;
	bool tokenMove = true;
	Attributes att;
	ArrayList openList = new ArrayList();
	ArrayList closedList = new ArrayList();
	public Transform spritema;

	// Use this for initialization
	void Start () {
	

		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		att = GetComponent<Attributes> ();
		spritema = transform.GetChild (0);
		InvokeRepeating ("checkDirection", 0.1f, 0.1f);
		InvokeRepeating ("idleManager", 0.5f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {

		if (arrivedToDestination () ) {
			tokenMove = true;
		}
	}


	void idleManager(){
		if (att.isIdle () && tokenMove) {
			int rdn = Random.Range (0, 10);
			if (rdn == 0) {
				randomMove (gameObject.transform.position.x, gameObject.transform.position.z);
			}
		}
	}

	public void ForceRandomMove()
	{

		resumeMovement();
		float posX = Random.Range (gameObject.transform.position.x - 5.0f, gameObject.transform.position.x + 5.0f);
		float posY = Random.Range (gameObject.transform.position.z - 5.0f, gameObject.transform.position.z + 5.0f);
		tokenMove = false;
		
		moveTo(posX,posY);
			
			
		
	}
	void randomMove(float x, float y){
		if (tokenMove) {
			resumeMovement();
			float posX = Random.Range (x - 1.5f, x + 1.5f);
			float posY = Random.Range (y - 1.5f, y + 1.5f);
			tokenMove = false;
			
			moveTo(posX,posY);
			
			
		}
		
	}

	
	public bool arrivedToDestination(){

		float dist=agent.remainingDistance;
		
		if( dist <= 0.1f )
		{
			
			stopEntity();
			return true;
		} else {
			
			return false;
		}
		
		
	}

	public bool isHere(Vector3 v3){
		float dist;
		if (agent.pathPending) {
			dist = Vector3.Distance(transform.position,v3);
		}
		else{
			dist=agent.remainingDistance;

		}
		if (dist<=0.5f) {
			stopEntity ();
			return true;
		} else {
			return false;
		}
	}
	public void randomMoveNearTo(float x,float y){
		if (tokenMove) {
			float posX = Random.Range (x - 1.5f, x + 1.5f);
			float posY = Random.Range (y - 1.5f, y + 1.5f);
			tokenMove = false;
			
			moveTo (posX, posY);
			
			
		}
	}

	public void randomMoveNearTo(float x, float y, int range){
		if (tokenMove) {
			float posX = Random.Range (x - range, x + range);
			float posY = Random.Range (y - range, y + range);
			tokenMove = false;
			
			moveTo (posX, posY);
			
			
		}
	}

	void pathfinding(){
		openList = new ArrayList ();
		closedList = new ArrayList ();

		Tile original = att.getCurrentTile ();
		openList.Add (original);
		do {
			Tile currentTile ;




		} while(openList.Count >0);

	}
	void checkDirection(){
		if (agent.destination != null) {
			/*
			if (agent.destination.x < gameObject.transform.position.x) {
				spritema.localRotation = Quaternion.Euler (new Vector3 (270, 180, 0));

			} else if (agent.destination.x >gameObject.transform.position.x){
				spritema.localRotation = Quaternion.Euler(new Vector3(90,0,0));

			}
		}*/
			if (agent.destination != null) {
				if (agent.desiredVelocity.x < 0) {
					spritema.localRotation = Quaternion.Euler (new Vector3 (270, 180, 0));

				} else if (agent.desiredVelocity.x >0){
					spritema.localRotation = Quaternion.Euler(new Vector3(90,0,0));

				}
			}
		}

	}

	public void stopEntity(){
		agent.Stop ();
		att.setMove(false);
		
	}

	public void resumeMovement(){
		if (!tokenMove) {
			tokenMove = true;
		}
		agent.Resume ();
	}

	public void moveTo(float x , float y){
		resumeMovement();
		agent.destination = new Vector3 (x, 0, y);
		att.setMove (true);
	}

	public void moveTo(Vector3 v){
		resumeMovement();
		agent.destination = v;
		att.setMove (true);

	}
	//pour debug
	public Vector3 getDestination(){
		return agent.destination;
	}

}
