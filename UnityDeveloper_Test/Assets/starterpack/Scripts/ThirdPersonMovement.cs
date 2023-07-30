using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Transform cam;
    public Rigidbody rb;
    public GroundCheck groundCheck;

    // Variables for player movement properties
    public float speed = 20f;
    public float jumpForce = 10f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private Vector3 movementDirection;
    private InputManager inputManager;

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        //Get the movement direction from input manager and noramlize it
        movementDirection = new Vector3(inputManager.WASD.x, 0f, inputManager.WASD.y).normalized;

        if (movementDirection.magnitude >= 0.1f)
        {
            // Calculate the target angle for player rotation based on the camera's direction
            float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            // Smoothly rotate the player towards the target angle
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        // Jump if the player is on the ground and the Jump key is pressed
        if (groundCheck.isGrounded && inputManager.Space)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        //apply the movement to player
        if (movementDirection.magnitude >= 0.1f)
        {
            Vector3 moveDir = Quaternion.Euler(0f, cam.eulerAngles.y, 0f) * movementDirection;
            rb.AddForce(moveDir * speed, ForceMode.VelocityChange);
        }
    }
}
