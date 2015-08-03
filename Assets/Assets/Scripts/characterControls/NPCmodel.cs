using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCmodel : CharacterModel {

	public List<string> allies;
	public List<string> enemies;
	public object       itemDrop;
	public int          experienceDrop = 0;

	public NPCmodel (string iCharacterName, List<string> iAllies,  List<string> iEnemies,
					   int iMaxHealth = 1, int iMaxMana = 0, int iStrength = 0, int iEndurance = 0,
	                   int iSpeed = 0, int iMagic = 0) 
	{
		DontDestroyOnLoad (gameObject);
		characterName = iCharacterName;
		characterType.Add("NPC");

		maxHealth = iMaxHealth;
		currentHealth = iMaxHealth;
		maxEnergy = iMaxMana;
		currentEnergy = iMaxMana;
		strength = iStrength;
		endurance = iEndurance;
		speed = iSpeed;
		technique = iMagic;

		if (iAllies == null){
			allies.Add("NPC");
		} else {
			allies = iAllies;
		}

		if(iEnemies == null){
			enemies.Add("Player");
		} else {
			enemies = iEnemies;
		}
	}

	public void destroyNPC() {
		Destroy (gameObject);
	}
}