using UnityEngine;
using System.Collections;

public class NewPunishmentRoomButton : MonoBehaviour {

	void Start() {
		CanvasRenderer renderer = GetComponent<CanvasRenderer> () as CanvasRenderer;
		renderer.SetAlpha (0.75f);
	}

	public void Clicked() {
		print ("Clicked!");
	}
}
