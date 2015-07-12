using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	[HideInInspector]
	public static GameManagerScript instance = null;

	public BoardManager boardScript;
	public GameObject gameHUD;
	private GameData gameData;

	// Use this for initialization
	void Awake () {
		//Singleton
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	void InitGame(){
		gameData = new GameData ();
		boardScript.StetupScene ();
		GameObject HudManager = Instantiate (gameHUD);
		HudManager.transform.Find ("NewPunishmentRoomButton").GetComponent<NewPunishmentRoomButton> ().setBoardManager(boardScript);

		int loadGame = PlayerPrefs.GetInt ("LoadGame");
		if (loadGame == 1) {
			LoadGame();
		}

		UpdateHUD ();
	}

	public void AddSinPoints(int sinPoints) {	
		gameData.sinPoints += sinPoints;

		UpdateHUD ();

		SaveLoad.Save (gameData);
	}

	public void RemoveSinPoints(int sinPoints) {
		AddSinPoints (-sinPoints);
	}

	public void LoadGame() {
		gameData = SaveLoad.Load ();
	}

	private void UpdateHUD()
	{
		GameHUDScript gameHUDScript = gameHUD.GetComponent<GameHUDScript>();
		gameHUDScript.SetSinPoints (gameData.sinPoints);
	}

	                                         
}
