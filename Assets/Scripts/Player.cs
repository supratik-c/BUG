using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health;
    public Weapon activeWeapon;
    public List<Weapon> availableWeapons = new List<Weapon>();
    public int CurrentIndex;
    public int globalAmmo;

    public Slider HealthBar;

    public GameManager GM;

    public Image CrossHair;

    public void Awake()
    {
        //activeWeapon = availableWeapons[1];
        ChangeWeapon(0);
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

    public void RecieveHealing(int healing) 
    {
        Debug.Log($"Health at {health} healing = {healing}");

        health += healing;
        if (health > 100) 
        {
            health = 100;
        }
        HealthBar.value = health;
    }

    public void ChangeWeapon(int index) 
    {
        if (index > availableWeapons.Count - 1)
        {
            index = 0;
        }
        else if (index < 0) 
        {
            index = availableWeapons.Count - 1;
        }
        Debug.Log($"index = {index}");
        for (int i = 0; i < availableWeapons.Count;i++) 
        {
            if (i == index)
            {
                activeWeapon = availableWeapons[index];
                activeWeapon.gameObject.SetActive(true);
                activeWeapon._SpriteAnimator.SetSprites(activeWeapon.fireSprites);
            }
            else 
            {
                availableWeapons[i].gameObject.SetActive(false);
            }
        }
        CurrentIndex = index;
        CrossHair.sprite = activeWeapon.CrossHair;
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

        if (Input.mouseScrollDelta.y > 0)
        {
            ChangeWeapon(CurrentIndex + 1);
        }
        else if (Input.mouseScrollDelta.y < 0) 
        {
            ChangeWeapon(CurrentIndex - 1);
        }

        
    }
}
