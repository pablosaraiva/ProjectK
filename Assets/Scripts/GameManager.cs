using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instace = null;
	public BoardManager boardScript;

	// Use this for initialization
	void Awake () {
		//Singleton
		if (instace == null)
			instace = this;
		else if (instace != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	void InitGame(){
		boardScript.StetupScene ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
