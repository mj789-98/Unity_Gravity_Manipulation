using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdPersonPlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 4f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool isJumping;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private Animator animator;

    public GameObject hologramPrefab;
    private GameObject hologram;
    private Quaternion hologramRotation;
    private bool isTeleporting;

    private float fallTimer;
    private bool isFalling;
    private bool isGameOver;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        bool isLeftKeyPressed = Input.GetKey(KeyCode.LeftArrow);
        bool isRightKeyPressed = Input.GetKey(KeyCode.RightArrow);
        bool isDownKeyPressed = Input.GetKey(KeyCode.DownArrow);
        bool isUpKeyPressed = Input.GetKey(KeyCode.UpArrow);

        float horizontal = (Input.GetKey(KeyCode.A) ? -1f : 0f) + (Input.GetKey(KeyCode.D) ? 1f : 0f);
        float vertical = (Input.GetKey(KeyCode.S) ? -1f : 0f) + (Input.GetKey(KeyCode.W) ? 1f : 0f);

        if (isLeftKeyPressed || isRightKeyPressed || isDownKeyPressed || isUpKeyPressed)
        {
            horizontal = 0f;
            vertical = 0f;
        }

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("IsMoving", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        // Check if the player is falling
        if (!isGrounded && controller.velocity.y < 0)
        {
            isFalling = true;
            fallTimer += Time.deltaTime;
        }
        else
        {
            isFalling = false;
            fallTimer = 0f;
        }

        // Check if the fall time exceeds the threshold
        if (isFalling && fallTimer >= 3f && !isGameOver)
        {
            GameOver();
        }

        // Check if teleporting key is pressed (Enter key)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isTeleporting && hologram != null)
            {
                // Teleport the player to the hologram's position
                controller.enabled = false;
                transform.position = hologram.transform.position;

                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.down, out hit, 10f, groundMask))
                {
                    // Calculate the new position on the wall
                    Vector3 wallPosition = hit.point + hit.normal * controller.skinWidth;
                    transform.position = wallPosition;
                }

                controller.enabled = true;

                // Rotate the player to match the hologram's rotation
                transform.rotation = hologramRotation;

                // Reset the hologram and teleporting flag
                Destroy(hologram);
                isTeleporting = false;
            }
        }

        // Check if the hologram projection keys are being held down
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (hologram == null)
            {
                // Instantiate the hologram prefab or create your own hologram object
                hologram = Instantiate(hologramPrefab, transform.position, Quaternion.identity);
                hologramRotation = Quaternion.Euler(0f, 0f, 180f); // Set the hologram rotation to match the player's rotation
                isTeleporting = true; // Start teleporting
            }
            else
            {
                // Rotate the hologram if it already exists
                hologram.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (hologram == null)
            {
                hologram = Instantiate(hologramPrefab, transform.position, Quaternion.identity);
                hologramRotation = Quaternion.Euler(0f, 0f, 90f);
                isTeleporting = true;
            }
            else
            {
                hologram.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (hologram == null)
            {
                hologram = Instantiate(hologramPrefab, transform.position, Quaternion.identity);
                hologramRotation = Quaternion.Euler(0f, 0f, -90f);
                isTeleporting = true;
            }
            else
            {
                hologram.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            }
        }
        else
        {
            // If no hologram projection keys are held, destroy the hologram
            if (!isTeleporting)
            {
                Destroy(hologram);
            }
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        // Activate the game over screen
        GameObject gameOverScreen = GameObject.Find("GameOverScreen");
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
            // Pass the current time if needed
            GameOverScreen gameOverScript = gameOverScreen.GetComponent<GameOverScreen>();
            if (gameOverScript != null)
            {
                gameOverScript.Setup(Time.time);
            }
        }
        else
        {
            Debug.LogWarning("Game Over Screen not found. Make sure the GameOverScreen object exists in the scene and is named 'GameOverScreen'.");
        }
    }
}
