using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPlayer : MonoBehaviour
{
	public GameObject endGame;
	public CameraFollow cameraFollow;
	public float forceValueUp;
	public float forceValueSide;
	public Rigidbody2D rig;

	public bool touchingLeft;
	public bool touchingRight;

	public float timeStart;

	void Awake()
	{
		timeStart = Time.time;
		cameraFollow.canMove = false;
	}

	void Update()
	{
		bool pushLeft = false;
		bool pushRight = false;

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			pushLeft = pushRight = true;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			pushLeft = pushRight = true;
		}

		for (int inputIdx = 0; inputIdx < Input.touchCount; ++inputIdx)
		{
			if (Input.GetTouch(inputIdx).phase == TouchPhase.Began)
			{
				pushLeft = pushRight = true;
				break;
			}
		}

		if (touchingLeft && pushRight)
		{
			rig.AddForce(new Vector2(forceValueSide, forceValueUp));
			rig.gravityScale = 1.0f;
			cameraFollow.canMove = false;
			touchingLeft = false;
		}
		else if (touchingRight && pushLeft)
		{
			rig.AddForce(new Vector2(-forceValueSide, forceValueUp));
			rig.gravityScale = 1.0f;
			cameraFollow.canMove = false;
			touchingRight = false;
		}
	}

	public void EndGame()
	{
		endGame.SetActive(true);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Platform")
		{
			Debug.Log("Hit");
			rig.velocity = Vector3.zero;
			rig.gravityScale = 0f;
			touchingLeft = coll.contacts[0].point.x < transform.position.x;
			touchingRight = coll.contacts[0].point.x > transform.position.x;
			cameraFollow.canMove = true;
			++ScoreManager.Instance.score;
        }
		else if (coll.gameObject.tag == "Wall")
		{
			endGame.SetActive(true);
			Destroy(gameObject);
		}
	}
}
