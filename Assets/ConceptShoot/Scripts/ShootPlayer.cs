using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayer : MonoBehaviour
{
	public GameObject endGame;
	

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Wall")
		{
			endGame.SetActive(true);
			Destroy(gameObject);
		}
	}
}
