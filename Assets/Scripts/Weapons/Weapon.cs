using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
abstract public class Weapon : MonoBehaviour
{
    public float Damage;
    public SpriteAnimation _SpriteAnimator;
    public AudioSource _AudioSource;
    public float DispersionAngle;
    public int ProjectileCount;
    public float RateOfFire;
    public float ProjectileCountModifier;
    public float FailRate;
    public AudioClip Sound;
    public AudioClip EmptySound;
    public int ammoDivisor;
    public LayerMask Mask;

    public Transform PlayerTransform;

	private void Awake()
	{
        PlayerTransform = Camera.main.transform;
	}


	abstract public void Fire();
    abstract public void Empty();


}
