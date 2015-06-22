using UnityEngine;
using System.Collections;

public class Sinner : MonoBehaviour {

	public Room currentRoom;
	private float timeInRoom = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeInRoom = timeInRoom + Time.deltaTime;
		if (timeInRoom > 1) {
			timeInRoom = 0;
			GoToRoom(currentRoom.nextRoom);
		}

	}

	private void GoToRoom(Room nextRoom) {
		if (nextRoom != null) {
			currentRoom = nextRoom;
			gameObject.transform.position = nextRoom.transform.position;
		} else {
			Destroy(gameObject);
		}
	}

}
