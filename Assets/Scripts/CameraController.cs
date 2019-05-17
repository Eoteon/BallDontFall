using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
	public const float Y_ANGLE_MIN = 10f;
	public const float Y_ANGLE_MAX = 50f;

	public Transform target;
	public Vector3 offset;
	public bool useOffsetVals;
	public float rotationSpeed;
	public Transform pivot;

	void Start() {
		if (!useOffsetVals) {
			offset = target.position - transform.position;
		}

		pivot.transform.position = target.transform.position;
		pivot.transform.parent = target.transform;

		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update() {
		target.Rotate(Input.GetAxis("Mouse Y") * -rotationSpeed, Input.GetAxis("Mouse X") * rotationSpeed, 0);
	

		float desiredYAngle = target.eulerAngles.y;
		float desiredXAngle = target.eulerAngles.x;
		Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
		transform.position = target.position - (rotation * offset);

		transform.LookAt(target);
	}
}
