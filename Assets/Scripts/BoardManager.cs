﻿using UnityEngine;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

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

	private Dictionary<RoomIndex, Room> roomsDict = new Dictionary<RoomIndex, Room>();

	public class RoomIndex{
		public int x;
		public int y;
		public RoomIndex(int x, int y){
			this.x = x;
			this.y = y;
		}

		public override bool Equals (object obj)
		{
			RoomIndex ri = (RoomIndex)obj;
			return (ri.x == this.x && ri.y == this.y);
		}
		//TODO Dont know if this hash is optimal
		public override int GetHashCode ()
		{
			return x * 10000 + y;
		}

	}

	public void StetupScene(){

		if (FindObjectOfType<EventSystem> () == null) {

		}

		roomsHolder = new GameObject ("Rooms").transform;
		sinnersHolder = new GameObject ("Sinners").transform;

		//Create the deskroom
		deskRoomInstance = (Instantiate (deskRoomPrefab, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<DeskRoom>();
		AddRoomAtIndexAndSetPosition (new RoomIndex (-1, 0), deskRoomInstance);

		//create waiting Room
		firstWaitRoomInstance = (Instantiate (waitingRoomPrefab, Vector3.zero, Quaternion.identity)as GameObject).GetComponent<WaitingRoom>();
		AddRoomAtIndexAndSetPosition(new RoomIndex(-2, 0), firstWaitRoomInstance);

		firstWaitRoomInstance.nextRoom = deskRoomInstance;


		CreateInitialRooms ();

		foreach (RoomIndex ri in roomsDict.Keys) {
			if(ri.x == 0){
				deskRoomInstance.AddToFirstRoomsList(roomsDict[ri]);
			}
		}

	}

	private void CreateInitialRooms(){
		Room exitRoom = AddRoomAtIndexAndSetPosition (new RoomIndex (1, 0), (Instantiate (exitRoomPrefab) as GameObject).GetComponent<Room> ());
		AddRoomAtIndexAndSetPosition (new RoomIndex (0, 0), (Instantiate (punishmentRoomPrefabs[0]) as GameObject).GetComponent<Room> ()).nextRoom = exitRoom;

	}

	void Start(){
		InvokeRepeating ("TrySendNextSinner", 1.0F, 1.5F);
	}

	void TrySendNextSinner(){
		if (firstWaitRoomInstance.CanSinnerArrive ()) {
			Sinner si = (Instantiate(sinnerPrefab) as GameObject).GetComponent<Sinner>();
			si.transform.SetParent(sinnersHolder);
			//TODO change (random?) sin.Type assigment and amount
			si.Sins.Add(new Sin(Sin.Type.Wrath, 100));
			firstWaitRoomInstance.OnSinnerArive(si);
		}
	}


	public bool IsSlotFree(RoomIndex ri){
		return !roomsDict.ContainsKey (ri);
	}
	public bool IsSlotFree(int indexX, int indexY){
		return IsSlotFree(new RoomIndex(indexX, indexY));
	}

	public Room AddRoomAtIndex(RoomIndex ri, Room room){
		roomsDict.Add (ri, room);
		if (ri.x == 0) {
			deskRoomInstance.AddToFirstRoomsList(roomsDict[ri]);
		}
		room.transform.SetParent (roomsHolder);
		return room;
	}

	public Room AddRoomAtIndexAndSetPosition(RoomIndex ri, Room room){
		AddRoomAtIndex (ri, room);
		room.transform.position = GridToWorld3 (ri);
		return room;
	}

	public Vector2 GridToWorld2(RoomIndex ri){
		return new Vector2((roomWidth + distanceInterRoomsWidth)*(ri.x), (ri.y)*(roomHeight + distanceInterRoomsHeight) - distanceInterRoomsHeight/2);
	}
	public Vector3 GridToWorld3(RoomIndex ri){
		return new Vector3((roomWidth + distanceInterRoomsWidth)*(ri.x), (ri.y)*(roomHeight + distanceInterRoomsHeight) - distanceInterRoomsHeight/2, 0);
	}

	public RoomIndex WorldSnapToGrid(Vector2 worldPos){
		return new RoomIndex (Mathf.RoundToInt((worldPos.x/(roomWidth + distanceInterRoomsWidth))),Mathf.RoundToInt( (worldPos.y)/(roomHeight + distanceInterRoomsHeight)));
	}
	
}
