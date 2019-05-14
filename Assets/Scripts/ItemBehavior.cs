using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour {

	public float respawnTimer = 5f;

	// Update is called once per frame
	void Update() {
		transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}

	public void startRespawn() {
		Invoke("respawnItem", respawnTimer);
	}

	void respawnItem() {
		gameObject.SetActive(true);
	}
}
