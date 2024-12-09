using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject currentLocation; // The current location GameObject
    public GameObject targetLocation;  // The target location GameObject to switch to

    public void OnInteract()
    {
        if (currentLocation != null && targetLocation != null)
        {
            // Disable the current location
            currentLocation.SetActive(false);

            // Enable the target location
            targetLocation.SetActive(true);

            Debug.Log($"Switched from {currentLocation.name} to {targetLocation.name}");
        }
        else
        {
            Debug.LogWarning("CurrentLocation or TargetLocation is not set.");
        }
    }
}