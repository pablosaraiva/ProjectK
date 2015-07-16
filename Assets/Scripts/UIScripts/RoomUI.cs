using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class RoomUI : MonoBehaviour
{

	public GameObject CanvasRoomPrefab;
	private Room room;
	public GameObject linkPrefab;

	private List<GameObject> linkButtons = new List<GameObject> ();

	public void Start ()
	{
		room = transform.GetComponentInParent<Room> ();
	}

	public void OnClickRoom ()
	{
		if (room.isReady) {
			GameObject canvasRoom = Instantiate (CanvasRoomPrefab) as GameObject;
			
			canvasRoom.GetComponent<RoomCanvasUIScript> ().RoomUI = this;
			canvasRoom.GetComponent<RoomCanvasUIScript> ().Room = room;
		} else {
			room.isReady = true;
		}
	}

	public void OnClickLink ()
	{
		if (room == null || room.BoardManager == null)
			return;
		
		
		foreach (Room adjRoom in room.BoardManager.AdjacentRooms(room)) {
			GameObject link = Instantiate (linkPrefab, adjRoom.transform.position, Quaternion.identity) as GameObject;
			Room capturedRoom = adjRoom;
			Room thisCapturedRoom = this.room;
			//link.transform.GetComponentInChildren<EventTrigger>().OnPointerClick(
			/*
			link.transform.GetComponentInChildren<Button>().onClick.AddListener(() => {
				thisCapturedRoom.NextRoom = capturedRoom;
				
				ClickCancel();
			});*/
			linkButtons.Add (link);
		}
	}

	public void ClickCancel ()
	{

	}

	public void OnClickCloseButton ()
	{
		room.BoardManager.RemoveRoom (room);
	}

	public void SetHighLight (bool hightlight)
	{
		if (room != null)
			room.HightLightPipes (hightlight);
	}

	Room Room {
		get {
			return this.room;
		}
		set {
			room = value;
		}
	}

}
