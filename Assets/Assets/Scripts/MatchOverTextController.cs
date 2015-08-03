using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MatchOverTextController : MonoBehaviour {

	Text myText;

	void Awake()
	{
		myText=GetComponent<Text>();
	}

	// Use this for initialization
	void Start () {
		myText.text=GameController.winner+" WINS!";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
