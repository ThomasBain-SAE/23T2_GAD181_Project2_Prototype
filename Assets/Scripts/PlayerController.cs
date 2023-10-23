using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f; // Walking speed
    public float sprintSpeed = 10f; // Sprinting speed
    public float crouchSpeed = 2f; // Crouch speed
    public float jumpForce = 10f; // Jump force
    public float crouchHeight = 0.5f; // Height when crouching
    public float sensitivity = 2.0f; // Mouse sensitivity
    public Transform playerCamera;

    private bool isSprinting = false;
    private bool isCrouching = false;
    private float originalHeight;
    private Rigidbody rb;
    private float rotationX = 0;

    void Start()
    {
        rb = GetComponent < Rigidbody>();
        originalHeight = rb.transform.localScale.y;

        // Lock and hide the mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovementInput();
        HandleSprintInput();
        HandleCrouchInput();
        HandleMouseLook();
    }

    void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Move the player
        if (movement != Vector3.zero)
        {
            float speed = isSprinting ? sprintSpeed : isCrouching ? crouchSpeed : walkSpeed;
            rb.velocity = transform.TransformDirection(movement) * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void HandleSprintInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    void HandleCrouchInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;

            if (isCrouching)
            {
                rb.transform.localScale = new Vector3(rb.transform.localScale.x, crouchHeight, rb.transform.localScale.z);
            }
            else
            {
                rb.transform.localScale = new Vector3(rb.transform.localScale.x, originalHeight, rb.transform.localScale.z);
            }
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90);

        playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, mouseX, 0);
    }
}
