using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PipeScript : MonoBehaviour {

	private bool highlight = false;

	private SpriteRenderer pipeRenderer;

	private Vector3 enterPoint, exitPoint;

	private List<PipeArrow> pipeArrowsList = new List<PipeArrow> ();

	public GameObject pipeArrowPrefab;

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
			CancelInvoke();
			if (highlight) {
				InvokeRepeating ("CreateArrows", 0.3f, 0.3f);
				c.a = 1;
			} else {
				InvokeRepeating ("CreateArrows", 0.8f, 0.8f);
				c.a = 45F/255F;
			}
			pipeRenderer.color = c;

			foreach(PipeArrow arrow in pipeArrowsList){
				arrow.SetHighLight(highlight);
			}
		}
	}

	//TODO link on a better way, for no total overlapsing
	//TODO Clean this mess
	public void SetPositionAndScale(Transform roomFrom, Transform roomTo){
		enterPoint = roomFrom.position + new Vector3 (Room.roomWidth / 2F, Room.roomHeight / 2F, 0);
		exitPoint = roomTo.position + new Vector3 (- Room.roomWidth / 2F, Room.roomHeight / 2F, 0);
		//Debug.Log (roomTo.position);
		transform.position = ((exitPoint + enterPoint) / 2F);
		transform.rotation = Quaternion.identity;
		//print (enterPoint + "  " + exitPoint + "  " + Vector3.Angle(new Vector3(1,0,0), exitPoint - enterPoint));
		float angle = Vector3.Angle (new Vector3 (1, 0, 0), exitPoint - enterPoint);
		if (Vector3.Cross (new Vector3(1, 0, 0), exitPoint - enterPoint).z < 0)
			angle = 360 - angle;
		transform.Rotate (new Vector3 (0,0,angle));
		transform.localScale = new Vector3 (Vector3.Distance(enterPoint, exitPoint), 1, 1);

		ClearArrows ();
		CancelInvoke ();
		InvokeRepeating ("CreateArrows", 0.8f, 0.8f);
	}

	public void CreateArrows(){
		PipeArrow arrow = (Instantiate (pipeArrowPrefab, enterPoint, this.transform.rotation) as GameObject).GetComponent<PipeArrow>();
		pipeArrowsList.Add (arrow);
		arrow.SetHighLight (highlight);
		arrow.MoveToTarget (exitPoint, PipeArrowArriveEndCallback);
	}

	public void PipeArrowArriveEndCallback(PipeArrow arrow){
		pipeArrowsList.Remove (arrow);
		Destroy (arrow.gameObject);
	}

	public void OnDestroy(){
		ClearArrows ();
	}

	public void ClearArrows(){
		foreach (PipeArrow arrow in pipeArrowsList) {
			//To Prevent Error when closing the game/ as the destroy is async, It veryfies if exist befero destroy
			if(arrow!=null && arrow.gameObject!=null)
				Destroy(arrow.gameObject);
		}
		pipeArrowsList.Clear ();
	}


}
