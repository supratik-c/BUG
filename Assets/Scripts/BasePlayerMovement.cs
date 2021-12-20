using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerMovement : MonoBehaviour
{

    public CharacterController _characterController;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float JumpHeight = 3f;
    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;
    Vector3 Velocity;
    bool isGrounded;
    void Start() 
    {
        Debug.Log("Never tell me the odds!");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded && Velocity.y < 0) 
        {
            Velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //Debug.Log($"x = {x } z = {z}");
        Vector3 move = transform.right * x + transform.forward * z;

        _characterController.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }


        Velocity.y += gravity * Time.deltaTime;

        _characterController.Move(Velocity * Time.deltaTime);
    }

	private void FixedUpdate()
	{
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
    }
}
