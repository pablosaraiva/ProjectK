using UnityEngine;
using System;
using System.Collections;

public class Sinner : MonoBehaviour {

	public Room currentRoom;
	public float sinPoints = 200;
	
	private float maxSpeed = 20f;

	/*
	public Vector3 Target
	{
		get{ return target; }set
		{
			target = value;

			StopCoroutine("Movement");
			StartCoroutine("Movement", target);
		}
	}*/

	public void MoveToTarget(Vector3 target, Action<Sinner> onMoveFinishCallback){
		this.target = target;
		if(mouvementCoroutine!=null)
			StopCoroutine (mouvementCoroutine);
		mouvementCoroutine = StartCoroutine(Movement(target, onMoveFinishCallback));
			

	}

	Coroutine mouvementCoroutine = null;

	private Vector3 target;


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
}
