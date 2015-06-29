using UnityEngine;
using System.Collections;

public abstract class Room : MonoBehaviour {

	public Room nextRoom;
	public bool reserved = false;

	public readonly static float roomWidth = 128;
	public readonly static float roomHeight = 64;
	public readonly static float sinnerWidth = 18;

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
}
