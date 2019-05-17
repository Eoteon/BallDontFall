using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	private CharacterController controller;

	public float moveSpeed = 100f;
	public float jumpHeight = 10f;

	private Vector3 moveDirection;

	void Start() {
		controller = GetComponent<CharacterController>();
	}

	void Update() {
		moveDirection = ((transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"))) * moveSpeed;
		moveDirection = moveDirection.normalized;

		if (controller.isGrounded) {
			moveDirection.y = 0f;
			if (Input.GetButtonDown("Jump")) {
				moveDirection.y = jumpHeight;
			}
		}

		moveDirection.y += Physics.gravity.y * 1.5f;
		controller.Move(moveDirection * Time.deltaTime);
		updateCanvasText();
	}

	void updateCanvasText() {
		Text ballInfoText = GameObject.FindWithTag("BallInfoText").GetComponent<Text>();
		ballInfoText.text = "\nPos(x,y,z): (" +  transform.position.x + "," +  transform.position.y + "," +  transform.position.z + ")";
	}

}
