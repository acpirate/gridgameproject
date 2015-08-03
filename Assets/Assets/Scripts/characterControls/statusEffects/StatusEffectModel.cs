using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusEffectModel : MonoBehaviour {

	public string    	 effectName;
	public string		 description;
	public int			 duration;
	public List<string>  timing;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void applyEffect (CharacterModel target, string timing) {
		print (name + " has yet to be setup");
	}

}
