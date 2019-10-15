using UnityEngine;
using System.Collections;

public class BasicAttack {

	public int name;
	public int minDamage, maxDamage;
	//public EnumTypeAttack type;
	public bool ranged;
	public int range = 0;
	public int secondCooldown;
	public int maxCooldown;
	public bool ready = true;
	public string missileName;


	public BasicAttack(){
		minDamage = 1;
		maxDamage = 1;
		//type = EnumTypeAttack.NORMAL;
		ranged = false;
		maxCooldown = 5;
		secondCooldown = maxCooldown;
		
		
	}

	public BasicAttack(int minD, int maxD, bool range, int cooldown)
	{
		minDamage = minD;
		maxDamage = maxD;
		ranged = range;
		maxCooldown = cooldown;
		secondCooldown = maxCooldown;

	}

	public string showInfoAttack(){
		string str = "";
		//str += "Attack Tornado " ;
		str += "Damage :   "+ minDamage + " - " + maxDamage + "\n";
		if (ranged) {
			str += "Range : yes" + "\n";
		} else {
			str += "Range : no" + "\n";
			
		}
		str += "Cooldown :   " + maxCooldown +"s \n";
		return str;
	}
}
