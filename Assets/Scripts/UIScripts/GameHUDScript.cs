using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameHUDScript : MonoBehaviour {
	public void SetSinPoints(int sinPoints) {
		Text sinPointsText = (Text) GameObject.FindWithTag ("SinPointsUI").GetComponent<Text>();
		sinPointsText.text = sinPoints.ToString ();
	}

}
