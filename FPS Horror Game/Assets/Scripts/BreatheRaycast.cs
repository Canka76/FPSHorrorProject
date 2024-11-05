using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreatheRaycast : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask = default;
    [SerializeField] private float rayDistance = 10f; // Default distance for raycast
    [SerializeField] private Vector3 breatheRaycastPoint = new Vector3(0.5f, 0.5f, 0f); // Center of the screen
    [SerializeField] private float gazeDriftFlexer = 0.2f; // Allowed time for the ray to "drift" off the target

    private Camera playerCamera;
    private BreatheAction currentBreatheAction;
    [SerializeField] private float timeSinceLastHit;

    private void Awake()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        // Only handle raycasting if playerCamera is assigned
        if (playerCamera != null)
        {
            HandleBreatheRaycast();
        }
    }

    private void HandleBreatheRaycast()
    {
        // Cast a ray from the camera's viewpoint towards the scene
        if (Physics.Raycast(playerCamera.ViewportPointToRay(breatheRaycastPoint), out RaycastHit hit, rayDistance))
        {
            // Check if hit object is within the specified layer mask
            if (((1 << hit.collider.gameObject.layer) & layerMask) != 0 
                && (currentBreatheAction == null || hit.collider.gameObject.GetInstanceID() != currentBreatheAction.GetInstanceID()))
            {
                // Try to get the BreatheAction component on the hit object
                if (hit.collider.TryGetComponent(out BreatheAction breatheAction))
                {
                    currentBreatheAction = breatheAction;
                    currentBreatheAction.OnFocusEnemy();
                    timeSinceLastHit = Time.time; // Reset the time since last hit
                }
            }
            else if (currentBreatheAction != null)
            {
                // Update the time since the last valid hit
                timeSinceLastHit = Time.time;
            }
        }
        else if (currentBreatheAction != null)
        {
            // Check if allowed drift time has passed since the last hit
            if (Time.time - timeSinceLastHit > gazeDriftFlexer)
            {
                currentBreatheAction.OnLoseFocusEnemy();
                currentBreatheAction = null;
            }
        }
    }
}
