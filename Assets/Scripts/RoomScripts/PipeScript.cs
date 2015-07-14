using UnityEngine;
using System.Collections;

public class PipeScript : MonoBehaviour {

	private bool highlight = false;

	private SpriteRenderer pipeRenderer;
	void Start(){
		pipeRenderer = transform.Find ("PipeImage").GetComponent<SpriteRenderer> ();
	}

	public bool HighLight {
		get {
			return this.highlight;
		}
		set {
			highlight = value;
			Color c = pipeRenderer.color;
			if (highlight) {
				c.a = 1;
			} else {
				c.a = 45F/255F;
			}
			pipeRenderer.color = c;
		}
	}

	//TODO link on a better way, for no total overlapsing
	//TODO Clean this mess
	public void SetPositionAndScale(Transform roomFrom, Transform roomTo){
		Vector3 enterPoint = roomFrom.position + new Vector3 (Room.roomWidth / 2F, Room.roomHeight / 2F, 0);
		Vector3 exitPoint = roomTo.position + new Vector3 (- Room.roomWidth / 2F, Room.roomHeight / 2F, 0);
		//Debug.Log (roomTo.position);
		transform.position = ((exitPoint + enterPoint) / 2F);
		transform.rotation = Quaternion.identity;
		//print (enterPoint + "  " + exitPoint + "  " + Vector3.Angle(new Vector3(1,0,0), exitPoint - enterPoint));
		float angle = Vector3.Angle (new Vector3 (1, 0, 0), exitPoint - enterPoint);
		if (Vector3.Cross (new Vector3(1, 0, 0), exitPoint - enterPoint).z < 0)
			angle = 360 - angle;
		transform.Rotate (new Vector3 (0,0,angle));
		transform.localScale = new Vector3 (Vector3.Distance(enterPoint, exitPoint), 1, 1);
	}
}
