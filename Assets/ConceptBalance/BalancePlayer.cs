using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancePlayer : MonoBehaviour
{
	public GameObject endGame;
	public float forceValue;
	public Rigidbody2D rig;
	public ConstantForce2D force;

	public float speedBounds;

	void Update ()
	{
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
			else if (Input.GetTouch(inputIdx).position.x > Screen.width / 2)
			{
				pushRight = true;
			}
        }

		if (pushLeft)
		{
			rig.AddTorque(forceValue, ForceMode2D.Force);
		}
		if (pushRight)
		{
			rig.AddTorque(-forceValue, ForceMode2D.Force);
		}
	}

	void FixedUpdate()
	{
		rig.velocity = Vector3.ClampMagnitude(rig.velocity, speedBounds);
	}

	public void EndGame()
	{
		endGame.SetActive(true);
	}
}
