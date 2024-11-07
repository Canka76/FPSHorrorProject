using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftDoor : MonoBehaviour
{
    private bool isOpen = false;
    private Vector3 defaultPosition;

    void Awake()
    {
        // Store the default position of the door when the game starts
        defaultPosition = transform.position;
    }

    // Toggles the door open or closed
    public void OpenDoor()
    {
        if (isOpen)
        {
            // Close the door by resetting to default position
            transform.position = defaultPosition;
        }
        else
        {
            // Open the door by moving it to the specified position
            transform.position = new Vector3(1.5f, 1, 10f);
        }

        // Toggle the door's state
        isOpen = !isOpen;
    }
}