using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	[HideInInspector]
	public static GameManagerScript instance = null;

	public BoardManager boardScript;
	public GameObject gameHUD;
	private GameData gameData;

	void Awake () {
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

		SaveGame ();
	}

	public void RemoveSinPoints(int sinPoints) {
		AddSinPoints (-sinPoints);
	}

	private void LoadGame() {
		gameData = SaveLoad.Load ();
		foreach (RoomData roomData in gameData.boardData.roomData) {
			boardScript.AddRoomAtIndexAndSetPosition (roomData.roomIndex, (Instantiate (boardScript.punishmentRoomPrefabs [0]) as GameObject).GetComponent<Room> ());
		}
	}

	private void SaveGame() 
	{
		gameData.boardData.roomData = new RoomData[boardScript.roomsDict.Values.Count];
		int i = 0;
		foreach (Room room in boardScript.roomsDict.Values) 
		{
			if (!ShouldIgnore(room.roomIndex)) {
				RoomData roomData = new RoomData();
				roomData.roomIndex = room.roomIndex;
				gameData.boardData.roomData[i] = roomData;
				i++;
			}
		}
		SaveLoad.Save (gameData);
	}

	private void UpdateHUD()
	{
		GameHUDScript gameHUDScript = gameHUD.GetComponent<GameHUDScript>();
		gameHUDScript.SetSinPoints (gameData.sinPoints);
	}

	private bool ShouldIgnore(RoomIndex roomIndex)
	{
		if (roomIndex.y == 0) 
		{
			if ((roomIndex.x >= -2) && (roomIndex.x <= 1))
			{
				return true;
			}
		} 
		return false;
	}
}
