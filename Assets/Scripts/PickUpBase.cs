using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PickUpBase : MonoBehaviour
{

    public float pickupRange;
    public Weapon wpn;
    public Transform player, gunContainer, fpsCam;

    


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickUp();
        }
    }

    abstract public void PickUp();
    

}
