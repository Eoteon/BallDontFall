using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	private Rigidbody bulletRb;
	private float bulletSpawnTime;

	public float bulletLife = 3f;
	public float bulletSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
		bulletRb = GetComponent<Rigidbody>();
		bulletSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
		checkBulletLife();
    }

	void LateUpdate() {
		bulletRb.AddForce(transform.forward * bulletSpeed);
	}

	void checkBulletLife() {
		if (Time.time - bulletSpawnTime > bulletLife) {
			Destroy(this.gameObject);
		}
	}
}
