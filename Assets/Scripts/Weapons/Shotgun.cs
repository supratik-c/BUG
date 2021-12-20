using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;


[System.Serializable]
public class Shotgun : Weapon
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

        for (int i = 0; i < ProjectileCount; i++) 
        {
            //Vector3 direction = RayOrigin.forward + new Vector3(Random.Range(-DispersionAngle,DispersionAngle),0,0);
            Vector3 direction = Quaternion.Euler(Random.Range(-DispersionAngle, DispersionAngle), Random.Range(-DispersionAngle, DispersionAngle), Random.Range(-DispersionAngle, DispersionAngle)) * RayOrigin.forward;
            //Debug.DrawLine(RayOrigin.position, direction * 50, Color.magenta, 5);
            Debug.DrawRay(RayOrigin.position, direction * 100, Color.magenta, 5);
            RaycastHit hit;

            if (Physics.Raycast(RayOrigin.position, direction, out hit, 100, Mask,QueryTriggerInteraction.Ignore))
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
