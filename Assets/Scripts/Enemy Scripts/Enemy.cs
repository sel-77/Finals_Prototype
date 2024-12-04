using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 3f;
    public float growthRate = 0.5f; // Rate at which the enemy grows per second
    public float maxScaleMultiplier = 2f; // 2x size limit
    private Vector2 originalScale; // Store the enemy's original scale

    private bool hasKilledPlayer = false; // Prevent duplicate player death logic

    void Start()
    {
        originalScale = transform.localScale; // Store the initial scale of the enemy (as Vector2)
    }

    void Update()
    {
        if (!hasKilledPlayer)
        {
            // Gradually grow the enemy over time
            Vector2 growth = Vector2.one * growthRate * Time.deltaTime;
            transform.localScale += (Vector3)growth; // Cast back to Vector3 for transform.localScale

            // Check if the enemy has reached 2x its size
            if (transform.localScale.x >= originalScale.x * maxScaleMultiplier &&
                transform.localScale.y >= originalScale.y * maxScaleMultiplier)
            {
                KillPlayer();
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (!hasKilledPlayer)
        {
            health -= amount;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject); // Remove the enemy from the scene
    }

    void KillPlayer()
    {
        hasKilledPlayer = true; // Ensure this logic runs only once
        Debug.Log("Player has been killed!");

        // Disable player controls (example)
        ShootMode shooter = FindObjectOfType<ShootMode>(); // Find the Shooter script in the scene
        if (shooter != null)
        {
            shooter.enabled = false; // Disable the shooting ability
        }

        // Optional: Show a game over screen
        // Example: GameOverManager.Instance.ShowGameOverScreen();
    }
}
