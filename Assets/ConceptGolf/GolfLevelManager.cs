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
	public GolfLevel currentLevel;

	void Awake()
	{
		instance = this;
		currentLevel.Activate();
	}

	public void NextHole()
	{
		GameObject newLevelObj = Instantiate<GameObject>(possibleLevels[Random.Range(0, possibleLevels.Count)], currentLevel.transform.position + Vector3.down * holesOffset, Quaternion.identity);
		GolfLevel newLevel = newLevelObj.GetComponentInChildren<GolfLevel>();

		Destroy(currentLevel.gameObject, 2.0f);

		currentLevel = newLevel;
		newLevel.Activate();
	}
}
