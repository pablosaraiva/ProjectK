using UnityEngine;
using System.Collections;

public class NewPunishmentRoomButton : MonoBehaviour {

	public GameObject punishmentRoomPrefab;
	private GameObject newRoom;
	public UIShowHide doh;
	private BoardManager boardManager;

	public void setBoardManager(BoardManager boardManager) {
		this.boardManager = boardManager;
	}

	void Start() {
		CanvasRenderer renderer = GetComponent<CanvasRenderer> () as CanvasRenderer;
		renderer.SetAlpha (0.75f);
	}

	public void Clicked() {
		newRoom = Instantiate (punishmentRoomPrefab);
	}

	public void OnGUI() {
		if (newRoom != null){
			Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RoomIndex gridIndex = boardManager.WorldSnapToGrid(mouseWorldPosition);
			newRoom.transform.position =  boardManager.GridToWorld2(gridIndex);

			if (!boardManager.IsSlotFree(gridIndex)) {
				doh.show ();
			} else {
				doh.hide ();
				if (Input.GetButtonDown("Fire1")) {
					boardManager.AddRoomAtIndex(gridIndex, newRoom.GetComponent<Room>());
					newRoom = null;
					GameManagerScript gameManagerScript = GameManagerScript.instance;
					gameManagerScript.RemoveSinPoints(100);
				} else if (Input.GetButtonDown("Fire2")) {
					Destroy (newRoom);
				}
			}
		}
	}
}
