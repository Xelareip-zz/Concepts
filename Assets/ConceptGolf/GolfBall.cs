using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
	private static GolfBall instance;
	public static GolfBall Instance
	{
		get
		{
			return instance;
		}
	}

	void Awake()
	{
		instance = this;
	}
}
