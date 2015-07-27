using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class RoomUI : MonoBehaviour
{


	private Room room;
	public GameObject linkPrefab;

	private List<GameObject> linkButtons = new List<GameObject> ();

	public void Start ()
	{
		room = transform.GetComponentInParent<Room> ();
		if (room.NextRoom != null) {
			//maybe pass that in the inspector.
			this.transform.FindChild("Canvas").FindChild("LinkImage").gameObject.SetActive(false);
			this.transform.FindChild("Canvas").FindChild("CancelPipe").gameObject.SetActive(true);
		}
	}

	public void OnClickRoom ()
	{
		//At this moment we are not using this. But it may be usefull
		/*
		if (room.isReady) {
			GameObject canvasRoom = Instantiate (CanvasRoomPrefab) as GameObject;
			
			canvasRoom.GetComponent<RoomCanvasUIScript> ().RoomUI = this;
			canvasRoom.GetComponent<RoomCanvasUIScript> ().Room = room;
		} else {
			room.isReady = true;
		}*/
	}

	public void OnClickLink ()
	{
		if (room == null || room.BoardManager == null)
			return;
		
		
		foreach (Room adjRoom in room.BoardManager.AdjacentRooms(room)) {
			GameObject link = Instantiate (linkPrefab, adjRoom.transform.position, Quaternion.identity) as GameObject;
			Room capturedRoom = adjRoom;
			Room thisCapturedRoom = this.room;
			EventTrigger trigger = link.transform.GetComponentInChildren<EventTrigger> ();
			EventTrigger.Entry entry = new EventTrigger.Entry ();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener ((eventData) => {
				thisCapturedRoom.NextRoom = capturedRoom;
				ClaerLinkButtons();
			});
			trigger.triggers.Add (entry);
			linkButtons.Add (link);
		}
	}

	public void ClickCancel ()
	{
		room.NextRoom = null;
		ClaerLinkButtons ();
	}

	private void ClaerLinkButtons(){
		
		foreach (GameObject link in linkButtons) {
			Destroy (link);
		}
		linkButtons.Clear ();
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
