using UnityEngine;
using System.Collections;

public class PunishmentRoom : Room {

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

	public override bool CanSinnerArrive ()
	{
		return this.sinner == null  && !reserved;
	}

	public override bool CanSetNextRoom ()
	{
		return true;
	}

	public override void OnSinnerArive (Sinner sinner)
	{
		reserved = false;
		this.sinner = sinner;
		//TODO fix entry position
		sinner.transform.position = transform.position;
	}
}
