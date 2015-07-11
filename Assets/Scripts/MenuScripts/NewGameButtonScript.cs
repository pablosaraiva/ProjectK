using UnityEngine;
using System.Collections;

public class NewGameButtonScript : MonoBehaviour {
	public void startNewGame() {
		Application.LoadLevel (1);
		PlayerPrefs.SetInt ("LoadGame", 0);
	}

}
