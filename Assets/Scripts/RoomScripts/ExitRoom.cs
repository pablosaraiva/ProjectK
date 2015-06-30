using UnityEngine;
using System.Collections;

public class ExitRoom : Room {

	Sinner sinner;

	private float timeCounter = 0;
	void Update () {
		if (sinner != null) {

		}
	}

	public override bool HasNextRoom ()
	{
		return false;
	}

	public override bool CanSinnerArrive ()
	{
		return this.sinner == null  && !reserved;
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
		sinner.transform.position = transform.position - new Vector3(roomWidth/2 + sinnerWidth/2, 0, 0);

		sinner.MoveToTarget (this.transform.position, ArriveOnMiddleCallback);
	}

	public void ArriveOnMiddleCallback(Sinner sinner){
		//TODO play the animation of 'gone', than kill the instance, and calculate the points? 
	}
}
