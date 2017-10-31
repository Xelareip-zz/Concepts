using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomWave : MonoBehaviour
{
	public float targetSize;
	public float speed;
	public float damage;

	void Update()
	{
		if (transform.localScale.x >= targetSize)
		{
			Destroy(gameObject);
		}

		transform.localScale = Vector3.one * (transform.localScale.x + Time.deltaTime * speed);
	}

}
