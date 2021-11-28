using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public Weapon activeWeapon;
    public List<Weapon> availableWeapons = new List<Weapon>();
    public int globalAmmo = 0;
    


    public void Awake()
    {
        
        activeWeapon = availableWeapons[0];
    }

    public void Shoot()
    {
        if (globalAmmo == 0)
        {
            // Play click sound
            activeWeapon.Empty();
            
        } else
        {
            // Fire gun
            activeWeapon.Fire();
            globalAmmo -= activeWeapon.ammoDivisor;
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            // Ded
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }
}
