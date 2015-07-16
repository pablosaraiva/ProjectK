using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RoomCanvasUIScript : MonoBehaviour {

	private RoomUI roomUI;
	private Room room;

	public GameObject linkPrefab;

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
