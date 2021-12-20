using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

abstract public class EnemyBase : MonoBehaviour
{
	[SerializeField]
    public int Health { get; set; }
	public float AttackSpeed;
	public float AttackRange;
    public int Damage;
    public float MovementSpeed;

	public NavMeshAgent Agent;

	public AgentNavigation nav;

	public List<AudioClip> HurtSounds = new List<AudioClip>();
	public List<AudioClip> DeathSounds = new List<AudioClip>();
	public List<AudioClip> AttackSounds = new List<AudioClip>();

	public GameManager GM;

	public AudioSource _AudioSource;

	public bool Dead = false;

	public List<GameObject> Pickups = new List<GameObject>();

	public Vector2 DropChance = Vector2.zero;

	public void Init()
	{
		Agent = GetComponent<NavMeshAgent>();
		Agent.speed = MovementSpeed;
		nav = GetComponent<AgentNavigation>();
		_AudioSource = GetComponent<AudioSource>();
		GM = FindObjectOfType<GameManager>();
		Dead = false;
	}

	abstract public void Attack();
    abstract public void TakeDamage(int damage);
    abstract public void Die();

	public void Drop() 
	{
		float Determiation = Random.Range(0,DropChance.y);

		Debug.Log($"determination = {Determiation} drop x = {DropChance.x}");

		if (Determiation <= DropChance.x) 
		{
			Instantiate(Pickups[Random.Range(0, Pickups.Count - 1)],transform.position,Quaternion.identity,null);
		}
	}

	abstract public void OnTriggerEnter();
	abstract public void OnTriggerExit();
}
