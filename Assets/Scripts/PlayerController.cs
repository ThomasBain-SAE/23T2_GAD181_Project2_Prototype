using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float sensitivity = 2f;

    private Transform playerTransform;
    private Transform cameraTransform;

    private float rotationX = 0;

    private void Start()
    {
        playerTransform = transform;
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;

        // Initialize the camera's rotation to match the player's initial rotation
        cameraTransform.rotation = playerTransform.rotation;
    }

    private void Update()
    {
        // Player movement using "W," "A," "S," "D" keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        Vector3 moveVelocity = moveDirection * moveSpeed;
        playerTransform.Translate(moveVelocity * Time.deltaTime);

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
