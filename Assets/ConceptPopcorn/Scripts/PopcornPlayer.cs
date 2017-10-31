using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornPlayer : MonoBehaviour
{
	private static PopcornPlayer instance;
	public static PopcornPlayer Instance
	{
		get
		{
			return instance;
		}
	}

	public GameObject endGame;
	public float deathHeight;
	public LineRenderer deathLine;

	public List<Corn> rawCornList;
	
	void Awake()
	{
		rawCornList = new List<Corn>();
		instance = this;
	}

	void Update()
	{
		deathLine.SetPosition(0, new Vector3(-5, deathHeight, 0.0f));
		deathLine.SetPosition(1, new Vector3(5, deathHeight, 0.0f));

		Vector3 inputPos;

		if (Input.touchCount != 0)
		{
			inputPos = Input.GetTouch(0).position;
		}
		else
		{
			inputPos = Input.mousePosition;
		}

		transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Wall")
		{
			endGame.SetActive(true);
			Destroy(gameObject);
		}
	}

	public void RegisterRawCorn(Corn corn)
	{
		rawCornList.Add(corn);
		rawCornList.Sort((x, y) => x.lifetime < y.lifetime ? -1 : 1);
	}

	public void UnregisterRawCorn(Corn corn)
	{
		rawCornList.Remove(corn);
	}

	public Corn GetNextCorn()
	{
		return rawCornList[0];
	}

	public void EndGame()
	{
		endGame.SetActive(true);
	}
}
