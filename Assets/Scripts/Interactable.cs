using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject currentLocation; // The current location GameObject
    public GameObject targetLocation;  // The target location GameObject to switch to
    public GameObject requiredItemPrefab; // Prefab of the required item for interaction

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();

        // Check if requiredItemPrefab is assigned
        if (requiredItemPrefab == null)
        {
            Debug.LogError("Required Item Prefab is not assigned!");
        }
    }

    public void OnInteract(Item item)
    {
        // Check if requiredItemPrefab is assigned
        if (requiredItemPrefab != null)
        {
            // Check if the player has the required item in the inventory
            if (inventoryManager.HasItemInInventory(requiredItemPrefab.name))
            {
                if (currentLocation != null && targetLocation != null)
                {
                    // Disable the current location
                    currentLocation.SetActive(false);

                    // Enable the target location
                    targetLocation.SetActive(true);

                    Debug.Log($"Interacted with {item.gameObject.name}. Switched from {currentLocation.name} to {targetLocation.name}");

                    // Disable the item after it is used for interaction (instead of destroying it)
                    item.gameObject.SetActive(false);
                    Debug.Log($"{item.gameObject.name} has been disabled.");
                }
                else
                {
                    Debug.LogWarning("CurrentLocation or TargetLocation is not set.");
                }
            }
            else
            {
                // Provide feedback if the required item is not in the player's inventory
                Debug.Log("You don't have the required item in your inventory to interact with this object.");
            }
        }
        else
        {
            // Log if requiredItemPrefab is not assigned
            Debug.LogError("Required Item Prefab is not assigned!");
        }
    }
}
