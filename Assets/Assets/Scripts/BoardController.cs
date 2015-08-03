using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BoardController : MonoBehaviour 
{
	public GameObject tilePrefab;
	public GameObject boardDisplay;
	public GameController gameController;
	public int boardSize=5;
	public int figureOffset=5;
	public float boardOffsetX=0f;
	public float boardOffsetY=1000f;



	private int boardSpace=140;

	GameObject[,] board;

	void Awake() {
		board = new GameObject[boardSize,boardSize];
		GenerateBoard();
	
	}

	public int GetSpeedAt(int xLoc, int yLoc)
	{
		int tempSpeed;

		tempSpeed=board[xLoc,yLoc].GetComponent<TileController>().speed;

		return tempSpeed;
	}

	public int GetAttackAt(int xLoc, int yLoc)
	{
		int tempAttack;
		
		tempAttack=board[xLoc,yLoc].GetComponent<TileController>().attack;
		
		return tempAttack;
	}

	public void PeekAtTilesInRange(int xStart, int yStart)
	{
		List<GameObject> tilesInRange=GetValidMoves(xStart,yStart);

		foreach (GameObject tile in tilesInRange)
		{
			TileController tempTileController = tile.GetComponent<TileController>();
			tempTileController.peek=true;
			tempTileController.SetValueDisplay();
		}
	}

	public void UnPeekTiles()
	{
		foreach (GameObject tile in board)
		{
			TileController tempTileController = tile.GetComponent<TileController>();

			tempTileController.peek=false;
			tempTileController.SetValueDisplay();
		}
	}

	public void RevealTileAt(int xReveal, int yReveal)
	{
		TileController tempTileController = board[xReveal,yReveal].GetComponent<TileController>();
		tempTileController.revealed=true;
		tempTileController.SetValueDisplay();
	}
	
	void GenerateBoard() {
		for (int colCounter=0;colCounter<boardSize;colCounter++)
		{
			for (int rowCounter=0;rowCounter<boardSize;rowCounter++)
			{
				//generate the tile object
				GameObject tempDisplay = Instantiate(tilePrefab) as GameObject;
				//parent the tile to the game board so its coordinates are relative to the board space
				tempDisplay.transform.SetParent(boardDisplay.transform);
				//adds the tile to the board so it can be referenced later
				board[colCounter,rowCounter] = tempDisplay;
				//temporary scale and positions
				Vector3 scale = new Vector3 (1,1,1);
				Vector3 tempPosition = GetSpacePosition(colCounter, rowCounter);
				//move the tile, GUI objects are moved by recttransform and not by the gameobject transform
				tempDisplay.GetComponent<RectTransform>().anchoredPosition = tempPosition;
				tempDisplay.transform.localScale = scale;
				//sets the coordinates of the tile so the tile can return them when it is clicked
				//otherwise the board has to be iterated over to determine which tile was clicked
				tempDisplay.GetComponent<TileController>().SetCoords(colCounter,rowCounter);

			}
		}
	}

	public void PlaceFigure(GameObject figureToPlace) 
	{
		figureToPlace.transform.SetParent(boardDisplay.transform);
		figureToPlace.transform.localScale=new Vector3(1,1,1);
	}

	public void PositionFigure(GameObject figureToPosition,int xCoord, int yCoord)
	{
		Vector3 tempPosition = GetSpacePosition(xCoord, yCoord);

		tempPosition= new Vector3(tempPosition.x + figureOffset, tempPosition.y + figureOffset, tempPosition.z);

		figureToPosition.GetComponent<RectTransform>().anchoredPosition = tempPosition;
	}

	Vector3 GetSpacePosition(int xCoord, int yCoord)
	{
		Vector3 tempSpacePostion=new Vector3(xCoord * boardSpace + boardOffsetX, yCoord * boardSpace + boardOffsetY, 0);

		return tempSpacePostion;
	}

	public void HighLightValidMoves(GameObject player)
	{
		FigureController playerController = player.GetComponent<FigureController>();

		foreach (GameObject tile in board)
		{
			TileController tempTileController= tile.GetComponent<TileController>();
			Button tempButton = tempTileController.GetComponent<Button>();
			tempButton.interactable=true;
			if (!(gameController.IsValidMove(playerController.x, playerController.y, tempTileController.x,tempTileController.y)))
			{
				tempButton.interactable=false;
			}
		}
	}

	public List<GameObject> GetValidMoves(int xCoord, int yCoord)
	{
		List <GameObject> validMoveList=new List<GameObject>();

		foreach (GameObject tile in board)
		{
			TileController tempTileController = tile.GetComponent<TileController>();
			int tileX = tempTileController.x;
			int tileY = tempTileController.y;

			if (gameController.IsValidMove(xCoord, yCoord, tileX, tileY))
			{
				validMoveList.Add(tile);
			}
		}

		return validMoveList;
	}
}

