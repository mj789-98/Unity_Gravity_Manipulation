using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramController : MonoBehaviour
{
    public GameObject hologram;
    public float rotationSpeed = 720f; // Adjust this value to control the rotation speed

    private Quaternion originalRotation; //the rotation of the player
    private Quaternion rotationZ;
    private Quaternion rotationX;
    private bool isRotating = false;

    private InputManager inputManager;

    private void Start()
    {
        inputManager = InputManager.Instance;
        rotationZ = Quaternion.Euler(0f, 0f, 90f);
        rotationX = Quaternion.Euler(90f, 0f, 0f);
        hologram.SetActive(false); // Set the initial state of the hologram to inactive
    }

    private void Update()
    {
        //Set the original rotation as the players rotation
        originalRotation = transform.rotation;

        //Check for input from input manager
        if (inputManager.LeftArrowKey)
        {
            RotateHologram(Quaternion.Inverse(rotationZ));
        }
        else if (inputManager.RightArrowKey)
        {
            RotateHologram(rotationZ);
        }
        else if (inputManager.UpArrowKey)
        {
            RotateHologram(Quaternion.Inverse(rotationX));
        }
        else if (inputManager.DownArrowKey)
        {
            RotateHologram(rotationX);
        }
        else
        {
            StopRotation(); //stop rotation if no key is being pressed
        }
    }

    private void RotateHologram(Quaternion target)
    {
        if (!isRotating)
        {
            isRotating = true;
            hologram.SetActive(true); // Activate the hologram when a direction key is pressed
        }

        Quaternion targetRotation = transform.rotation * target; 

        // Rotate the hologram towards the target rotation
        hologram.transform.rotation = Quaternion.RotateTowards(hologram.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // stop rotating if the hologram has reached the target rotation
        if (Quaternion.Angle(hologram.transform.rotation, targetRotation) <= 0.1f)
        {
            isRotating = false;
        }
    }

    private void StopRotation()
    {
        if (isRotating)
        {
            isRotating = false;
        }

        // Rotate the hologram back to its original rotation (the player's rotation)
        hologram.transform.rotation = Quaternion.RotateTowards(hologram.transform.rotation, originalRotation, rotationSpeed * Time.deltaTime);

        // Check if the hologram has reached original rotation
        if (Quaternion.Angle(hologram.transform.rotation, originalRotation) <= 0.1f)
        {
            hologram.SetActive(false);
        }
    }
}
