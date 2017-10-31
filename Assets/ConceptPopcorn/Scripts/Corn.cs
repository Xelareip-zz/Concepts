using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : MonoBehaviour
{
	public GameObject raw;
	public GameObject popped;
	public Rigidbody2D rig;
	public LineRenderer lineRenderer;

	public float popForce;

	public Vector3 direction;
	public float lifetime;

	void Start()
	{
		PopcornPlayer.Instance.RegisterRawCorn(this);
	}

	void Update()
	{
		if (lifetime >= 0.0f && lifetime < Time.deltaTime)
		{
			if (transform.position.y > PopcornPlayer.Instance.deathHeight)
			{
				PopcornPlayer.Instance.EndGame();
			}
			else
			{
				Pop();
			}
		}
		if (PopcornPlayer.Instance.GetNextCorn() == this)
		{
			lineRenderer.enabled = true;

			float angle = -Mathf.Clamp(PopcornPlayer.Instance.transform.position.x * 14, -90, 90);

			direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up;
			//direction.x = -direction.x;

			lineRenderer.SetPosition(0, transform.position);
			lineRenderer.SetPosition(1, transform.position + direction);
		}
		else
		{
			lineRenderer.enabled = false;
		}

		lifetime -= Time.deltaTime;

		if (lifetime < 0 && transform.position.y < PopcornPlayer.Instance.transform.position.y)
		{
			Destroy(gameObject);
			++ScoreManager.Instance.score;
		}
	}

	void Pop()
	{
		PopcornPlayer.Instance.UnregisterRawCorn(this);
		raw.SetActive(false);
		popped.SetActive(true);
		
		rig.AddForce(direction * popForce, ForceMode2D.Impulse);
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

	void OnDrawGizmos()
	{
		if (raw.activeSelf)
		{
			Vector3 direction = (transform.position - PopcornPlayer.Instance.transform.position).normalized;

			Gizmos.DrawLine(transform.position, transform.position + direction);
		}
	}
}
