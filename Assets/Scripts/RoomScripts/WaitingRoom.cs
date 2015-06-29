using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WaitingRoom : Room {

	private int capacity = 4;

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
		if (timeCounter >= 5 && sinnersList.Count > 0) {
			if (HasNextRoom() && nextRoom.CanSinnerArrive()) {
				Sinner sinnerToMove = sinnersList [0];
				sinnersList.RemoveAt (0);
				nextRoom.OnSinnerArive (sinnerToMove);
				timeCounter = 0;
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
		return sinnersList.Count <= capacity;
	}

	public override void OnSinnerArive (Sinner sinner) {
		sinnersList.Add (sinner);
		//TODO fix entry position
		sinner.transform.position = this.transform.position;
	}

}
