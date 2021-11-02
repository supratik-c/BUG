using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{


    private void Awake()
    {
        _AudioSource.clip = Sound;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }
    }

    public override void Fire()
    {
        _SpriteAnimator.StartAnimation();
        _AudioSource.Play();
    }

}
