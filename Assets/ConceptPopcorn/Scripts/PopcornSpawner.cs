using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornSpawner : MonoBehaviour
{
	public GameObject cornModel;

	public Vector2 spawnDelay;
	public Vector2 spawnRange;

	public float nextSpawn;

	void Update()
	{
		nextSpawn -= Time.deltaTime;
		if (nextSpawn <= 0.0f)
		{
			Spawn();
			nextSpawn = Random.Range(spawnDelay.x, spawnDelay.y);
		}
	}

	void Spawn()
	{
		GameObject newCornObj = Instantiate(cornModel, new Vector3(Random.Range(spawnRange.x, spawnRange.y), transform.position.y, transform.position.z), Quaternion.identity);

		Corn newCorn = newCornObj.GetComponent<Corn>();
		
	}
}
