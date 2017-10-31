using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackStartPlatform : MonoBehaviour
{
	void OnCollisionExit2D(Collision2D coll)
	{
		Destroy(gameObject);
	}
}
