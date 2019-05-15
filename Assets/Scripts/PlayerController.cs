using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Renderer renderer;
	private Rigidbody player;
	private List<GameObject> platformList;
	private int itemCount = -1;
	private float lastBoostTime;

	public float moveSpeed = 10f;
	public bool isBoosting;
	public GameObject bulletPrefab;

	void Start() {
		renderer = GetComponent<Renderer>();
		player = GetComponent<Rigidbody>();
		player.freezeRotation = true;
		platformList = new List<GameObject>();
		lastBoostTime = Time.time;
		incrementItemCount();
	}

	void Update() {
		updateCanvasText();
	}

	void LateUpdate() {
		movePlayer(0f);
		checkForFloor();
		handleShoot();
	}

	void movePlayer(float axisYMovement) {
		float axisXMovement = Input.GetAxis("Horizontal");
		float axisZMovement = Input.GetAxis("Vertical");

		if (Input.GetKey("space")) {
			axisYMovement += 1f;
			isBoosting = true;
		} else if (Time.time - lastBoostTime > 1){
			isBoosting = false;
			lastBoostTime = Time.time;
		}
		
		Vector3 movement = new Vector3(axisXMovement, axisYMovement, axisZMovement);
		player.AddForce(movement * moveSpeed, ForceMode.Acceleration);
		rotatePlayer();
	}

	void rotatePlayer() {
		float mouseXMovement = Input.GetAxis("Mouse X") / 20f;
		if (mouseXMovement != 0) {
			player.transform.rotation = new Quaternion(player.transform.rotation.x, player.transform.rotation.y + mouseXMovement, player.transform.rotation.z, player.transform.rotation.w);
		}
	}

	void checkForFloor() {
		if (player.transform.position.y <0.5f) {
			player.transform.position = new Vector3(player.transform.position.x, 0.5f, player.transform.position.z);
			
			if (platformList.Count == 3) {
				Destroy(platformList[0]);
				platformList.RemoveAt(0);
			}

			GameObject newPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
			newPlatform.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z);
			platformList.Add(newPlatform);
		}
	}

	void handleShoot() {
		if (Input.GetMouseButton(0)) {
			Instantiate(bulletPrefab, player.transform.position, player.transform.rotation);
		}
	}

	void OnTriggerEnter(Collider otherObject) {
		if (otherObject.gameObject.CompareTag("Item")) {
			handleItemInteraction(otherObject);
		} else if (otherObject.gameObject.CompareTag("JumpPlatform")) {
			handleJumpPlatform();
		}
	}

	void handleItemInteraction(Collider itemObject) {
		itemObject.gameObject.SetActive(false);
		ItemBehavior itemBehavior = itemObject.GetComponent<ItemBehavior>();
		itemBehavior.startRespawn();
		incrementItemCount();
	}

	void incrementItemCount() {
		itemCount++;
	}

	void handleJumpPlatform() {
		movePlayer(200f);
	}

	void updateCanvasText() {
		Text ballInfoText = GameObject.FindWithTag("BallInfoText").GetComponent<Text>();
		ballInfoText.text = "Velocity(x,y): (" + player.velocity.x + "," + player.velocity.y + ")"
			+ "\nPos(x,y,z): (" + player.transform.position.x + "," + player.transform.position.y + "," + player.transform.position.z + ")";
	}

}
