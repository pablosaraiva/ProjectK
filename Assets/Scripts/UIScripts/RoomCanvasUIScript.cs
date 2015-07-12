using UnityEngine;
using System.Collections;

public class RoomCanvasUIScript : MonoBehaviour {

	private RoomUI roomUI;

	public void ClickBuyLink(){
		roomUI.OnClickToLink ();
	}

	public void ClickExit(){
		roomUI.OnClickCancelLinkButtons ();
		Destroy(this.gameObject);
	}

	public void ClickCancel(){
		roomUI.OnClickCancelLinkButtons ();
	}

	public RoomUI RoomUI {
		get {
			return this.roomUI;
		}
		set {
			roomUI = value;
		}
	}
}
