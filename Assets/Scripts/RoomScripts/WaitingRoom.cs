using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WaitingRoom : Room {

	private int capacity = 6;
	private int reservedSlots = 0;

	private List<Sinner> sinnersList;



	private float timeCounter = 0;
	// Use this for initialization
	void Start () {
		sinnersList = new List<Sinner> ();
	}

	// Update is called once per frame
	void Update () {
		//TODO just a test method;

		//Move the sinner if time here is over
		if (timeCounter >= 7 && sinnersList.Count > 0) {
			if (HasNextRoom() && nextRoom.CanSinnerArrive()) {
				Sinner sinnerToMove = sinnersList [0];
				sinnersList.RemoveAt (0);
				sinnerToMove.MoveToTarget(this.transform.position + new Vector3(roomWidth/2 + sinnerWidth/2, 0, 0), WalkOutsideCallBack);
				nextRoom.ReserveSinnerPlace();
				timeCounter = 0;
				RearangeSinnersPosition();
			}
		} else if (sinnersList.Count > 0) {
			timeCounter += Time.deltaTime;
		}
	}

	public override bool HasNextRoom ()
	{
		return base.HasNextRoom ();
	}

	public override bool CanSetNextRoom () {
		return true;
	}

	
	public override bool CanSinnerArrive ()
	{
		return sinnersList.Count + reservedSlots < capacity;
	}

	public override void OnSinnerArive (Sinner sinner) {
		reservedSlots = Mathf.Max (0, reservedSlots - 1);
		sinnersList.Add (sinner);
		//TODO fix entry position
		Vector3 startPos = new Vector3 (this.transform.position.x - (roomWidth/2 + sinnerWidth/2), this.transform.position.y + Random.Range(-3F, 3F), 0);
		sinner.transform.position = startPos;

		RearangeSinnersPosition ();
	}

	private void RearangeSinnersPosition(){
		float distanceInterSinners = (roomWidth - sinnerWidth*1.5F)/(capacity -1);
		foreach(Sinner si in sinnersList){
			Vector3 target = new Vector3 (this.transform.position.x + roomWidth/2F - sinnerWidth*1.5F/2F - distanceInterSinners*(sinnersList.IndexOf(si)), si.transform.position.y, 0);
			si.MoveToTarget (target, null);
		}
	}

	public override void ReserveSinnerPlace ()
	{
		reservedSlots++;
	}

}
