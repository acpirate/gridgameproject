using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum FIGURETYPE { PLAYER, ENEMY }

public class FigureController : MonoBehaviour {

	public FIGURETYPE myType;
	public string figureName;
	public int x;
	public int y;
	public Color myColor;

	public int health=50;
	public int attack=10;
	public int speed=10;
	public int mana=20;


	// Use this for initialization
	void Start () {
		myColor=GetComponent<Image>().color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetCoords(int inX, int inY)
	{
		x=inX;
		y=inY;
	}

	public string StatString()
	{
		string outString="";
		outString+="Health: "+health.ToString()+"\n";
		outString+="Attack: "+attack.ToString()+"\n";
		outString+="Speed: "+speed.ToString()+"\n";
		outString+="Mana: "+mana.ToString()+"\n";

		return outString;
	}
}
