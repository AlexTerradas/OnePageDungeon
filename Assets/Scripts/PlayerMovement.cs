using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("VARIABLES")]
    public float gravity = 7f;
    public float maxJumpCharge = 3f;
    public float jumpChargeSpeed = 1f;
    public float jumpHorizontalSpeedM = 0.1f;
    public float rotationSpeedXD = 3f;

    //[Header("IMAGE")]
    //public Transform xMarkerImage;
    //public Transform xMarkerStartPos;
    //public float rotationImage;
    //public float speedImage;

    [Header("Slider Power Jump")]
    public Slider powerBar;
    public Camera mainCamera;

    [Header("References")]
    public GroundDetector groundDetector;
    private CharacterController controller;
    [NonSerialized]public Vector3 movement;
    private float vSpeed;
    private float jumpCharge = 0f;
    private Transform startPosition;

    private float temp;
    public bool isRotating;
    private int horizontalDirection, verticalDirection;
    public bool jumpPressed = false;
    public bool jumpReleased = false;
    private float currentStopGravityTime;
    public float stopGravityTime = 0.1f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        //xMarkerStartPos = xMarkerImage;
        //rotationImage = 0f;
        //speedImage = 1.4f;
        SetPower(0f);

        startPosition = transform;

        powerBar.maxValue = maxJumpCharge;
    }

    private void Update()
    {
        //GRAVITY
        if (groundDetector.isGrounded)
        {
            currentStopGravityTime -= Time.deltaTime;

            if(stopGravityTime <= 0)
            {
                vSpeed = 0.0f;
            }

            if (Input.GetKey(KeyCode.Space) && !isRotating)
                jumpPressed = true;
            else
                jumpPressed = false;

            if (Input.GetKeyUp(KeyCode.Space) && !isRotating)
            {
                jumpReleased = true;
            }                

            if (Input.GetKeyDown(KeyCode.E) && !isRotating && !jumpPressed)
            {
                isRotating = true;
                horizontalDirection = 1;
                verticalDirection = 0;
                temp = 0;
                //RotateRightImage();
            }
            if (Input.GetKeyDown(KeyCode.Q) && !isRotating && !jumpPressed)
            {
                isRotating = true;
                horizontalDirection = -1;
                verticalDirection = 0;
                temp = 0;
                //RotateLeftImage();
            }
        }
        else
        {
            currentStopGravityTime = stopGravityTime;
        }

        if (isRotating)
        {
            Time.timeScale = rotationSpeedXD;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void FixedUpdate()
    {
        powerBar.transform.LookAt(powerBar.transform.position + mainCamera.transform.forward * Time.fixedDeltaTime);

        if (groundDetector.isGrounded)
        {
            movement = Vector3.zero;

            //ROTATE
            RotateCharacter();
        }

        if (jumpPressed)
        {
            jumpCharge += jumpChargeSpeed * Time.fixedDeltaTime;

            if (jumpCharge > maxJumpCharge)
            {
                jumpCharge = maxJumpCharge;
            }

            SetPower(jumpCharge);
        }

        if (jumpReleased)
        {
            jumpReleased = false;
            SetPower(0f);

            movement += transform.right * jumpCharge * jumpHorizontalSpeedM;

            vSpeed = jumpCharge;
            jumpCharge = 0f;            
        }

        vSpeed -= gravity * Time.fixedDeltaTime;
        movement.y = vSpeed;

        //MOVEMENT
        controller.Move(movement);
    }

    private void RotateCharacter()
    {

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
