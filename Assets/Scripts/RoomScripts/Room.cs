using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public abstract class Room : MonoBehaviour {

	private Room nextRoom;
	[HideInInspector]
	public bool reserved = false;

	public readonly static float roomWidth = 128-6;
	public readonly static float roomHeight = 64;
	public readonly static float sinnerWidth = 18;

	public GameObject pipePrefab;
	private PipeScript pipe;
	private BoardManager boardManager;
	private BoardManager.RoomIndex roomIndex;

	public virtual bool HasNextRoom(){
		return nextRoom != null;
	}

	public abstract bool CanSinnerArrive();

	//To be set true on normal room, and false on exit room and special cases
	public abstract bool CanSetNextRoom();

	//Use to put sinner on array(waiting Room) or save referenca(Punishment Room)
	public abstract void OnSinnerArive(Sinner sinner);

	//To call before go to room, when you need to walk outside the previous room, to stop other sinners to enter
	public virtual void ReserveSinnerPlace(){
		reserved = true;
	}

	public virtual void WalkOutsideCallBack(Sinner sinner){
		if (nextRoom != null) {
			nextRoom.OnSinnerArive(sinner);
		}
	}

	public Room NextRoom {
		get {
			return this.nextRoom;
		}
		set {
			nextRoom = value;
			if(nextRoom!=null){
				if(pipe==null) pipe = (Instantiate(pipePrefab) as GameObject).GetComponent<PipeScript>();
				pipe.SetPositionAndScale(this.transform, nextRoom.transform);
			}else if (pipe!=null){
				Destroy(pipe.gameObject);
			}
		}
	}

	public BoardManager BoardManager {
		get {
			return this.boardManager;
		}
		set {
			boardManager = value;
		}
	}

	public BoardManager.RoomIndex RoomIndex {
		get {
			return this.roomIndex;
		}
		set {
			roomIndex = value;
		}
	}


	public List<GameObject> NextRoomButtonClick(GameObject linkPrefab){
		if (boardManager == null)
			return null;

		List<GameObject> buttonsList = new List<GameObject> ();
		foreach (Room adjRoom in boardManager.AdjacentRooms (this)) {
			GameObject link = Instantiate(linkPrefab, adjRoom.transform.position, Quaternion.identity) as GameObject;
			Room capturedRoom = adjRoom;
			Room thisCapturedRoom = this;
			link.transform.GetComponentInChildren<Button>().onClick.AddListener(() => {

				thisCapturedRoom.NextRoom = capturedRoom;
			});
			buttonsList.Add(link);
		}
		return buttonsList;
	}

	public void HightLightPipes(bool highlight){
		if(pipe!= null)
			pipe.SetHighLight (highlight);
		if (nextRoom != null)
			nextRoom.HightLightPipes (highlight);
	}

}
