using UnityEngine;
using System.Collections;

public class ExitRoom : Room {

	SinnerScript sinner;


	void Update () {

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

	public override void OnSinnerArive (SinnerScript sinner)
	{
		reserved = false;
		this.sinner = sinner;

		Vector3 startPos = new Vector3 (this.transform.position.x - (roomWidth/2 + sinnerWidth/2), this.transform.position.y, 0);
		sinner.transform.position = startPos;
		sinner.MoveToTarget(this.transform.position, SinnerArriveOnMiddleOfRoom);
	}

	public void SinnerArriveOnMiddleOfRoom(SinnerScript sinner){
		Animator animator = sinner.transform.Find("RenderObject").GetComponent<Animator> ();

		animator.SetTrigger ("gone");

		StartCoroutine (GoneAnimationController(animator));
		GameManagerScript.instance.AddSinPoints (sinner.PointsOfPunishment);
	}

	private IEnumerator GoneAnimationController(Animator animator){
		while (!animator.GetCurrentAnimatorStateInfo (0).IsName("Done")) {
			yield return null;
		}
		Destroy (sinner.gameObject);
		this.reserved = false;
		this.sinner = null;
		yield return null;
	}
}
