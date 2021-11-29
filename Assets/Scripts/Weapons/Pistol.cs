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



    public void Awake()
    {
        _SpriteAnimator.SetSprites(fireSprites);
    }

    private void Update()
	{
        lastShotTimer += Time.deltaTime;
	}

	public override bool Fire()
    {
        if (lastShotTimer < RateOfFire) 
        {
            
            return false;
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

                Destroy(Instantiate(HitBlood,hit.point,Quaternion.LookRotation(hit.normal,Vector3.up),hit.transform),5);
            }
            else 
            {
                Destroy(Instantiate(HitDebris, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up), hit.transform), 5);
            }
        }
        lastShotTimer = 0;
        return true;
    }

	public override void Empty()
    {
        _EmptyAnimator.StartAnimation();
        _AudioSource.PlayOneShot(EmptySound);
    }
}
