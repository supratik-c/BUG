using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerMovement : MonoBehaviour
{

    public CharacterController _characterController;
    public float speed = 12f;
    public float gravity = -9.81f;

    Vector3 Velocity;
    void Start() 
    {
        Debug.Log("Never tell me the odds!");
    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        _characterController.Move(move * speed * Time.deltaTime);

        Velocity.y += gravity * Time.deltaTime;

        _characterController.Move(Velocity * Time.deltaTime);
    }
}
