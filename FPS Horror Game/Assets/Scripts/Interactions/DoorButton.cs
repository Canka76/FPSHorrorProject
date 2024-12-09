using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorButton : Interactible
{
    [SerializeField] private UnityEvent DoorOpen;

    // Called when interacting with the button
    public override void OnInteract()
    {
        if (DoorOpen != null && DoorOpen.GetPersistentEventCount() > 0)
        {
            DoorOpen.Invoke();
        }
        else
        {
            Debug.LogWarning("DoorOpen event has no listeners assigned.");
        }
    }

    // Called when the button gains focus
    public override void OnFocus()
    {
        Debug.Log("Door button focused.");
        // Add any visual or audio feedback here if needed
    }

    // Called when the button loses focus
    public override void OnLoseFocus()
    {
        Debug.Log("Lost focus on door button.");
        // Add any cleanup or visual/audio feedback removal here if needed
    }
}