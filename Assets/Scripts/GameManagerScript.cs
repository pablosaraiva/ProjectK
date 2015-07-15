using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour 
{

	[HideInInspector]
	public static GameManagerScript instance = null;
	[HideInInspector]
	public BoardManager boardManagerScript;
	public GameObject gameHUD;
	private GameData gameData;

	void Awake () 
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		boardManagerScript = GetComponent<BoardManager> ();

		InitGame ();
	}

	void InitGame()
	{
		gameData = new GameData ();
		boardManagerScript.StetupScene ();
		GameObject HudManager = Instantiate (gameHUD);
		HudManager.transform.Find ("NewPunishmentRoomButton").GetComponent<NewPunishmentRoomButton> ().setBoardManager(boardManagerScript);


		int loadGame = PlayerPrefs.GetInt ("LoadGame");
		if (loadGame == 1) {
			LoadGame();
		}
		UpdateHUD ();
	}

	public void AddSinPoints(int sinPoints) 
	{	
		gameData.sinPoints += sinPoints;

		UpdateHUD ();

		SaveGame ();
	}

	public void RemoveSinPoints(int sinPoints) 
	{
		AddSinPoints (-sinPoints);
	}

	private void LoadGame() 
	{
		gameData = SaveLoad.Load ();

		// We have to add all the rooms before start creating the relationships

		// Create rooms
		foreach (RoomData roomData in gameData.boardData.roomDataList) {
			boardManagerScript.AddRoomAtIndexAndSetPosition (roomData.roomIndex, (Instantiate (boardManagerScript.punishmentRoomPrefabs [0]) as GameObject).GetComponent<Room> ());
		}

		// Add Sinners
		foreach (SinnerData sinnerData in gameData.boardData.sinnerDataList) {
			GameObject sinner = Instantiate(boardManagerScript.sinnerPrefab);
			SinnerScript sinnerScript = sinner.GetComponent<SinnerScript>();
			sinnerScript.Sins.Add (new Sin(Sin.Type.Wrath, 100));

			Room room;
			boardManagerScript.roomsDict.TryGetValue(sinnerData.currentRoomIndex, out room);
			room.OnSinnerArive(sinnerScript);
		}

		// Add relationships
		foreach (RoomData roomData in gameData.boardData.roomDataList) {
			Room room;
			if (boardManagerScript.roomsDict.TryGetValue(roomData.roomIndex, out room))
			{
				if (roomData.nextRoomIndex != null)
				{
					Room nextRoom;
					if (boardManagerScript.roomsDict.TryGetValue(roomData.nextRoomIndex, out nextRoom))
					{
						room.NextRoom = nextRoom;
					}
				}
			}
		}
	}

	private void SaveGame() 
	{
		// Rooms and relationships
		gameData.boardData.roomDataList.Clear ();
		foreach (Room room in boardManagerScript.roomsDict.Values) 
		{
			if (!ShouldIgnore(room.roomIndex)) {
				RoomData roomData = new RoomData();
				roomData.roomIndex = room.roomIndex;
				if (room.HasNextRoom()) {
					roomData.nextRoomIndex = room.NextRoom.roomIndex;
				}

				gameData.boardData.roomDataList.Add(roomData);
			}
		}

		// Sinners
		GameObject[] sinners = GameObject.FindGameObjectsWithTag ("Sinner");
		foreach (GameObject sinner in sinners) 
		{
			SinnerData sinnerData = new SinnerData();
			SinnerScript sinnerScript = sinner.GetComponent<SinnerScript>();
			if (sinnerScript.currentRoom != null)
			{
				sinnerData.currentRoomIndex = sinnerScript.currentRoom.roomIndex;
				gameData.boardData.sinnerDataList.Add(sinnerData);
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
