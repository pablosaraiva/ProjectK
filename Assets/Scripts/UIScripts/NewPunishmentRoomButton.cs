using UnityEngine;
using System.Collections;

public class NewPunishmentRoomButton : MonoBehaviour {

	public GameObject punishmentRoomPrefab;
	private GameObject newRoom;
	public Vector2 gridSize = new Vector2 (142, 70);
	public UIShowHide doh;

	void Start() {
		CanvasRenderer renderer = GetComponent<CanvasRenderer> () as CanvasRenderer;
		renderer.SetAlpha (0.75f);
	}

	public void Clicked() {
		newRoom = Instantiate (punishmentRoomPrefab);
	}

	public void OnGUI() {
		if (newRoom != null) {
			Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			int gridPositionX = Mathf.RoundToInt (mouseWorldPosition.x / gridSize.x);
			int gridPositionY = Mathf.RoundToInt((mouseWorldPosition.y - (gridSize.y / 2)) / gridSize.y);
			Vector2 worldPosition = new Vector2(gridSize.x * gridPositionX, gridSize.y * gridPositionY);
			newRoom.transform.position =  worldPosition;

			Rigidbody2D rb = newRoom.GetComponent<Rigidbody2D> ();
			if (rb.IsTouchingLayers()) {
				doh.show ();
			} else {
				doh.hide ();
				if (Input.GetButtonDown("Fire1")) {
					newRoom = null;
				}
			}
		}
	}
}
