using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossStartPlatform : MonoBehaviour
{
	void OnCollisionExit2D(Collision2D coll)
	{
		Destroy(gameObject);
	}
}
