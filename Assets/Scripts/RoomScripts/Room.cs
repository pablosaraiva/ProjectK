using UnityEngine;
using System.Collections;

public abstract class Room : MonoBehaviour {

	public Room nextRoom;
	

	public virtual bool HasNextRoom(){
		return nextRoom != null;
	}

	public abstract bool CanSinnerArrive();

	//To be set true on normal room, and false on exit room and special cases
	public abstract bool CanSetNextRoom();

	//Use to put sinner on array(waiting Room) or save referenca(Punishment Room)
	public abstract void OnSinnerArive(Sinner sinner);
}
