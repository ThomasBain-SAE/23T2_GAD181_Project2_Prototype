using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float sensitivity = 2.0f;
    private Vector3 rotation = Vector3.zero;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Player Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            Vector3 move = transform.TransformDirection(moveDirection);
            characterController.Move(move * moveSpeed * Time.deltaTime);
        }

        // Mouse Rotation
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotation.x -= mouseY;
        rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotation);
        transform.rotation *= Quaternion.Euler(0, mouseX, 0);
    }
}
