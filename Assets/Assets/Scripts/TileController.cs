using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TileController : MonoBehaviour {

	GameController gameController;

	public int x;
	public int y;
	public int speed;
	public int attack;
	public bool revealed=false;
	public bool peek=false;

	Text myText;

	void Awake()
	{
		gameController=GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		myText = GetComponentInChildren<Text>();

		speed=Random.Range(0,16);
		attack=15-speed;

		SetValueDisplay();

	}

	public void SetCoords(int inX, int inY)
	{
		x = inX;
		y = inY;
	}

	public void TileClick()
	{
		//Debug.Log("clicked on tile " + x.ToString() + " " + y.ToString());
		gameController.TryMove(x,y);
	}

	public string TileValueString()
	{
		string tempString="?";

		if (revealed || peek)
			tempString=speed.ToString()+","+attack.ToString();

		return tempString;
	}

	public void SetValueDisplay()
	{
		myText.text=TileValueString();
	}

}
