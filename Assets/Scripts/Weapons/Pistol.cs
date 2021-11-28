using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pistol : Weapon
{

    public override void Fire()
    {
        _SpriteAnimator.StartAnimation();
        _AudioSource.PlayOneShot(Sound);
    }

    public override void Empty()
    {
        _EmptyAnimator.StartAnimation();
        _AudioSource.PlayOneShot(EmptySound);
    }

}
