using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

	public float roomPunishmentPoints = 50;
	public Room nextRoom;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SinnerLeaveRoom(Sinner sinner) {
		sinner.Punish (roomPunishmentPoints);
	}
}
