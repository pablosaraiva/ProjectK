using UnityEngine;
using System;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	public int maxSlotWidth = 4;
	public int maxSlotHeight = 4;

	public GameObject sinnerPrefab;
	public GameObject waitingRoomPrefab;
	public GameObject deskRoomPrefab;
	public GameObject exitRoomPrefab;
	public GameObject[] punishmentRoomPrefabs;

	private GameObject firstWaitRoomInstance;
	private GameObject deskRoomInstance;

	//TODO pass it in some other way
	private float roomWidth = Room.roomWidth;
	private float roomHeight = Room.roomHeight;

	private int distanceInterRoomsWidth = 30;
	private int distanceInterRoomsHeight = 30;

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

		Transform boardHolder = new GameObject ("Rooms").transform;
		//Create the deskroom
		deskRoomInstance = Instantiate (deskRoomPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		deskRoomInstance.transform.SetParent(boardHolder);

		//create waiting Room
		firstWaitRoomInstance = Instantiate (waitingRoomPrefab, new Vector3 (-(roomWidth + distanceInterRoomsWidth), 0, 0), Quaternion.identity)as GameObject;
		firstWaitRoomInstance.transform.SetParent(boardHolder);

		firstWaitRoomInstance.GetComponent<Room> ().nextRoom = deskRoomInstance.GetComponent<Room> ();


		for (int x  = 0; x<maxSlotWidth; x++) {
			for (int y = 0; y < maxSlotHeight; y++) {
				GameObject toInstantiate = waitingRoomPrefab;
				if( x == 0 && y == (int)maxSlotHeight/2){
					toInstantiate = punishmentRoomPrefabs[0];
				}else if( x == 1 && y == (int)maxSlotHeight/2){
					toInstantiate = exitRoomPrefab;
				}
				GameObject instance = Instantiate(toInstantiate, new Vector3((roomWidth + distanceInterRoomsWidth)*(1+x), (y-(int)maxSlotHeight/2)*(roomHeight + distanceInterRoomsHeight), 0), Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);

				roomsDict.Add(new RoomIndex(x,y), instance.GetComponent<Room>());
			}
		}

		roomsDict [new RoomIndex (0, (int)maxSlotHeight / 2)].nextRoom = roomsDict [new RoomIndex (0, (int)maxSlotHeight / 2)];
		deskRoomInstance.GetComponent<Room> ().nextRoom = roomsDict [new RoomIndex (0, (int)maxSlotHeight / 2)];

	}

	private float timeCounterToNextSinner = 0;
	private float nextSinnerDelta = 1.5F;

	void Update(){
		timeCounterToNextSinner += Time.deltaTime;

		if (timeCounterToNextSinner >= nextSinnerDelta && firstWaitRoomInstance.GetComponent<Room>().CanSinnerArrive()) {
			firstWaitRoomInstance.GetComponent<Room>().OnSinnerArive((Instantiate(sinnerPrefab) as GameObject).GetComponent<Sinner>());
			timeCounterToNextSinner = 0;
		}
	}
	
}
