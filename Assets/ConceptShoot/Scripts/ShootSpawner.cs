using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpawner : MonoBehaviour
{
	public GameObject ballModel;
	public GameObject player;
	public Vector3 direction;

	public float initialSpeed;

	public float shootFrequency;
	private float lastShot;

	void Start()
	{
		lastShot = Time.time;
	}

	void Update()
	{
		Vector3 screenPos = Vector3.zero;
		for (int touchIdx = 0; touchIdx < Input.touchCount; ++touchIdx)
		{
			Touch touch = Input.GetTouch(touchIdx);
			if (touch.phase == TouchPhase.Began)
			{
				screenPos = touch.position;
				break;
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			screenPos = Input.mousePosition;
		}

		if (screenPos == Vector3.zero)
		{
			return;
		}
		direction = Camera.main.ScreenToWorldPoint(screenPos) - transform.position;
		direction.z = transform.position.z;
		
		if (lastShot + shootFrequency < Time.time)
		{
			GameObject ball = Instantiate(ballModel, transform.position, Quaternion.FromToRotation(Vector3.up, direction));
			ball.GetComponent<Rigidbody2D>().velocity = direction.normalized * initialSpeed;
			lastShot = Time.time;
		}
	}
}
