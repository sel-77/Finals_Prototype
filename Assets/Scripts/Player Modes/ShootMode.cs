using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMode : MonoBehaviour
{
    public LayerMask shootableLayer; // Layer for shootable objects
    public float shootDamage = 1f; // Amount of damage dealt
    private Camera mainCamera; // Camera to convert screen space to world space

    void Start()
    {
        // Automatically get the main camera
        mainCamera = Camera.main;
    }

    void Update()
    {
        // If left mouse button is pressed, check for shooting
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Convert mouse position to world position
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, shootableLayer);

        if (hit.collider != null)
        {
            // Check if the hit object is an enemy
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Deal damage to the enemy
                enemy.TakeDamage(shootDamage);
                Debug.Log("Shot enemy: " + hit.collider.name);
            }
        }
    }
}