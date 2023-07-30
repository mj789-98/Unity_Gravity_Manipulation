using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
 
    private Animator animator;
    public Animator hologramAnimator;
    public GroundCheck groundCheck;

    int isRunningHash;
    int isGroundedHash;

    private InputManager inputManager;

    void Start()
    {
        animator = GetComponent<Animator>();

        //Use hash as it is faster
        isRunningHash = Animator.StringToHash("isRunning");
        isGroundedHash = Animator.StringToHash("isGrounded");

        inputManager = InputManager.Instance;
    }

    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isGrounded = animator.GetBool(isGroundedHash);

        bool movementKeyPressed = inputManager.WASD.magnitude >= 0.1f;

        // play movement animation when any movement key is pressed
        if (!isRunning && movementKeyPressed)
        {
            animator.SetBool(isRunningHash, true);
            hologramAnimator.SetBool(isRunningHash, true);
        }

        if (isRunning && !movementKeyPressed)
        {
            animator.SetBool(isRunningHash, false);
            hologramAnimator.SetBool(isRunningHash, false);
        }

        // play jumping animation if player is not grounded
        if (groundCheck.isGrounded)
        {
            animator.SetBool(isGroundedHash, true);
            hologramAnimator.SetBool(isGroundedHash, true);
        }

        if (!groundCheck.isGrounded)
        {
            animator.SetBool(isGroundedHash, false);
            hologramAnimator.SetBool(isGroundedHash, false);
        }
    }
}
