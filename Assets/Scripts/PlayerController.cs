using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 100.0f;
    public float jumpForce = 7.0f;
    public float maxOilCapacity = 10.0f; // Maximum oil capacity
    public float jumpCooldown = 2.0f; // Cooldown in seconds

    private float oilAmount = 0.0f; // Player's oil amount
    private float lastJumpTime = -2.0f; // Initialize to a value less than -jumpCooldown

    private Rigidbody rb;
    private Transform playerTransform;
    private Transform cameraTransform;
    private float rotationX = 0;
    public float sensitivity = 2.0f; // Camera sensitivity

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerTransform = transform;
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform.rotation = playerTransform.rotation;
    }

    private void Update()
    {
        // Player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

        Vector3 moveVelocity = transform.forward * moveSpeed * verticalInput;
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        playerTransform.Rotate(Vector3.up * mouseX * sensitivity);

        // Check if the jump cooldown has expired
        if (Time.time - lastJumpTime >= jumpCooldown && Input.GetKeyDown(KeyCode.Space))
        {
            // Perform the jump
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Update the last jump time
            lastJumpTime = Time.time;
        }
    }

    public float GetOilAmount()
    {
        return oilAmount;
    }

    public void SetOilAmount(float amount)
    {
        oilAmount = Mathf.Clamp(amount, 0, maxOilCapacity);
    }

    // Add any additional methods or variables as needed.
}