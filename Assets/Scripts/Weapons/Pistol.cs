using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Pistol : Weapon
{

    public GameObject Hitting;

    public Transform RayOrigin;

    private float lastShotTimer;

	private void Update()
	{
        lastShotTimer += Time.deltaTime;
	}

	public override void Fire()
    {
        if (lastShotTimer < RateOfFire) 
        {
            
            return;
        }

        _SpriteAnimator.StartAnimation();
        _AudioSource.PlayOneShot(Sound);

        RaycastHit hit;

        
        if (Physics.Raycast(RayOrigin.position, RayOrigin.forward, out hit, 50, Mask,QueryTriggerInteraction.Ignore))
        {
            Hitting = hit.transform.gameObject;
            if (hit.transform.GetComponent<EnemyBase>())
            {
                EnemyBase enemy = hit.transform.GetComponent<EnemyBase>();
                enemy.TakeDamage((int)Damage);
            }
        }
        lastShotTimer = 0;
    }

	public override void Empty()
    {
        _EmptyAnimator.StartAnimation();
        _AudioSource.PlayOneShot(EmptySound);
    }
}
