using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 100.0f;
    public float jumpForce = 7.0f;
    public float maxOilCapacity = 10.0f; // Maximum oil capacity

    private float oilAmount = 0.0f; // Player's oil amount

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
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Use Mathf.Clamp to limit the rotationX

        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        playerTransform.Rotate(Vector3.up * mouseX * sensitivity);

        // Jump on Space key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public float GetOilAmount()
    {
        return oilAmount;
    }

    public void SetOilAmount(float amount)
    {
        oilAmount = Mathf.Clamp(amount, 0, maxOilCapacity); // Use Mathf.Clamp to limit the oilAmount
    }

    // Add any additional methods or variables as needed.
}