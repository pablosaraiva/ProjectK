using UnityEngine;
using System.Collections;

public class DeskRoom : Room {

	Sinner sinner;

	private float timeCounter = 0;
	void Update () {
		if (sinner != null) {
			timeCounter += Time.deltaTime;

			if(timeCounter>=2 && HasNextRoom()){
				nextRoom.OnSinnerArive(sinner);
				sinner = null;
			}
		}
	}

	public override bool HasNextRoom ()
	{
		return false;
	}

	public override bool CanSinnerArrive ()
	{
		return this.sinner == null && !reserved;
	}

	public override bool CanSetNextRoom ()
	{
		return false;
	}

	public override void OnSinnerArive (Sinner sinner)
	{
		reserved = false;
		this.sinner = sinner;
		//TODO fix entry position
		Vector3 startPos = new Vector3 (this.transform.position.x - (roomWidth/2 + sinnerWidth/2), this.transform.position.y, 0);
		sinner.transform.position = startPos;
		sinner.MoveToTarget(this.transform.position, null);

	}

}
