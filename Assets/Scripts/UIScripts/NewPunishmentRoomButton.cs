using UnityEngine;
using System.Collections;

public class NewPunishmentRoomButton : MonoBehaviour {

	public GameObject punishmentRoomPrefab;
	private GameObject newRoom;

	void Start() {
		CanvasRenderer renderer = GetComponent<CanvasRenderer> () as CanvasRenderer;
		renderer.SetAlpha (0.75f);
	}

	public void Clicked() {
		newRoom = Instantiate (punishmentRoomPrefab);
	}

	public void OnGUI() {
		if (newRoom != null) {
			Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			newRoom.transform.position =  position;
			if (Input.GetButtonDown("Fire1")) {
				newRoom = null;
			}
		}
	}
}
