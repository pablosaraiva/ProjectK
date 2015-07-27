using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SinnerScript : MonoBehaviour {

	public Room currentRoom;
	private List<Sin> sins = new List<Sin>();
	
	private float maxSpeed = 20f;
	private float speed = 20f;

	private float bigScale = 2;

	private int pointsOfPunishment = 0;
	private Animator animator;

	public void Awake(){
		animator = transform.Find("RenderObject").GetComponent<Animator> ();
	}

	public void MoveToTarget(Vector3 target, Action<SinnerScript> onMoveFinishCallback){
		if (mouvementCoroutine != null) 
		{
			StopCoroutine (mouvementCoroutine);
		}
		mouvementCoroutine = StartCoroutine(Movement(target, onMoveFinishCallback));
	}

	Coroutine mouvementCoroutine = null;


	IEnumerator Movement (Vector3 target, Action<SinnerScript> onMoveFinishCallback)
	{

		animator.SetBool ("walking", true);
		while(Vector3.Distance(transform.position, target) > 0.05f)
		{
			///print(maxSpeed + "  " + Time.deltaTime + "   " +  maxSpeed*Time.deltaTime);
			transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);

			yield return null;
		}
		animator.SetBool ("walking", false);
		if (onMoveFinishCallback != null) {
			onMoveFinishCallback (this);
		}
	}

	public List<Sin> Sins {
		get {
			return this.sins;
		}
		set {
			sins = value;
		}
	}

	public int PointsOfPunishment {
		get {
			return this.pointsOfPunishment;
		}
	}

	public void IncreasePointsOfPunishment(int amount){
		pointsOfPunishment += amount;
	}

	public void SetBig(bool big){
		if (big) {
			animator.SetTrigger ("big");
			transform.localScale = new Vector3(bigScale,bigScale,bigScale);
			speed = maxSpeed * bigScale;
		} else {
			animator.SetTrigger ("small");
			transform.localScale= new Vector3(1,1,1);
			speed = maxSpeed;
		}
	}
}
