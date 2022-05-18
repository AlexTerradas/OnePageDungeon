using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float maxVelocity = 5f;
    public float jumpSpeed = 5f;
    public float gravity = 7f;
    public float maxJumpCharge = 3f;
    public float jumpChargeSpeed = 1f;
    public float jumpHorizontalSpeedM = 0.1f;
    //private Rigidbody rb;
    private CharacterController controller;
    private CollisionFlags collisionFlags;
    private Vector3 movement;
    private float vSpeed;
    private float jumpCharge = 0;

    float temp;
    bool isRotating;
    int horizontalDirection, verticalDirection;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            movement = Vector3.zero;

            //ROTATE
            RotateCharacter();
        }

        //GRAVITY
        if (controller.isGrounded)
        {
            vSpeed = 0;
            if (Input.GetKey(KeyCode.Space))
            {
                jumpCharge += jumpChargeSpeed * Time.deltaTime;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                if (jumpCharge > maxJumpCharge)
                {
                    jumpCharge = maxJumpCharge;
                }

                movement += transform.right * jumpCharge * jumpHorizontalSpeedM;

                vSpeed = jumpCharge;
                jumpCharge = 0;
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        movement.y = vSpeed;

        //MOVEMENT
        collisionFlags = controller.Move(movement);
    }

    //void FixedUpdate()
    //{
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        print("W");
    //        //rb.AddForce(playerSpeed * new Vector3(0,0,1));
    //        rb.velocity = playerSpeed * new Vector3(0, 0, 1);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        print("S");
    //        //rb.AddForce(playerSpeed * new Vector3(0, 0, -1));
    //        rb.velocity = playerSpeed * new Vector3(0, 0, -1);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        print("A");
    //        //rb.AddForce(playerSpeed * new Vector3(-1, 0, 0));
    //        rb.velocity = playerSpeed * new Vector3(-1, 0, 0);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        print("D");
    //        //rb.AddForce(playerSpeed * new Vector3(1, 0, 0));
    //        rb.velocity = playerSpeed * new Vector3(1, 0, 0);
    //    }

    //    if (Input.GetKeyUp(KeyCode.W))
    //    {

    //    }

    //    rb.velocity = rb.velocity.normalized * maxVelocity;
    //}

    private void RotateCharacter()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            isRotating = true;
            horizontalDirection = 1;
            verticalDirection = 0;
            temp = 0;
        }
        if (Input.GetKeyDown(KeyCode.Q) && !isRotating)
        {
            isRotating = true;
            horizontalDirection = -1;
            verticalDirection = 0;
            temp = 0;
        }
        transform.Rotate(Vector3.up * 90 * Time.fixedDeltaTime * horizontalDirection, Space.World);
        transform.Rotate(Vector3.right * 90 * Time.fixedDeltaTime * verticalDirection, Space.World);
        temp += 90 * Time.fixedDeltaTime;
        if (temp >= 90)
        {
            temp = 0;
            horizontalDirection = 0;
            verticalDirection = 0;
            isRotating = false;
        }
    }
}
