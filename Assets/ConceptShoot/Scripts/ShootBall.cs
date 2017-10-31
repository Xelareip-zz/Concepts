using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBall : MonoBehaviour
{
	void Update()
	{
		CheckInCam();
	}

	void CheckInCam()
	{
		Camera cam = Camera.main;
		Vector3 relativePos = transform.position - cam.transform.position;

		if (Mathf.Abs(relativePos.x) > cam.orthographicSize / cam.aspect ||
			Mathf.Abs(relativePos.y) > cam.orthographicSize)
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.collider.tag == "Player")
		{
			++ScoreManager.Instance.score;
			Destroy(gameObject);
		}
	}
}
