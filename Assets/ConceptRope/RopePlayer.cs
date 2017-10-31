using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePlayer : MonoBehaviour
{
	public Rigidbody rig;
	public float jumpSpeed;

	public GameObject endGame;

	public bool canJump;

	void Start()
	{
		ScoreManager.Instance.score = -1;
    }

	void Update()
	{
		if (canJump && Input.GetMouseButtonDown(0))
		{
			rig.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
			canJump = false;
		}
	}

	void OnCollisionEnter(Collision coll)
	{
		if (coll.gameObject.tag == "Ground")
		{
			canJump = true;
			++ScoreManager.Instance.score;
		}
		else if (coll.gameObject.tag == "Wall")
		{
			endGame.SetActive(true);
			Destroy(gameObject);
		}
	}
}
