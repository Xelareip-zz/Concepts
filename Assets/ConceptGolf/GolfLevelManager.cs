using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfLevelManager : MonoBehaviour
{
	private static GolfLevelManager instance;
	public static GolfLevelManager Instance
	{
		get
		{
			return instance;
		}
	}

	public List<GameObject> possibleLevels;

	public float holesOffset;

	public int simultaneousLevels;
	public List<GolfLevel> levels;
	public GolfLevel currentLevel
	{
		get
		{
			return levels[0];
		}
	}

	public bool resetSpeed;

	void Awake()
	{
		instance = this;
		SpawnHoles();
		currentLevel.Activate();
	}

	void AddLevel()
	{
		GameObject newLevelObj = Instantiate<GameObject>(possibleLevels[Random.Range(0, possibleLevels.Count)], levels[levels.Count - 1].transform.position + Vector3.down * holesOffset, Quaternion.identity);
		GolfLevel newLevel = newLevelObj.GetComponentInChildren<GolfLevel>();
		levels.Add(newLevel);
	}

	public void SpawnHoles()
	{
		while (levels.Count <= simultaneousLevels)
		{
			AddLevel();
		}
	}

	public void NextHole()
	{
		SpawnHoles();

		Destroy(currentLevel.gameObject, 0.5f);

		levels.RemoveAt(0);
		currentLevel.Activate();
		SpawnHoles();
	}
}
