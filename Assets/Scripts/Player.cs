using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health;
    public Weapon activeWeapon;
    public List<Weapon> availableWeapons = new List<Weapon>();
    public int globalAmmo;

    public Slider HealthBar;

    public GameManager GM;

    public void Awake()
    {
        activeWeapon = availableWeapons[0];
        HealthBar.maxValue = health;
        HealthBar.value = health;
        GM = FindObjectOfType<GameManager>();
    }

    public void Shoot()
    {
        if (globalAmmo <= 0)
        {
            // Play click sound
            activeWeapon._SpriteAnimator.SetSprites(activeWeapon.emptySprites);
            activeWeapon.Empty();

        } else
        {
            // Fire gun
            activeWeapon._SpriteAnimator.SetSprites(activeWeapon.fireSprites);

            if (activeWeapon.Fire()) 
            {
                globalAmmo -= activeWeapon.ammoDivisor;
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        HealthBar.value = health;
        if(health <= 0)
        {
            // Ded
            GM.WinLose(false);
            Debug.Log("Ded");
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
            //Debug.Log($"Global = {globalAmmo}");
        }

        if (transform.position.y < -5) 
        {
            TakeDamage(health + 10);
        }
    }
}
