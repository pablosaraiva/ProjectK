using UnityEngine;
using System.Collections;

public class ContinueButtonScript : MonoBehaviour {

	public void continueGame() {
		Application.LoadLevel (1);
		PlayerPrefs.SetInt ("LoadGame", 1);
	}

}
