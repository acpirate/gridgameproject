using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerModel : CharacterModel {
	
	public static PlayerModel player1;
	public static PlayerModel player2;

	public static int numberOfPlayers = 1;
	
	public string heroClass;
	public int    totalXP;
	public int    remainingXP;
	
	// Use this for initialization
	void Awake () {
		if (player1 == null) {
			DontDestroyOnLoad (gameObject);
			player1 = this;
		} else if ((numberOfPlayers == 2) && (player2 == null)) {
			DontDestroyOnLoad (gameObject);
			player2 = this;
		} else if ((player1 != this) || (player2 != this && numberOfPlayers == 2)) {
			Destroy (gameObject);
		}
	}
	
	
	public void Save (string fileName) {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/" + fileName + ".dat");
		
		var saveData = new playerData ();
		saveData.heroClass = heroClass;
		saveData.totalXP = totalXP;
		saveData.remainingXP = remainingXP;
		saveData.name = name;
		saveData.maxHealth = maxHealth;
		saveData.currentHealth = currentHealth;
		saveData.maxEnergy = maxEnergy;
		saveData.currentEnergy = currentEnergy;
		saveData.strength = strength;
		saveData.endurance = endurance;
		saveData.speed = speed;
		saveData.technique = technique;
		saveData.specialAbilities = specialAbilities;
		
		bf.Serialize (file, saveData);
		file.Close ();
	}
	
	public void Load (string fileName) {
		if(File.Exists(Application.persistentDataPath + "/" + fileName + ".dat")) {
			
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/" + fileName + ".dat", FileMode.Open);
			playerData loadData = (playerData)bf.Deserialize (file);
			file.Close ();
			
			heroClass = loadData.heroClass;
			totalXP = loadData.totalXP;
			remainingXP = loadData.remainingXP;
			name = loadData.name;
			maxHealth = loadData.maxHealth;
			currentHealth = loadData.currentHealth;
			maxEnergy = loadData.maxEnergy;
			currentEnergy = loadData.currentEnergy;
			strength = loadData.strength;
			endurance = loadData.endurance;
			speed = loadData.speed;
			technique = loadData.technique;
			specialAbilities = (List<object>)loadData.specialAbilities;
		}
	}
}

[Serializable]
class playerData {
	
	//local variables (PlayerController)
	public string heroClass;
	public int    totalXP;
	public int    remainingXP;
	
	// Super class variables (CharacterController)
	public string name;
	public int    maxHealth;
	public int    currentHealth;
	public int    maxEnergy;
	public int    currentEnergy;
	public int    strength;
	public int	  endurance;
	public int    speed;
	public int    technique;
	public List<object> specialAbilities;
}