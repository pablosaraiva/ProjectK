using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RoomUI : MonoBehaviour {

	public GameObject linkPrefab;
	public GameObject CanvasRoomPrefab;

	private List<GameObject> buttons = new List<GameObject>();

	private Room room;

	public void Start(){
		room = transform.GetComponentInParent<Room> ();
	}

	public void OnClickOnRoom(){
		GameObject canvasRoom = Instantiate (CanvasRoomPrefab) as GameObject;

		canvasRoom.GetComponent<RoomCanvasUIScript> ().RoomUI = this;
	}


	public void OnClickToLink(){
		if (room==null || room.BoardManager == null)
			return;
		

		foreach (Room adjRoom in room.BoardManager.AdjacentRooms(room)) {
			GameObject link = Instantiate(linkPrefab, adjRoom.transform.position, Quaternion.identity) as GameObject;
			Room capturedRoom = adjRoom;
			Room thisCapturedRoom = this.room;
			link.transform.GetComponentInChildren<Button>().onClick.AddListener(() => {
				thisCapturedRoom.NextRoom = capturedRoom;

				OnClickCancelLinkButtons();
			});
			buttons.Add(link);
		}

	}

	public void OnClickCancelLinkButtons(){
		foreach (GameObject go in buttons) {
			Destroy(go.gameObject);
		}
		buttons.Clear();
	}

	public void SetHighLight(bool hightlight){
		if(room!= null)
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
