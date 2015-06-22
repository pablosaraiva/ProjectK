using UnityEngine;
using System.Collections;

public class Sinner : MonoBehaviour {

	public Room currentRoom;
	private float timeInRoom = 0;
	public float sinPoints = 200;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		print (sinPoints);
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
			Game game = gameObject.GetComponent<Game>();
			game.SinnerLeaveRoom(this, currentRoom);
		} else {
			Destroy(gameObject);
		}
	}

	public void Punish(float punishmentPoints) {
		this.sinPoints = this.sinPoints - punishmentPoints;
		if (sinPoints < 0) {
			sinPoints = 0;
		}
	}

}
