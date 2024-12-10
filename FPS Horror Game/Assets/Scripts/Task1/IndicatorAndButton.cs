using UnityEngine;

public class RotateWithScroll : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f; // Speed of rotation
    [SerializeField] private float indicatorSpeed = 2f; // Speed of indicator movement
    public Transform Button; // Reference to the button to rotate
    public Transform minY,maxY; // Minimum Y position
    

    void Update()
    {
        ButtonScroll();
        IndicatorMovement();
    }

    void ButtonScroll()
    {
        // Get the scroll wheel input
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Rotate the button based on the scroll input
        if (scrollInput != 0)
        {
            Button.Rotate(0, 0, scrollInput * rotationSpeed * Time.deltaTime * 100f);
        }
    }

    void IndicatorMovement()
    {
        // Get the scroll wheel input
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Calculate the new Y position based on the scroll input
        if (scrollInput != 0)
        {
            float newY = transform.position.y + scrollInput * indicatorSpeed * Time.deltaTime * 100f;

            // Clamp the Y position to stay within the specified range
            newY = Mathf.Clamp(newY, minY.position.y, maxY.position.y);

            // Update the indicator's position
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}