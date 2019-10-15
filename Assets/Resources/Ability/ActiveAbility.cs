using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbility : Ability {

	GameObject effect;

	public ActiveAbility(AbilityType t, int level , GameObject eff){
		type = t;
		lvl = level;
		effect = effect;
	}
}
