using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GolfCamera : MonoBehaviour
{
	public Vector3 lookOffset;
	public Vector3 posOffset;
	public float moveSpeed;
	public bool updateInEditor;

	
	void Awake()
	{
		posOffset = transform.position;
	}
	
	void Update ()
	{
		if (Application.isPlaying || updateInEditor)
		{
			transform.position += moveSpeed * Time.deltaTime * (GolfLevelManager.Instance.currentLevel.transform.position + posOffset - transform.position);
			//transform.LookAt(GolfLevelManager.Instance.currentLevel.transform.position + lookOffset);
		}
	}
}
