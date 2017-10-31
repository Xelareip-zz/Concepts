using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceBall : MonoBehaviour
{
	public BalancePlayer player;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Wall")
		{
			player.EndGame();
			Destroy(gameObject);
		}
	}
}
