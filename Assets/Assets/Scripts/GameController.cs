using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum GAMESTATE { PLAYING, INFODISPLAY};

public class GameController : MonoBehaviour {

	public static string winner;


	public BoardController boardController;
	public GameObject playerPrefab;
	public GameObject enemyPrefab;
	public Sprite sharedSpaceSprite;

	public Text playerStatDisplay;
	public Text enemyStatDisplay;

	public Text playerAttackInfo;
	public Text enemyAttackInfo;
	public Text winnerText;
	public Text outcomeReport;
	public GameObject combatResolutionDisplay;


	public Image playerPortait;
	public Image enemyPortrait;

	GAMESTATE gameState;
	GameObject playerFigure;
	GameObject enemyFigure;
	FigureController playerFigureController;
	FigureController enemyFigureController;

	// Use this for initialization
	void Start () {
		gameState=GAMESTATE.PLAYING;
		winner="";

		//initialize player figure;
		playerFigure=Instantiate(playerPrefab) as GameObject;
		playerFigureController = playerFigure.GetComponent<FigureController>();
		playerFigureController.myType=FIGURETYPE.PLAYER;
		playerFigureController.figureName="Player";
		//playerFigure.myDisplayObject=Instantiate(playerPrefab) as GameObject;
		boardController.PlaceFigure(playerFigure);
		boardController.PositionFigure(playerFigure,2,0);
		playerFigureController.SetCoords(2,0);
		boardController.PeekAtTilesInRange(2,0);
		playerStatDisplay.text=playerFigureController.StatString();
		playerPortait.color=playerFigureController.gameObject.GetComponent<Image>().color;


		//initialize enemy figure;
		enemyFigure=Instantiate(enemyPrefab) as GameObject;
		enemyFigureController=enemyFigure.GetComponent<FigureController>();
		enemyFigureController.myType=FIGURETYPE.ENEMY;
		enemyFigureController.figureName="Enemy";
		boardController.PlaceFigure(enemyFigure);
		boardController.PositionFigure(enemyFigure,2,4);
		enemyFigureController.SetCoords(2,4);
		enemyStatDisplay.text=enemyFigureController.StatString();
		enemyPortrait.color=enemyFigureController.gameObject.GetComponent<Image>().color;


		boardController.HighLightValidMoves(playerFigure);
	}
	
	// Update is called once per frame
	void Update () {
		if (gameState==GAMESTATE.INFODISPLAY) 
		{
			if (Input.GetMouseButtonDown(0))
			{
				gameState=GAMESTATE.PLAYING;
				combatResolutionDisplay.SetActive(false);
				CheckForGameOver();
			}
		}

	}


	public void TryMove (int xTarget, int yTarget) 
	{

		if (IsValidMove(playerFigureController.x,playerFigureController.y,xTarget,yTarget))
		{
			boardController.UnPeekTiles();

			playerFigureController.SetCoords(xTarget,yTarget);
			boardController.PositionFigure(playerFigure,xTarget,yTarget);
			boardController.HighLightValidMoves(playerFigure);
			EnemyMove();
			FigureDraw();
			boardController.PeekAtTilesInRange(playerFigureController.x,playerFigureController.y);
			boardController.RevealTileAt(enemyFigureController.x,enemyFigureController.y);
			boardController.RevealTileAt(playerFigureController.x,playerFigureController.y);
			ResolveCombat();
			//CheckForGameOver();
		}
	}

	
	void ResolveCombat()
	{	
		//activate the combat info display, can't click on board while the info display is active
		gameState=GAMESTATE.INFODISPLAY;
		combatResolutionDisplay.SetActive(true);

		//calculate combat stats
		FigureController fasterFigure=playerFigureController;
		FigureController slowerFigure=enemyFigureController;

		int tempPlayerTileSpeed = boardController.GetSpeedAt(playerFigureController.x, playerFigureController.y);
		int tempEnemyTileSpeed = boardController.GetSpeedAt(enemyFigureController.x, enemyFigureController.y);

		int totalPlayerSpeed = tempPlayerTileSpeed + playerFigureController.speed;
		int totalEnemySpeed = tempEnemyTileSpeed + enemyFigureController.speed;

		int tempPlayerTileAttack = boardController.GetAttackAt(playerFigureController.x, playerFigureController.y);
		int tempEnemyTileAttack = boardController.GetAttackAt(enemyFigureController.x, enemyFigureController.y);

		int totalPlayerAttack = tempPlayerTileAttack + playerFigureController.attack;
		int totalEnemyAttack = tempEnemyTileAttack + enemyFigureController.attack;


		//decide who wins
		if (totalEnemySpeed>totalPlayerSpeed) {
		//	Debug.Log("enemy is faster");
			fasterFigure=enemyFigureController;
			slowerFigure=playerFigureController;
		}

		//set display text
		playerAttackInfo.text=playerFigureController.figureName + ": Speed " + totalPlayerSpeed + " Attack "+ totalPlayerAttack;
		enemyAttackInfo.text=enemyFigureController.figureName + ":  Speed " +  totalEnemySpeed + " Attack " + totalEnemyAttack;

		string winnerName="";
		if (fasterFigure==playerFigureController) winnerName = playerFigureController.figureName;
		if (fasterFigure==enemyFigureController) winnerName = enemyFigureController.figureName;

		winnerText.text = winnerName + " is Faster!";

		//handle ties
		if (totalEnemySpeed==totalPlayerSpeed) 
		{
			winnerText.text = "Combatants are the same speed!";
			outcomeReport.text = "No damage is dealt!";
			return;
		}

		//calculate total damage, redundante, needs to be re-factored
		int tempTileAttack=boardController.GetAttackAt(fasterFigure.x, fasterFigure.y);
		int totalAttack=tempTileAttack+fasterFigure.attack;

		//damage figure
		slowerFigure.health-=totalAttack;

		//set stat displays
		playerStatDisplay.text=playerFigureController.StatString();
		enemyStatDisplay.text=enemyFigureController.StatString();

		//outcome text set
		outcomeReport.text = fasterFigure.figureName + " hits " + slowerFigure.figureName + " for " + totalAttack + " melee damage!\n" +
			slowerFigure.figureName + " has " + slowerFigure.health + " Health left.";

	}


	void CheckForGameOver()
	{
		if (playerFigureController.health<=0) winner="ENEMY";
		if (enemyFigureController.health<=0) winner="PLAYER";

		if (winner!="") Application.LoadLevel("matchover");

	}

	public bool IsValidMove(int startX, int startY, int endX, int endY)
	{
		bool validMove=false;
		int moveDistance=1;
		if (Mathf.Abs(startX-endX)+Mathf.Abs(startY-endY)<=moveDistance) validMove=true;

		return validMove;
	}

	public void FigureDraw()
	{
		Image playerImage=playerFigure.GetComponent<Image>();
		Image enemyImage=enemyFigure.GetComponent<Image>();

		if (playerFigureController.x == enemyFigureController.x && playerFigureController.y == enemyFigureController.y)
		{
			playerImage.color=new Color(1,1,1,.8f);
			enemyImage.color=Color.clear;
			playerImage.sprite=sharedSpaceSprite;
		}
		else
		{
			playerImage.sprite=null;
			playerImage.color=playerFigureController.myColor;
			enemyImage.color=enemyFigureController.myColor;
		}
	}

	public void EnemyMove()
	{
		List<GameObject> possibleMoves=new List<GameObject>();

		possibleMoves=boardController.GetValidMoves(enemyFigureController.x,enemyFigureController.y);

		GameObject moveChoice = possibleMoves[Random.Range(0,possibleMoves.Count)];
		TileController moveLocationTileController= moveChoice.GetComponent<TileController>();

		int moveX=moveLocationTileController.x;
		int moveY=moveLocationTileController.y;
		enemyFigureController.x=moveX;
		enemyFigureController.y=moveY;

		boardController.PositionFigure(enemyFigure,moveX,moveY);
	}
		


}
