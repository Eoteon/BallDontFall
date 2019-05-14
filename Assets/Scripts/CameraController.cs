using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
	public float minFov = 15f;
	public float maxFov = 150f;
	public float fovSensitivity = 10f;

	public GameObject player;
	private Vector3 offset;
	int fovZoomCount = 0;

    // Start is called before the first frame update
    void Start()
    {
		offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		updateFov();
		transform.position = player.transform.position + offset;
    }

	void updateFov() {
		float newFov = Camera.main.fieldOfView + Input.GetAxis("Mouse ScrollWheel") * fovSensitivity * -1;
		if (player.GetComponent<PlayerController>().isBoosting && fovZoomCount < 100) {
			newFov += 0.1f;
			fovZoomCount++;
		} else if (fovZoomCount > 0) {
			newFov -= 0.1f;
			fovZoomCount--;
		}
		Camera.main.fieldOfView = Mathf.Clamp(newFov, minFov, maxFov);
	}
}
