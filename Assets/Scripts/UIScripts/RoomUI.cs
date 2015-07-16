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
		if (room.isReady) {
			GameObject canvasRoom = Instantiate (CanvasRoomPrefab) as GameObject;
			
			canvasRoom.GetComponent<RoomCanvasUIScript> ().RoomUI = this;
			canvasRoom.GetComponent<RoomCanvasUIScript> ().Room = room;
		} else {
			room.isReady = true;
		}
	}

	public void OnClickCloseButton ()
	{
		Debug.Log ("Begin OnClickCloseButton");

		if (room == null || room.BoardManager == null) {
			return;
		}

		Destroy (room.BoardManager.roomsDict [room.roomIndex]);

		room.BoardManager.roomsDict.Remove (room.roomIndex);

		room = null;

		Debug.Log ("End OnClickCloseButton");
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
