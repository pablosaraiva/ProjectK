using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Sinner : MonoBehaviour {

	public Room currentRoom;
	private List<Sin> sins = new List<Sin>();
	
	private float maxSpeed = 20f;

	private int pointsOfPunishment = 0;

	public void MoveToTarget(Vector3 target, Action<Sinner> onMoveFinishCallback){
		if(mouvementCoroutine!=null)
			StopCoroutine (mouvementCoroutine);
		mouvementCoroutine = StartCoroutine(Movement(target, onMoveFinishCallback));

	}

	Coroutine mouvementCoroutine = null;


	IEnumerator Movement (Vector3 target, Action<Sinner> onMoveFinishCallback)
	{
		Animator animator = transform.Find("RenderObject").GetComponent<Animator> ();
		animator.SetBool ("walking", true);
		while(Vector3.Distance(transform.position, target) > 0.05f)
		{
			///print(maxSpeed + "  " + Time.deltaTime + "   " +  maxSpeed*Time.deltaTime);
			transform.position = Vector3.MoveTowards(transform.position, target, maxSpeed*Time.deltaTime);

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

	int PointsOfPunishment {
		get {
			return this.pointsOfPunishment;
		}
	}

	public void IncreasePointsOfPunishment(int amount){
		pointsOfPunishment += amount;
	}
}
