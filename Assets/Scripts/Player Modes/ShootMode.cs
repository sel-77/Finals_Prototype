using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMode : MonoBehaviour
{
    public LayerMask shootableLayer; // Layer for shootable objects
    public float shootDamage = 1f; // Amount of damage dealt
    public Transform shootCrosshair; // Reference to the shoot crosshair object
    public float crosshairExpandSize = 1.5f; // Scale multiplier for crosshair expansion
    public float crosshairShrinkSpeed = 5f; // Speed of crosshair shrinking back to normal

    private Camera mainCamera; // Camera to convert screen space to world space
    private Vector3 originalCrosshairScale; // To store the original crosshair scale
    private bool isShooting = false; // To track if the player is currently shooting

    void Start()
    {
        // Automatically get the main camera
        mainCamera = Camera.main;

        // Store the original crosshair scale
        if (shootCrosshair != null)
        {
            originalCrosshairScale = shootCrosshair.localScale;
        }
    }

    void Update()
    {
        // If left mouse button is pressed, check for shooting
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
            Shoot();
        }

        // If left mouse button is released, stop shooting
        if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
        }

        // Handle crosshair size adjustments
        HandleCrosshairScaling();
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

    private void HandleCrosshairScaling()
    {
        if (shootCrosshair == null) return;

        if (isShooting)
        {
            // Expand the crosshair
            shootCrosshair.localScale = Vector3.Lerp(
                shootCrosshair.localScale,
                originalCrosshairScale * crosshairExpandSize,
                Time.deltaTime * crosshairShrinkSpeed
            );
        }
        else
        {
            // Shrink the crosshair back to normal
            shootCrosshair.localScale = Vector3.Lerp(
                shootCrosshair.localScale,
                originalCrosshairScale,
                Time.deltaTime * crosshairShrinkSpeed
            );
        }
    }
}