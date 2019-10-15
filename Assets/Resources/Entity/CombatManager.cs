using UnityEngine;
using System.Collections;

public class CombatManager : MonoBehaviour {


	public BasicAttack basicatk;
	Creature crea;
	Attributes att;
	public bool tokenUse = true;
	bool enemyOnRange = false;
	AnimationManager animMana;
	MovementManager mvt;
	AudioSource audioS;
	int range;
	public float durationbleed = 0;

	// Use this for initialization
	void Start () {

		att = GetComponent<Attributes> ();
		animMana = GetComponent<AnimationManager> ();
		mvt = GetComponent<MovementManager> ();
		audioS = Camera.main.GetComponent<AudioSource> ();
		crea = GetComponent<Creature> ();
		InvokeRepeating ("cooldown", 1, 1);
		InvokeRepeating ("attackManager", 0.1f, 0.1f);
		//basicatk = new BasicAttack ();

	}
	
	// Update is called once per frame
	void Update () {
	

	}

	void attackManager(){
		if (crea.enemy != null) {
			att.setFight(true);
		} else {
			att.setFight(false);
		}
		if (att.isFighting() && crea.enemy != null) {
			isOnRange ();
			
			if(enemyOnRange){
				if (basicatk.ready) {
					if(tokenUse){
						tokenUse = false;
						StartCoroutine (useAttack (basicatk));
					}
					
				}
				else{
					if(tokenUse){
						//mvt.randomMoveNearTo(crea.enemy.transform.position.x,crea.enemy.transform.position.z);
						
					}
				}
				
				
			}
			else {
				if(!basicatk.ranged){
					mvt.moveTo(crea.enemy.transform.position.x,crea.enemy.transform.position.z);
					
				}
				else{
					mvt.randomMoveNearTo(crea.enemy.transform.position.x,crea.enemy.transform.position.z,basicatk.range);
					
				}
				
			}
			
		}
		if (crea.enemy == null) {
			enemyOnRange = false;
		}


		if (att.life != att.maxLife && GetComponentInChildren<Renderer>().material.color != Color.white) {
			if(durationbleed >=1){
				durationbleed = 0;
			}
			else{
				durationbleed +=0.1f;
			}
			GetComponentInChildren<Renderer>().material.color = Color.Lerp(GetComponentInChildren<Renderer>().material.color,Color.white,durationbleed);

		}
	}

	void cooldown(){
		
		if (basicatk != null) {
			if (basicatk.secondCooldown >= basicatk.maxCooldown) {
				basicatk.secondCooldown = basicatk.maxCooldown;
				basicatk.ready = true;
			} else {
				basicatk.secondCooldown += 1;
			}
		}
		
		
		
	}

	void isOnRange(){
		if (basicatk.ranged) {
			if(Mathf.Abs(crea.enemy.transform.position.x - transform.position.x )<= basicatk.range){
				if(Mathf.Abs(crea.enemy.transform.position.z - transform.position.z )<= basicatk.range){
					//Debug.Log("onrange");
					enemyOnRange = true;
				}
				
			}
		}
	}

	IEnumerator useAttack(BasicAttack atk){
		if (enemyOnRange) {
			if(att.isMoving()){
				mvt.stopEntity();
				
			}
			animMana.animationFight = true;
			yield return new WaitForSeconds (0.5f);
			if(audioS != null){
				audioS.PlayOneShot (Resources.Load ("Sound/Attack/melee sound") as AudioClip);
				
			}
			
			yield return new WaitForSeconds (0.5f);
			
			if (!basicatk.ranged) {
				if (crea.enemy != null) {
					//if (enemyOnRange) {
					
					atk.secondCooldown = 0;
					atk.ready = false;
					int damage = Random.Range (atk.minDamage, atk.maxDamage + 1);
					crea.enemy.GetComponent<Attributes> ().receivedDamage (damage, gameObject);
					tokenUse = true;
					Debug.Log ("Ouch " + damage);
					//} else {
					//tokenUse = true;
					
					//Debug.Log ("Rater");
					//}
					
				}
				animMana.animationFight = false;
				
				
			} /*else {

				if (crea.enemy != null) {
					atk.secondCooldown = 0;
					atk.ready = false;
					int damage = Random.Range (atk.minDamage, atk.maxDamage + 1);
					GameObject missile = Instantiate (Resources.Load ("Missile/" + atk.missileName),
					                                  new Vector3 (transform.position.x +0.5f, 0, transform.position.z+0.5f),
					                                  Quaternion.Euler(new Vector3(0,30,0))) as GameObject;
					missile.GetComponent<Missile> ().launcher = gameObject;
					missile.GetComponent<Missile> ().cibled = stats.getEnemy ();
					missile.GetComponent<Missile> ().dmg = damage;
					
					tokenUse = true;
					Debug.Log ("Tornado " + damage);
					
					
					
				} else {
					tokenUse = true;
				}
				
				animMana.animationFight = false;
				
			}*/
			
			
			
			
			mvt.resumeMovement();
			
			
		} else {
			tokenUse = true;
		}
	}

	void OnTriggerEnter(Collider other){
		if (att != null) {
			if (crea.enemy != null && att.isFighting() == true) {
				
				
				if(other.gameObject == crea.enemy && basicatk.ranged == false){
					mvt.stopEntity();
					
					enemyOnRange = true;
					//Debug.Log("touch eneme");
				}
			}
		}

		
	}
	void OnTriggerStay(Collider other){
		if (att != null) {
			if (crea.enemy != null && att.isFighting() == true) {
				
				
				if(other.gameObject == crea.enemy && basicatk.ranged == false){
					mvt.stopEntity();
					
					enemyOnRange = true;
					//Debug.Log("touch eneme");
				}
			}
		}
		
	}
	void OnTriggerExit(Collider other){
		if (crea.enemy != null && att.isFighting() == true) {
			if(other.gameObject == crea.enemy && basicatk.ranged == false){
				enemyOnRange = false;
			}
		}
		
	}
}
