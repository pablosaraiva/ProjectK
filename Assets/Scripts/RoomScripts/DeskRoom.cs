﻿using UnityEngine;
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
		return this.sinner == null;
	}

	public override bool CanSetNextRoom ()
	{
		return false;
	}

	public override void OnSinnerArive (Sinner sinner)
	{
		this.sinner = sinner;
		//TODO fix entry position
		sinner.transform.position = this.transform.position;

	}
}
