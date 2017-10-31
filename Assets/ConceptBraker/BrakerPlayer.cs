using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakerPlayer : MonoBehaviour
{
	public GameObject endGame;
	public float forceValue;
	public Rigidbody2D rig;
	public ConstantForce2D force;

	public float speedBounds;

	void Update () {

		if (Input.touchCount != 0 || Input.GetMouseButton(0))
		{
			force.relativeForce = new Vector2(0, -forceValue);
		}
		else
		{
			force.relativeForce = new Vector2(0, forceValue);
		}
	}

	void FixedUpdate()
	{
		rig.velocity = Vector3.ClampMagnitude(rig.velocity, speedBounds);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Wall")
		{
			endGame.SetActive(true);
			Destroy(gameObject);
		}
	}
}
