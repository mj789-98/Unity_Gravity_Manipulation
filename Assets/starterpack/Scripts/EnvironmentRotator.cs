using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnvironmentRotator : MonoBehaviour
{
    public Transform environment;

    public float rotationSpeed = 30f;
    private bool isRotatingEnvironment = false;

    private float targetRotationAmount = 90f;
    private float currentRotationAmount = 0f;
    private Vector3 rotationAxis;
    private InputManager inputManager;

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

        // Update is called once per frame
        void Update()
    {
        if (inputManager.LeftArrowKey && inputManager.Enter)
        {
            isRotatingEnvironment = true;
            rotationAxis = CalculateRotationAxis(-transform.right);
            currentRotationAmount = 0f;
        }
        else if (inputManager.RightArrowKey && inputManager.Enter)
        {
            isRotatingEnvironment = true;
            rotationAxis = CalculateRotationAxis(transform.right);
            currentRotationAmount = 0f;
        }
        else if (inputManager.UpArrowKey && inputManager.Enter)
        {
            isRotatingEnvironment = true;
            rotationAxis = CalculateRotationAxis(transform.forward);
            currentRotationAmount = 0f;
        }
        else if (inputManager.DownArrowKey && inputManager.Enter)
        {
            isRotatingEnvironment = true;
            rotationAxis = CalculateRotationAxis(-transform.forward);
            currentRotationAmount = 0f;
        }

        if (isRotatingEnvironment)
        {
            float rotationThisFrame = rotationSpeed * Time.deltaTime;

            // Limit the rotation to avoid overshooting the target rotation
            if (currentRotationAmount + rotationThisFrame > targetRotationAmount)
            {
                rotationThisFrame = targetRotationAmount - currentRotationAmount;
            }

            // Rotate the EnvironmentContainer around the character's position
            environment.transform.RotateAround(transform.position, rotationAxis, rotationThisFrame);

            // Update the current rotation amount
            currentRotationAmount += Mathf.Abs(rotationThisFrame);

            // Check if the target rotation has been reached
            if (currentRotationAmount >= targetRotationAmount)
            {
                isRotatingEnvironment = false;
                currentRotationAmount = 0f; // Reset the current rotation amount for the next rotation
            }
        }
    }

    // Calculate the rotation axis based on the player's axis
    private Vector3 CalculateRotationAxis(Vector3 referenceAxis)
    {
        if (Mathf.Abs(referenceAxis.x) > Mathf.Abs(referenceAxis.z))
        {
            if(referenceAxis.x < 0)
                return Vector3.forward;
            else
                return Vector3.back;
        }
        else
        {
            if (referenceAxis.z < 0)
                return Vector3.left;
            else
                return Vector3.right;
        }
    }
}
