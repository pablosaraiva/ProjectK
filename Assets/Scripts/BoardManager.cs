using UnityEngine;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{

	public int maxSlotWidth = 4;
	public int maxSlotHeight = 4;

	public GameObject sinnerPrefab;
	public GameObject waitingRoomPrefab;
	public GameObject deskRoomPrefab;
	public GameObject exitRoomPrefab;
	public GameObject[] punishmentRoomPrefabs;
	
	private WaitingRoom firstWaitRoomInstance;
	private DeskRoom deskRoomInstance;

	private float roomWidth = Room.roomWidth;
	private float roomHeight = Room.roomHeight;

	private int distanceInterRoomsWidth = 80;
	private int distanceInterRoomsHeight = 40;

	private Transform roomsHolder;
	private Transform sinnersHolder;

	public Dictionary<RoomIndex, Room> roomsDict = new Dictionary<RoomIndex, Room> ();

	public void StetupScene ()
	{

		if (FindObjectOfType<EventSystem> () == null) {

		}

		roomsHolder = new GameObject ("Rooms").transform;
		sinnersHolder = new GameObject ("Sinners").transform;

		//Create the deskroom
		deskRoomInstance = (Instantiate (deskRoomPrefab, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<DeskRoom> ();
		AddRoomAtIndexAndSetPosition (new RoomIndex (-1, 0), deskRoomInstance);

		//create waiting Room
		firstWaitRoomInstance = (Instantiate (waitingRoomPrefab, Vector3.zero, Quaternion.identity)as GameObject).GetComponent<WaitingRoom> ();
		AddRoomAtIndexAndSetPosition (new RoomIndex (-2, 0), firstWaitRoomInstance);

		firstWaitRoomInstance.NextRoom = deskRoomInstance;

		CreateInitialRooms ();

		foreach (RoomIndex ri in roomsDict.Keys) {
			if (ri.x == 0) {
				deskRoomInstance.AddToFirstRoomsList (roomsDict [ri]);
			}
		}

	}

	private void CreateInitialRooms ()
	{
		Room exitRoom = AddRoomAtIndexAndSetPosition (new RoomIndex (1, 0), (Instantiate (exitRoomPrefab) as GameObject).GetComponent<Room> ());
		AddRoomAtIndexAndSetPosition (new RoomIndex (0, 0), (Instantiate (punishmentRoomPrefabs [0]) as GameObject).GetComponent<Room> ()).NextRoom = exitRoom;
	}

	public void RemoveRoom (Room room)
	{
		/*
		 * Create a variable to compare if the player is trying to remove
		 * the first punishment room
		 */
		RoomIndex mainIndex = new RoomIndex (0, 0);
		
		/*
		 * Verify if the room can be removed
		 */
		if (room == null || mainIndex.Equals (room.roomIndex)) {
			return;
		}
		
		/*
		 * Remove the room from the FirstRoomsList of deskRoom
		 */
		deskRoomInstance.RemoveFromFirstRoomsList (room);
		
		/*
		 * Remove all the links this room has.
		 */
		foreach (Room adjacentRoom in AdjacentRooms(room)) {
			if (room.Equals (adjacentRoom.NextRoom)) {
				adjacentRoom.NextRoom = null;
			}
		}
		room.NextRoom = null;
		
		/*
		 * Finally, destroy the room gameObject and free the reference
		 * from the Dictorary of BoardManager.
		 */
		Destroy (roomsDict [room.roomIndex]);
		Destroy (roomsDict [room.roomIndex].gameObject);
		roomsDict.Remove (room.roomIndex);
	}

	void Start ()
	{
		InvokeRepeating ("TrySendNextSinner", 1.0F, 1.5F);
	}

	void TrySendNextSinner ()
	{
		if (firstWaitRoomInstance.CanSinnerArrive ()) {
			SinnerScript si = (Instantiate (sinnerPrefab) as GameObject).GetComponent<SinnerScript> ();
			si.transform.SetParent (sinnersHolder);
			//TODO change (random?) sin.Type assigment and amount
			si.Sins.Add (new Sin (Sin.Type.Wrath, 100));
			firstWaitRoomInstance.OnSinnerArive (si);
		}
	}


	public bool IsSlotFree (RoomIndex ri)
	{
		return !roomsDict.ContainsKey (ri);
	}
	public bool IsSlotFree (int indexX, int indexY)
	{
		return IsSlotFree (new RoomIndex (indexX, indexY));
	}

	public Room AddRoomAtIndex (RoomIndex ri, Room room)
	{
		roomsDict.Add (ri, room);
		room.roomIndex = ri;
		if (ri.x == 0) {
			deskRoomInstance.AddToFirstRoomsList (roomsDict [ri]);
		}
		room.transform.SetParent (roomsHolder);
		room.BoardManager = this;
		room.roomIndex = ri;
		return room;
	}

	public Room AddRoomAtIndexAndSetPosition (RoomIndex ri, Room room)
	{
		AddRoomAtIndex (ri, room);
		SetRoomPosition (ri, room);
		return room;
	}

	private void SetRoomPosition (RoomIndex ri, Room room)
	{
		room.transform.position = GridToWorld3 (ri);
	}

	public Vector2 GridToWorld2 (RoomIndex ri)
	{
		return new Vector2 ((roomWidth + distanceInterRoomsWidth) * (ri.x), (ri.y) * (roomHeight + distanceInterRoomsHeight) - distanceInterRoomsHeight / 2);
	}
	public Vector3 GridToWorld3 (RoomIndex ri)
	{
		return new Vector3 ((roomWidth + distanceInterRoomsWidth) * (ri.x), (ri.y) * (roomHeight + distanceInterRoomsHeight) - distanceInterRoomsHeight / 2, 0);
	}

	public RoomIndex WorldSnapToGrid (Vector2 worldPos)
	{
		return new RoomIndex (Mathf.RoundToInt ((worldPos.x / (roomWidth + distanceInterRoomsWidth))), Mathf.RoundToInt ((worldPos.y) / (roomHeight + distanceInterRoomsHeight)));
	}

	public List<Room> AdjacentRooms (Room room)
	{
		List<Room> adjRooms = new List<Room> ();
		foreach (RoomIndex ri in roomsDict.Keys) {
			if (ri != room.roomIndex && (Math.Abs (ri.x - room.roomIndex.x) < 2 && Math.Abs (ri.y - room.roomIndex.y) < 2)) {
				adjRooms.Add (roomsDict [ri]);
			}
		}
		return adjRooms;
	}
}
