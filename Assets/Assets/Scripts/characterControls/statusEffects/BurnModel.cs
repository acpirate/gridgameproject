using UnityEngine;
using System.Collections;

public class BurnModel : StatusEffectModel
{

	public int    burnStrength;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void applyEffect (CharacterModel target, string currentTime)
	{
		foreach (var effectTiming in timing) {
			if (currentTime == effectTiming) {
				target.loseHealth (burnStrength);
				duration -= 1;
				if (duration <= 0) {
					Destroy (gameObject);
				}
				break;
			}
		}
	}
}
