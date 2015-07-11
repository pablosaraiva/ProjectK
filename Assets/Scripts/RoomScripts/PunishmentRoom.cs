using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;



public class PunishmentRoom : Room {
	
	[Serializable]
	public class SinEntry{
		public Sin.Type type;
		public int amount;
	}

	public SinEntry[] punishments;

	public int roomCost;
	

	private Sinner sinner;

	private float timeCounter = 0;
	void Update () {
		if (sinner != null) {
			timeCounter += Time.deltaTime;
			
			if(timeCounter>=2 && HasNextRoom() && nextRoom.CanSinnerArrive()){
				Punish();
				nextRoom.OnSinnerArive(sinner);
				timeCounter = 0;
				sinner = null;
			}
		}
	}

	public virtual void Punish(){
		foreach(SinEntry se in punishments){
			foreach(Sin sin in sinner.Sins){
				if(se.type == sin.SinType){
					sinner.IncreasePointsOfPunishment(sin.ReduceSinAmount(se.amount));
				}
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
