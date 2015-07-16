using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RoomUI : MonoBehaviour
{

	public GameObject CanvasRoomPrefab;
	private Room room;

	public void Start ()
	{
		room = transform.GetComponentInParent<Room> ();
	}

	public void OnClickRoom ()
	{
		GameObject canvasRoom = Instantiate (CanvasRoomPrefab) as GameObject;

		canvasRoom.GetComponent<RoomCanvasUIScript> ().RoomUI = this;
		canvasRoom.GetComponent<RoomCanvasUIScript> ().Room = room;
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
