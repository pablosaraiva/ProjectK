using UnityEngine;
using System;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	public int maxSlotWidth = 4;
	public int maxSlotHeight = 4;

	public GameObject waitingRoom;
	public GameObject deskRoom;
	public GameObject exitRoom;

	//TODO pass it in some other way
	private int roomWidth = 128;
	private int roomHeight = 64;

	private int distanceInterRoomsWidth = 20;
	private int distanceInterRoomsHeight = 20;

	public GameObject[] sinRooms;

	public void StetupScene(){
		Transform boardHolder = new GameObject ("Rooms").transform;
		//Create the deskroom
		GameObject deskRoomInstance = Instantiate (deskRoom, Vector3.zero, Quaternion.identity) as GameObject;
		deskRoomInstance.transform.SetParent(boardHolder);

		//create waiting Room
		GameObject firstWaitRoomInstance = Instantiate (waitingRoom, new Vector3 (-(roomWidth + distanceInterRoomsWidth), 0, 0), Quaternion.identity)as GameObject;
		firstWaitRoomInstance.transform.SetParent(boardHolder);


		for (int x  = 0; x<maxSlotWidth; x++) {
			for (int y = 0; y < maxSlotHeight; y++) {
				GameObject toInstantiate = waitingRoom;
				if( x == 0 && y == (int)maxSlotHeight/2){
					toInstantiate = sinRooms[0];
				}else if( x == 1 && y == (int)maxSlotHeight/2){
					toInstantiate = exitRoom;
				}
				GameObject instance = Instantiate(toInstantiate, new Vector3((roomWidth + distanceInterRoomsWidth)*(1+x), (y-(int)maxSlotHeight/2)*(roomHeight + distanceInterRoomsHeight), 0), Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
			}
		}


	}

	
}
