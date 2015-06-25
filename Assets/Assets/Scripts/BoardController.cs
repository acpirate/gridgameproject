using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoardController : MonoBehaviour 
{
	public GameObject tilePrefab;
	public GameObject boardDisplay;
	public int boardSize=5;

	private int boardSpace=140;

	Tile[,] board;

	void Awake() {
		board = new Tile[boardSize,boardSize];
		GenerateBoard();
	}

	void GenerateBoard() {
		for (int colCounter=0;colCounter<boardSize;colCounter++)
		{
			for (int rowCounter=0;rowCounter<boardSize;rowCounter++)
			{
				GameObject tempDisplay = Instantiate(tilePrefab) as GameObject;
				tempDisplay.transform.SetParent(boardDisplay.transform);

				Vector3 scale = new Vector3 (1,1,1);



				Vector3 tempPosition = new Vector3 (colCounter * boardSpace, rowCounter * boardSpace, 0);


				tempDisplay.GetComponent<RectTransform>().anchoredPosition = tempPosition;
				tempDisplay.transform.localScale = scale;
			}
		}
	}
}


public class Tile
{
	public GameObject displayObject;
}