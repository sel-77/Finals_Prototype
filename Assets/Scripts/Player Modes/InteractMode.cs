using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMode : MonoBehaviour
{
    public LayerMask interactableLayer; // Layer for interactable objects
    private Camera mainCamera; // Camera to convert screen space to world space

    void Start()
    {
        // Automatically get the main camera
        mainCamera = Camera.main;
    }

    void Update()
    {
        // If left mouse button is pressed, check for interaction
        if (Input.GetMouseButtonDown(0))
        {
            Interact();
        }
    }

    private void Interact()
    {
        // Cast a ray from the mouse position to detect interactable objects
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, interactableLayer);
        if (hit.collider != null)
        {
            // Check if the hit object is interactable
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                // Call the interaction method
                interactable.OnInteract();
                Debug.Log("Interacted with: " + hit.collider.name);
            }
        }
    }
}