using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsReset : MonoBehaviour
{

    //public Collider ResetCollider;
    public Vector3 resetPostion;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void OnTriggerEnter(Collider other)
    {
        other.transform.position = resetPostion;
    }
}
