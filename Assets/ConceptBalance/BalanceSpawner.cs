using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceSpawner : MonoBehaviour
{
	public GameObject platformModel;
	public GameObject player;
	public List<GameObject> platforms = new List<GameObject>();
	public int maxCount;
	public float lastSpawn;
	public float delay;
	public float spawnHeight;
	public Vector2 boundaries;

	void Update()
	{
		if (player == null)
		{
			return;
		}
		if (Time.time - delay > lastSpawn)
		{
			GameObject newPlat = Instantiate<GameObject>(platformModel, new Vector3(Random.Range(boundaries.x, boundaries.y), spawnHeight, player.transform.position.z), Quaternion.identity);
			platforms.Add(newPlat);
			lastSpawn = Time.time;
		}
		for (int idx = 0; idx < platforms.Count; ++idx)
		{
			if (platforms[idx] == null)
			{
				platforms.RemoveAt(idx);
				--idx;
			}
			else if (platforms[idx].transform.position.y < player.transform.position.y - 3)
			{
				++ScoreManager.Instance.score;
				Destroy(platforms[idx]);
				platforms.RemoveAt(idx);
				--idx;
			}
		}
	}
}
