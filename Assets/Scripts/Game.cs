using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	public void SinnerLeaveRoom(Sinner sinner, Room origin) {
		origin.SinnerLeaveRoom (sinner);
	}

	void OnGUI() {

	}

}
