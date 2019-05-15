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
	private Vector2 rotation;
	private int fovZoomCount = 0;

    // Start is called before the first frame update
    void Start()
    {
		offset = transform.position - player.transform.position;
		rotation = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
		updateFov();
		transform.position = player.transform.position + offset;
		updateVerticalRotation();
		//rotation.y += Input.GetAxis("Mouse X");
		//rotation.x -= Input.GetAxis("Mouse Y");
		//transform.eulerAngles = (Vector2)rotation * 3;
		updateDebugText();
    }

	void updateVerticalRotation() {
		float mouseYMovement = Input.GetAxis("Mouse Y") / 20f;
		Quaternion newRotation = player.transform.rotation;
		newRotation.x = transform.rotation.x - mouseYMovement;
		newRotation.z = transform.rotation.z;
		transform.rotation = newRotation;
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

	void updateDebugText() {
		Text cameraInfoText = GameObject.FindGameObjectWithTag("CameraInfoText").GetComponent<Text>();
		cameraInfoText.text = "Rotation(x,y,z): (" + transform.rotation.x + "," + transform.rotation.y + "," + transform.rotation.z + ")";
	}
}
