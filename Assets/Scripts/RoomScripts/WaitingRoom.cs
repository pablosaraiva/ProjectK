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

		InvokeRepeating ("TrySendToNextRoom", 1.0F, 1.0F);
	}

	// Update is called once per frame
	void Update () {
	}

	void TrySendToNextRoom(){
		if (sinnersList.Count > 0 && HasNextRoom () && nextRoom.CanSinnerArrive ()) {
			Sinner sinnerToMove = sinnersList [0];
			sinnersList.Remove(sinnerToMove);
			sinnerToMove.MoveToTarget(this.transform.position + new Vector3(roomWidth/2 + sinnerWidth/2, 0, 0), WalkOutsideCallBack);
			nextRoom.ReserveSinnerPlace();
			RearangeSinnersPosition();
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
