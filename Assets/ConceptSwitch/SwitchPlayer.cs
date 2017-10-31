using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour
{
	private static SwitchPlayer instance;
	public static SwitchPlayer Instance
	{
		get
		{
			return instance;
		}
	}

	public Color[] colors = new Color[2];
	public int currentColorIdx;
	public Color currentColor
	{
		get
		{
			return colors[currentColorIdx];
		}
	}

	public GameObject endGame;

	public SpriteRenderer sprite;

	void Awake()
	{
		instance = this;
	}

	private void Switch()
	{
		currentColorIdx = 1 - currentColorIdx;
		sprite.color = currentColor;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Switch();
		}
	}

	public void EndGame()
	{
		endGame.SetActive(true);
	}

	void OnCollisionEnter(Collision coll)
	{
	}
}
