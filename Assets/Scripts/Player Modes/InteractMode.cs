using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMode : MonoBehaviour
{
    public LayerMask interactableLayer; // Layer for interactable objects
    public Texture2D defaultCursor; // Default cursor sprite
    public Texture2D interactCursor; // Cursor sprite for interactables

    private Camera mainCamera; // Camera to convert screen space to world space
    private bool isHoveringInteractable = false;

    void Start()
    {
        // Automatically get the main camera
        mainCamera = Camera.main;

        // Set the default cursor at the start
        SetCursor(defaultCursor);
    }

    void Update()
    {
        CheckHoverState();

        // If left mouse button is pressed, check for interaction
        if (Input.GetMouseButtonDown(0))
        {
            Interact();
        }
    }

    private void CheckHoverState()
    {
        // Cast a ray from the mouse position to detect interactable objects
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, interactableLayer);

        if (hit.collider != null)
        {
            // Check if the hit object is interactable
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                if (!isHoveringInteractable)
                {
                    Debug.Log("Hovering over interactable: " + hit.collider.name);
                    SetCursor(interactCursor);
                    isHoveringInteractable = true;
                }
                return;
            }
        }

        // If no interactable is hovered, reset to the default cursor
        if (isHoveringInteractable)
        {
            Debug.Log("No longer hovering over an interactable.");
            SetCursor(defaultCursor);
            isHoveringInteractable = false;
        }
    }

    private void Interact()
    {
        // Cast a ray from the mouse position to detect interactable objects
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, interactableLayer);

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

    private void SetCursor(Texture2D cursor)
    {
        Vector2 hotspot = Vector2.zero; // Adjust if necessary (e.g., center the cursor)
        Cursor.SetCursor(cursor, hotspot, CursorMode.Auto);
    }

    public void ResetCursorToDefault()
    {
        SetCursor(defaultCursor);
    }
}