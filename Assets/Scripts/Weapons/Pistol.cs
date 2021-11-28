using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pistol : Weapon
{

    public GameObject Hitting;

    public Transform RayOrigin;

    public override void Fire()
    {
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

    }

	public override void Empty()
    {
        _SpriteAnimator.StartAnimation();
        _AudioSource.PlayOneShot(EmptySound);
    }
}
