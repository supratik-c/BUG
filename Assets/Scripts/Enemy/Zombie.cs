using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : EnemyBase
{

	public GameObject Gib;


	// Start is called before the first frame update
	void Start()
    {
		Init();
		Health = 100;
    }

	public override void Attack()
	{
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

		// Chance to drop a health pickup on death
		if (Random.Range(0, 100) < 5)
        {

        }
		
		//throw new System.NotImplementedException();
	}

	public override void TakeDamage(int damage)
	{
		Health -= damage;

		if (Health <= 0) 
		{
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
