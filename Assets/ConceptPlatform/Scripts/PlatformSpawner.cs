using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
	public GameObject platformModel;
	public GameObject player;
	public List<GameObject> platforms = new List<GameObject>();
	public int maxCount;
	public float distance;
	public Vector2 boundaries;

	void Update()
	{
		for (int idx = 0; idx < platforms.Count; ++idx)
		{
			if (platforms[idx] == null)
			{
				platforms.RemoveAt(idx);
				--idx;
			}
			else if (platforms[idx].transform.position.y < transform.position.y - 10)
			{
				Destroy(platforms[idx]);
				platforms.RemoveAt(idx);
				--idx;
			}
		}

		while (platforms.Count < maxCount)
		{
			GameObject newPlat = null;
			if (platforms.Count != 0)
			{
				newPlat = Instantiate<GameObject>(platformModel, new Vector3(Random.Range(boundaries.x, boundaries.y), platforms[platforms.Count - 1].transform.position.y + distance, player.transform.position.z), Quaternion.identity);
			}
			else
			{
				newPlat = Instantiate<GameObject>(platformModel, new Vector3(Random.Range(boundaries.x, boundaries.y), player.transform.position.y + distance, player.transform.position.z), Quaternion.identity);
			}
			platforms.Add(newPlat);
		}
	}
}
