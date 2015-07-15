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
	

	private SinnerScript sinner;
	
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

	public override void OnSinnerArive (SinnerScript sinner)
	{
		reserved = false;
		this.sinner = sinner;
		
		Vector3 startPos = new Vector3 (this.transform.position.x - (roomWidth/2 + sinnerWidth/2), this.transform.position.y, 0);
		sinner.transform.position = startPos;
		sinner.MoveToTarget(this.transform.position, SinnerArriveOnMiddleOfRoom);
		sinner.currentRoom = this;
	}

	public void SinnerArriveOnMiddleOfRoom(SinnerScript sinner){
		//TODO wait a little, or implements new animation.
		Punish ();

		StartCoroutine (TryToSendNextRoom());
	}

	public IEnumerator TryToSendNextRoom(){
		while (!HasNextRoom() || !NextRoom.CanSinnerArrive()) {
			yield return null;
		}
		NextRoom.ReserveSinnerPlace ();
		sinner.MoveToTarget(this.transform.position + new Vector3(roomWidth/2 + sinnerWidth/2, 0, 0), WalkOutsideCallBack);
	}

	public override void WalkOutsideCallBack (SinnerScript sinner)
	{
		base.WalkOutsideCallBack (sinner);
		this.sinner = null;
		this.reserved = false;
	}
}
