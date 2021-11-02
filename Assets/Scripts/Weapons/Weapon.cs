using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    abstract public void Fire();



}
