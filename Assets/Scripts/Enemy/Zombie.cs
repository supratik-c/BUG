using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : EnemyBase
{

	public GameObject Gib;

	private float LastAttack;

	// Start is called before the first frame update
	void Start()
    {
		Init();
		Health = 100;
    }

	private void Update()
	{

		LastAttack += Time.deltaTime;
		if (nav.PlayerTransform != null && nav.PlayerDistance <= AttackRange)
		{
			Attack();
		}
	}

	public override void Attack()
	{
		
		if (LastAttack < AttackSpeed) 
		{
			return;
		}

		
		Player p = nav.PlayerTransform.GetComponent<Player>();
		p.TakeDamage(Damage);
		LastAttack = 0;
		//throw new System.NotImplementedException();
	}

	public override void Die()
	{
		GameObject gib = Instantiate(Gib);
		gib.transform.position = transform.position;

		GameObject tmpAudio = new GameObject();
		tmpAudio.transform.position = transform.position;
		AudioSource tmpAS = tmpAudio.AddComponent<AudioSource>();
		tmpAS.clip = (DeathSounds[Random.Range(0, DeathSounds.Count)]);
		tmpAS.Play();

		Destroy(tmpAudio, 10);

		Destroy(gameObject);
		Destroy(gib, 5);

		GM.MinusEnemy();

		// Chance to drop a health pickup on death
		if (Random.Range(0, 100) < 5)
        {

        }
		//throw new System.NotImplementedException();
		Drop();
	}

	public override void TakeDamage(int damage)
	{
		Health -= damage;

		if (Health <= 0 && !Dead) 
		{
			Dead = true;
			Die();
			return;
		}

		_AudioSource.PlayOneShot(HurtSounds[Random.Range(0,HurtSounds.Count)]);
	}

	public override void OnTriggerEnter()
	{
		//throw new System.NotImplementedException();
	}

	public override void OnTriggerExit()
	{
		//throw new System.NotImplementedException();
	}
}
