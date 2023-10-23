using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 100.0f;
    private Rigidbody rb;
    public float sensitivity = 2f;
    private Transform playerTransform;
    private Transform cameraTransform;

    private float rotationX = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents the player from tilting due to physics
        playerTransform = transform;
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;

        // Initialize the camera's rotation to match the player's initial rotation
        cameraTransform.rotation = playerTransform.rotation;
    }

    private void Update()
    {
        // Player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Rotate the player based on the input
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

        // Apply movement using forces to the Rigidbody
        Vector3 moveVelocity = transform.forward * moveSpeed * verticalInput;
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

        // Camera rotation based on mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Apply camera rotation
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        playerTransform.Rotate(Vector3.up * mouseX * sensitivity);
    }
}
