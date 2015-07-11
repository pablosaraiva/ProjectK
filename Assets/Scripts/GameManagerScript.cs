using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	[HideInInspector]
	public static GameManagerScript instance = null;

	public BoardManager boardScript;
	public GameObject gameHUD;
	private int sinPoints = 0;

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
		this.sinPoints += sinPoints;

		UpdateHUD ();

		GameData gameData = new GameData ();
		gameData.sinPoints = this.sinPoints;
		SaveLoad.Save (gameData);
	}

	public void RemoveSinPoints(int sinPoints) {
		AddSinPoints (-sinPoints);
	}

	public void LoadGame() {
		GameData gameData = SaveLoad.Load ();
		this.sinPoints = gameData.sinPoints;
	}

	private void UpdateHUD()
	{
		GameHUDScript gameHUDScript = gameHUD.GetComponent<GameHUDScript>();
		gameHUDScript.SetSinPoints (this.sinPoints);
	}

	                                         
}
