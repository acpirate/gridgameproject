using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterModel : MonoBehaviour {

	//The maximum and minimum values for a stat
	public int 			statMax = 10;
	public int			statMin = -5;

	// character base stats
	public string       characterName;
	public List<string> characterType;
	public int          maxHealth;
	public int          maxEnergy;
	public int          strength;
	public int			endurance;
	public int          speed;
	public int          technique;


	public List<object> specialAbilities;

	//characters current stats
	public int          currentHealth;
	public int          currentMaxHealth;
	public int          currentEnergy;
	public int          currentMaxEnergy;
	public int          currentStrength;
	public int			currentEndurance;
	public int          currentSpeed;
	public int          currentTechnique;

	// game info
	public int xBoardPosition = 0;
	public int yBoardPosition = 0;

	// Visual interface
	public object sprite;
	public object face;
	public Color  baseColor;
	
	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		currentMaxHealth = maxHealth;
		currentEnergy = maxEnergy;
		currentMaxEnergy = maxEnergy;
		currentStrength = strength;
		currentEndurance = endurance;
		currentSpeed = speed;
		currentTechnique = technique;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void heal (int amount) {
		currentHealth += amount;
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
	}

	public void healMax () {
		currentHealth = maxHealth;
	}

	public void loseHealth (int amount) {
		currentHealth -= amount;
		if (currentHealth < 0) {
			currentHealth = 0;
		}
	}

	public void die () {
		//any death actions
		Destroy (gameObject);
	}

	public void changeMaxHealth (int amount) {
		maxHealth += amount;
		if (maxHealth < 0) {
			maxHealth = 0;
		}
	}

	public void recoverEnergy (int amount) {
		currentEnergy += amount;
		if (currentEnergy > maxEnergy) {
			currentEnergy = maxEnergy;
		}
	}

	public void loseEnergy (int amount) {
		currentEnergy -= amount;
		if (currentEnergy < 0) {
			currentEnergy = 0;
		}
	}

	public void changeMaxEnergy (int amount) {
		maxEnergy += amount;
		if (maxEnergy < 0) {
			maxEnergy = 0;
		}
	}

	public void changeStrength (int amount) {
		currentStrength += amount;
		if (currentStrength < statMin) {
			currentStrength = statMin;
		} else if (currentStrength > statMax) {
			currentStrength = statMax;
		}
	}

	public void changeEndurance (int amount) {
		currentEndurance += amount;
		if (currentEndurance < statMin) {
			currentEndurance = statMin;
		} else if (currentEndurance > statMax) {
			currentEndurance = statMax;
		}
	}

	public void changeSpeed (int amount) {
		currentSpeed += amount;
		if (currentSpeed < statMin) {
			currentSpeed = statMin;
		} else if (currentSpeed > statMax) {
			currentSpeed = statMax;
		}
	}

	public void changeTechnique (int amount) {
		currentTechnique += amount;
		if (currentTechnique < statMin) {
			currentTechnique = statMin;
		} else if (currentTechnique > statMax) {
			currentTechnique = statMax;
		}
	}
}
