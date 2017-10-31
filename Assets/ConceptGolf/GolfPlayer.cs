using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfPlayer : MonoBehaviour
{
	public Rigidbody golfBall;

	public GameObject endGame;

	public Vector2 shootDistanceBounds;
	public float shootMultiplier;
	public bool canShoot;

	public Vector3 dragStart;
	public Vector3 currentDrag;

	public LineRenderer shootLine;

	public float velocityThreshold;
	public Vector3 oldVelocity;

	private bool GetInputDown()
	{
		return Input.GetMouseButtonDown(0) || (Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Began);
	}

	private bool GetInputStay()
	{
		return Input.GetMouseButton(0) || 
			(Input.touchCount != 0 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary));
	}

	private bool GetInputUp()
	{
		return Input.GetMouseButtonUp(0) ||
			(Input.touchCount != 0 && (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended));
	}

	private Vector2 GetInputPos()
	{
		if (Input.touchCount != 0)
		{
			return Input.GetTouch(0).position;
		}
		return Input.mousePosition;
	}

	void Update()
	{
		if (GolfBall.Instance.transform.position.y < GolfLevelManager.Instance.currentLevel.transform.position.y - GolfLevelManager.Instance.currentLevel.deathLevel)
		{
			EndGame();
		}

		if (golfBall.velocity.magnitude < velocityThreshold)
		{
			golfBall.velocity = Vector3.zero;
			golfBall.angularVelocity = Vector3.zero;
		}
		canShoot = golfBall.velocity == Vector3.zero;

		shootLine.enabled = canShoot;
		if (canShoot == false)
		{
			dragStart = Vector3.zero;
			currentDrag = Vector3.zero;
		}

		if (canShoot && oldVelocity != Vector3.zero && GolfLevelManager.Instance.currentLevel.levelScore > 0)
		{
			EndGame();
			shootLine.enabled = false;
			return;
		}

		if (GetInputDown())
		{
			Ray ray = Camera.main.ScreenPointToRay(GetInputPos());

			Plane plane = new Plane(Vector3.up, GolfLevelManager.Instance.currentLevel.transform.position);

			float hitDist = 0.0f;
			plane.Raycast(ray, out hitDist);
			
			dragStart = ray.GetPoint(hitDist);
		}
		else if (GetInputStay())
		{
			Ray ray = Camera.main.ScreenPointToRay(GetInputPos());

			Plane plane = new Plane(Vector3.up, GolfLevelManager.Instance.currentLevel.transform.position);

			float hitDist = 0.0f;
			plane.Raycast(ray, out hitDist);

			currentDrag = ray.GetPoint(hitDist);
		}

		Vector3 shootVector = (dragStart - currentDrag) * shootMultiplier;

		if (canShoot && shootVector.magnitude > shootDistanceBounds.x)
		{
			shootVector = shootVector.normalized * (shootVector.magnitude - shootDistanceBounds.x); 
			shootVector = Vector3.ClampMagnitude(shootVector, shootDistanceBounds.y);
            if (GetInputUp())
			{
				golfBall.velocity = shootVector;
				GolfLevelManager.Instance.currentLevel.levelScore++;
			}
			shootLine.enabled = true;
			shootLine.SetPosition(0, golfBall.transform.position);
			shootLine.SetPosition(1, golfBall.transform.position + shootVector);
		}
		else
		{
			shootLine.enabled = false;
		}
		oldVelocity = golfBall.velocity;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(currentDrag, dragStart);
	}

	void EndGame()
	{
		endGame.SetActive(true);
		enabled = false;
	}
}
