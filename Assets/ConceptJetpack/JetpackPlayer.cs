using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackPlayer : MonoBehaviour
{
	public GameObject endGame;
	public float forceValueUp;
	public float forceValueSide;
	public Rigidbody2D rig;

	public bool pushingLeft;
	public bool pushingRight;

	public bool touchingLeft;
	public bool touchingRight;

	public float timeStart;

	void Awake()
	{
		pushingLeft = false;
		pushingRight = false;
		timeStart = Time.time;
	}

	void Update()
	{
		ScoreManager.Instance.score = Mathf.FloorToInt(Time.time - timeStart);
		bool pushLeft = false;
		bool pushRight = false;

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			pushLeft = true;
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			pushRight = true;
		}

		for (int inputIdx = 0; inputIdx < Input.touchCount; ++inputIdx)
		{
			if (Input.GetTouch(inputIdx).position.x < Screen.width / 2)
			{
				pushLeft = true;
			}
			if (Input.GetTouch(inputIdx).position.x > Screen.width / 2)
			{
				pushRight = true;
			}
		}

		pushLeft &= !touchingLeft;
		pushRight &= !touchingRight;

		if (!pushingRight && !pushingLeft && (pushLeft || pushRight))
		{
			rig.gravityScale = 1.0f;
			//rig.isKinematic = false;
		}

		pushingRight &= pushRight;
		pushingLeft &= pushLeft;

		if (pushingLeft && pushLeft)
		{
			rig.AddForce(new Vector2(-forceValueSide, forceValueUp));
		}
		else if (pushingRight && pushRight)
		{
			rig.AddForce(new Vector2(forceValueSide, forceValueUp));
		}
		else if (pushLeft)
		{
			rig.AddForce(new Vector2(-forceValueSide, forceValueUp));
			pushingLeft = true;
			touchingRight = false;
		}
		else if (pushRight)
		{
			rig.AddForce(new Vector2(forceValueSide, forceValueUp));
			pushingRight = true;
			touchingLeft = false;
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
			rig.gravityScale = 0.01f;
			//rig.isKinematic = true;
			touchingLeft = coll.contacts[0].point.x < transform.position.x;
			touchingRight = coll.contacts[0].point.x > transform.position.x;
			pushingLeft = false;
			pushingRight = false;
		}
		else if (coll.gameObject.tag == "Wall")
		{
			endGame.SetActive(true);
			Destroy(gameObject);
		}
	}
}
