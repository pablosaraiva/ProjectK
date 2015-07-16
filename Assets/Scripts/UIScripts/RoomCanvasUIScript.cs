using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RoomCanvasUIScript : MonoBehaviour {

	private RoomUI roomUI;
	private Room room;

	public GameObject linkPrefab;
	private List<GameObject> buttons = new List<GameObject>();

	public void ClickBuyLink(){
		if (room == null || room.BoardManager == null) {
			return;
		}

		foreach (Room adjRoom in room.BoardManager.AdjacentRooms(room)) {
			GameObject link = Instantiate(linkPrefab, adjRoom.transform.position, Quaternion.identity) as GameObject;
			Room capturedRoom = adjRoom;
			Room thisCapturedRoom = this.room;
			link.transform.GetComponentInChildren<Button>().onClick.AddListener(() => {
				thisCapturedRoom.NextRoom = capturedRoom;
				
				ClickExit();
			});
			buttons.Add(link);
		}
	}

	public void ClickExit(){
		Destroy(this.gameObject);

	}

	public RoomUI RoomUI {
		get {
			return this.roomUI;
		}
		set {
			roomUI = value;
		}
	}

	public Room Room {
		get {
			return this.room;
		}
		set {
			room = value;
		}
	}
}
