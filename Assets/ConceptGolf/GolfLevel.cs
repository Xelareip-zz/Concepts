using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfLevel : MonoBehaviour
{
	public Transform startPos;

	public TriggerSignal holeSignal;

	public float deathLevel;

	public int levelScore;

	void Awake()
	{
		levelScore = 0;
		holeSignal.collisionEnter += HoleSignal;
	}

	private void HoleSignal(Collider obj)
	{
		if (obj.gameObject == GolfBall.Instance.gameObject)
		{
			HolePassed();
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		if (startPos != null)
		{
			Gizmos.DrawWireSphere(startPos.position, 0.2f);
		}
	}

	public void Activate()
	{
		levelScore = 0;
		GolfBall.Instance.transform.position = startPos.position;
		Rigidbody ballRig = GolfBall.Instance.GetComponent<Rigidbody>();
		ballRig.velocity = Vector3.zero;
		ballRig.angularVelocity = Vector3.zero;
    }

	private void HolePassed()
	{
		ScoreManager.Instance.score++;
		holeSignal.collisionEnter -= HoleSignal;
		GolfLevelManager.Instance.NextHole();
	}
}
