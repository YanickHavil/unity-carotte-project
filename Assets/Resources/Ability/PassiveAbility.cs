using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveAbility : Ability{

	public PassiveAbility(AbilityType t, int level){
		type = t;
		lvl = level;
	}
}
