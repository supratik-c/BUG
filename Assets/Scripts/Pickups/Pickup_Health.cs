using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Health : PickUpBase
{
	public int Health_Give;

	public Transform PlayerTransform;

	private void Awake()
	{
		PlayerTransform = FindObjectOfType<Player>().transform;
	}

	private void Update()
	{
		Vector3 lookRot = (transform.position - PlayerTransform.position).normalized;
		lookRot.y = 0;
		Quaternion rotation = Quaternion.LookRotation(lookRot);
		transform.localRotation = rotation;
	}

	public override void PickUp()
	{
		player.RecieveHealing(Health_Give);
		Destroy(gameObject);
	}
}
