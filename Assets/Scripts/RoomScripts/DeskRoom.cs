﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DeskRoom : Room {

	Sinner sinner;
	public GameObject GoFirstRoomGUIPrefab;

	private List<Room> firstRoomsList = new List<Room>();


	private float timeCounter = 0;
	void Update () {
		if (sinner != null) {
			timeCounter += Time.deltaTime;

			if(timeCounter>=2 && HasNextRoom()){
				nextRoom.OnSinnerArive(sinner);
				sinner = null;
			}
		}
	}

	public override bool HasNextRoom ()
	{
		return false;
	}

	public override bool CanSinnerArrive ()
	{
		return this.sinner == null && !reserved;
	}

	public override bool CanSetNextRoom ()
	{
		return false;
	}

	public override void OnSinnerArive (Sinner sinner)
	{
		reserved = false;
		this.sinner = sinner;
		//TODO fix entry position
		Vector3 startPos = new Vector3 (this.transform.position.x - (roomWidth/2 + sinnerWidth/2), this.transform.position.y, 0);
		sinner.transform.position = startPos;
		sinner.MoveToTarget(this.transform.position, ShowGUIOfFirstRooms);

	}

	public void ShowGUIOfFirstRooms(Sinner si){
		GameObject guiHolder = new GameObject ("GUI HOLDER");
		guiHolder.transform.SetParent (this.transform);
		foreach(Room r in firstRoomsList){
			GameObject gUICanvasMoveToFirstRoom = Instantiate(GoFirstRoomGUIPrefab, r.transform.position, Quaternion.identity) as GameObject;
			gUICanvasMoveToFirstRoom.transform.SetParent(guiHolder.transform);
			Room capturedRoom = r;
			gUICanvasMoveToFirstRoom.transform.Find("MoveButton").gameObject.GetComponent<Button>().onClick.AddListener(() => {
				nextRoom = capturedRoom;
				sinner.MoveToTarget (this.transform.position + new Vector3 (roomWidth / 2 + sinnerWidth / 2, 0, 0), WalkOutsideCallBack);
				Destroy(guiHolder);
				Invoke("FreeRoomForNextSinner", 0.8F);
			});
		}
	}

	public void AddToFirstRoomsList(Room room){
		firstRoomsList.Add (room);
	}

	public void RemoveFromFirstRoomsList(Room room){
		firstRoomsList.Remove (room);
	}

	public List<Room> GetFirstRoomsList(){
		return firstRoomsList;
	}

	private void FreeRoomForNextSinner(){
		this.sinner = null;
		this.reserved = false;
		//Gritar, PROXIMO!!!
	}

	public override void WalkOutsideCallBack (Sinner sinner)
	{
		base.WalkOutsideCallBack (sinner);
		this.nextRoom = null;

	}

}
