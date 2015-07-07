using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	[HideInInspector]
	public static GameManager instace = null;

	public BoardManager boardScript;
	public GameObject GAMEHudPrefab;

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
		GameObject HudManager = Instantiate (GAMEHudPrefab);
		HudManager.transform.Find ("NewPunishmentRoomButton").GetComponent<NewPunishmentRoomButton> ().boardManager = boardScript;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
