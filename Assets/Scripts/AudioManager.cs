using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	[HideInInspector]
	public static AudioManager instance = null;

	public AudioSource musicAudioSource;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		
		DontDestroyOnLoad (gameObject);
	}




}
