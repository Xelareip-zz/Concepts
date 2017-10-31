using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomPlayer : MonoBehaviour
{
	public GameObject wavePrefab;
	public float chargeSpeed;

	public bool charging;
	public float power;


	void Awake()
	{
		charging = false;
	}

	void Update()
	{
		bool wasCharging = charging;
		if (Input.GetMouseButton(0))
		{
			charging = true;
			power += Time.deltaTime * chargeSpeed;
		}
		else
		{
			charging = false;
		}

		if (!charging && wasCharging)
		{
			Attack();
		}
	}

	private void Attack()
	{
		GameObject waveObj = Instantiate(wavePrefab, transform.position, Quaternion.identity);
		BoomWave wave = waveObj.GetComponent<BoomWave>();
		wave.targetSize = power;
		power = 0.0f;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, power / 4.0f);
	}
}
