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

	public void Init()
	{
		Agent = GetComponent<NavMeshAgent>();
		Agent.speed = MovementSpeed;
		nav = GetComponent<AgentNavigation>();
		_AudioSource = GetComponent<AudioSource>();
		GM = FindObjectOfType<GameManager>();
	}

	abstract public void Attack();
    abstract public void TakeDamage(int damage);
    abstract public void Die();

	abstract public void OnTriggerEnter();
	abstract public void OnTriggerExit();
}
