
using UnityEngine;
using System.Collections;

public class CamController : MonoBehaviour {

	private float minZoom = 50;
	private float maxZoom = 700;
	private float scrollSensitivity = 50F;

	private float mouseSensitivity = 1;
	private Vector3 lastPosition;
	
	// Update is called once per frame
	void Update () {
		float zoom = Input.GetAxis ("Mouse ScrollWheel") * scrollSensitivity * -1;
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

		if (transform.position.x - (camera.orthographicSize) < -665) {
			print(camera.orthographicSize/2);
			transform.position = new Vector3 (-665 + (camera.orthographicSize), transform.position.y, transform.position.z);
		}
	}


}
