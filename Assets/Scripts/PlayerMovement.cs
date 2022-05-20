using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("VARIABLES")]
    public float playerSpeed = 5f;
    public float maxVelocity = 5f;
    public float jumpSpeed = 5f;
    public float gravity = 7f;
    public float maxJumpCharge = 3f;
    public float jumpChargeSpeed = 1f;
    public float jumpHorizontalSpeedM = 0.1f;

    //[Header("IMAGE")]
    //public Transform xMarkerImage;
    //public Transform xMarkerStartPos;
    //public float rotationImage;
    //public float speedImage;

    [Header("Slider Power Jump")]
    public Slider powerBar;
    public Camera mainCamera;

    [Header("References")]

    private CharacterController controller;
    [NonSerialized]public Vector3 movement;
    private float vSpeed;
    private float jumpCharge = 0f;
    private Transform startPosition;

    private float temp;
    private bool isRotating;
    private int horizontalDirection, verticalDirection;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        //xMarkerStartPos = xMarkerImage;
        //rotationImage = 0f;
        //speedImage = 1.4f;
        SetPower(0f);

        startPosition = transform;
    }

    private void Update()
    {

        powerBar.transform.LookAt(powerBar.transform.position + mainCamera.transform.forward * Time.fixedDeltaTime);

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

                if (jumpCharge > maxJumpCharge)
                {
                    jumpCharge = maxJumpCharge;
                }

                SetPower(jumpCharge);

                //if (rotationImage == 0f)
                //{
                //    // x ++
                //    print("x++");
                //    xMarkerImage.transform.position += new Vector3(jumpCharge * jumpHorizontalSpeedM, 0f, 0f);
                //} 
                
                //if (rotationImage == 90f)
                //{
                //    // z--
                //    print(" z--");
                //    xMarkerImage.transform.position -= new Vector3(0f, 0f, jumpCharge * jumpHorizontalSpeedM);
                //} 
                
                //if (rotationImage == 180f)
                //{
                //    // x--
                //    print("x--");
                //    xMarkerImage.transform.position -= new Vector3(1f * jumpCharge * jumpHorizontalSpeedM, 0f, 0f);
                //}
                
                //if (rotationImage == 270f)
                //{
                //    // z++
                //    print("z++");
                //    xMarkerImage.transform.position += new Vector3(0f, 0f, jumpCharge * jumpHorizontalSpeedM);
                //}

            }

            if (Input.GetKeyUp(KeyCode.Space))
            {

                SetPower(0f);

                movement += transform.right * jumpCharge * jumpHorizontalSpeedM;

                vSpeed = jumpCharge;
                jumpCharge = 0f;

                //xMarkerImage.transform.position = xMarkerStartPos.position;
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        movement.y = vSpeed;

        //MOVEMENT
        controller.Move(movement);
    }

    private void RotateCharacter()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isRotating)
        {
            isRotating = true;
            horizontalDirection = 1;
            verticalDirection = 0;
            temp = 0;
            //RotateRightImage();
        }
        if (Input.GetKeyDown(KeyCode.Q) && !isRotating)
        {
            isRotating = true;
            horizontalDirection = -1;
            verticalDirection = 0;
            temp = 0;
            //RotateLeftImage();
        }

        transform.Rotate(Vector3.up * 90 * Time.fixedDeltaTime * horizontalDirection, Space.World);
        transform.Rotate(Vector3.right * 90 * Time.fixedDeltaTime * verticalDirection, Space.World);

        //xMarkerImage.Rotate(Vector3.up * 90 * Time.fixedDeltaTime * horizontalDirection, Space.World);
        //xMarkerImage.Rotate(Vector3.right * 90 * Time.fixedDeltaTime * verticalDirection, Space.World);

        temp += 90 * Time.fixedDeltaTime;
        if (temp >= 90)
        {
            temp = 0;
            horizontalDirection = 0;
            verticalDirection = 0;
            isRotating = false;
        }
    }

    //private void RotateLeftImage()
    //{
    //    rotationImage -= 90f;

    //    if (rotationImage < 0) rotationImage = 270f;
    //}

    //private void RotateRightImage()
    //{
    //    rotationImage += 90f;

    //    if (rotationImage > 270) rotationImage = 0f;

    //}

    public void SetPower(float health)
    {
        powerBar.value = health;
    }

    public void PlayerDie()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("killPlayer"))
        {
            PlayerDie();
        }
    }
}
