using UnityEngine;
using System.Collections;

public class CamController : MonoBehaviour {

	private float minZoom = 100;
	private float maxZoom = 500;
	private float sensitivity = 20;

	private float mouseSensitivity = 1;
	private Vector3 lastPosition;
	
	// Update is called once per frame
	void Update () {
		float zoom = Input.GetAxis ("Mouse ScrollWheel") * sensitivity;
		//print (zoom);
		Camera camera = GetComponent<Camera> ();
		camera.orthographicSize = Mathf.Max(Mathf.Min(zoom + camera.orthographicSize, maxZoom), minZoom);

		if (Input.GetMouseButtonDown(0))
		{
			lastPosition = Input.mousePosition;
		}
		
		if (Input.GetMouseButton(0))
		{
			Vector3 delta = lastPosition - Input.mousePosition;
			transform.Translate(delta.x * mouseSensitivity, delta.y * mouseSensitivity, 0);
			lastPosition = Input.mousePosition;
		}
	}


}
