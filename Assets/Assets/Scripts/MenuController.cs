using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {
	

	public void loadNewScene(string newScene) {
		Application.LoadLevel (newScene);
	}

	public void exitGame() {
		Application.Quit ();
	}
}
