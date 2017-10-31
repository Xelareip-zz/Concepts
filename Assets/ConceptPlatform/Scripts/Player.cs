using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Rigidbody2D rig;
	public float angle;
	public float rotationSpeed;
	public float speed;

	public bool stopped;
	public LineRenderer lineRenderer;

	public GameObject endGame;

	void Start()
	{
		stopped = true;
	}

	void Update()
	{
		if (stopped && (Input.touchCount != 0 || Input.GetMouseButtonDown(0)))
		{
			stopped = false;
			rig.AddForce(new Vector2(Mathf.Cos(angle), Mathf.Abs(Mathf.Sin(angle))) * speed);
        }

		lineRenderer.enabled = stopped;
		if (stopped)
		{
			angle += rotationSpeed * Time.deltaTime;
			angle = angle % 360.0f;
			lineRenderer.SetPosition(1, new Vector3(Mathf.Cos(angle), Mathf.Abs(Mathf.Sin(angle)), transform.position.z));
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Platform")
		{
			Destroy(coll.gameObject);
			rig.velocity = Vector2.zero;
			stopped = true;
			++ScoreManager.Instance.score;
		}
		else if (coll.tag == "Wall")
		{
			endGame.SetActive(true);
			Destroy(gameObject);
		}
	}
}
