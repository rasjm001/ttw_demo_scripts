using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akila.FPSFramework;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float speed = 50f;
    public float damage = 10f;
    public float lifetime = 5f;
    public float accuracy = 0.1f; // Spread factor (0 = perfect aim, higher = less accurate)

    private Rigidbody rb;

    public void Initialize(Vector3 targetPosition)
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody missing from EnemyProjectile!");
            return;
        }

        // Calculate direction toward the target
        Vector3 shootDirection = (targetPosition - transform.position).normalized;

        // Apply inaccuracy (random offset)
        shootDirection += new Vector3(
            Random.Range(-accuracy, accuracy),
            Random.Range(-accuracy, accuracy),
            Random.Range(-accuracy, accuracy)
        );

        shootDirection.Normalize(); // Ensure consistent speed

        // Apply velocity using Rigidbody (THIS IS THE FIX!)
        rb.velocity = shootDirection * speed;

        Destroy(gameObject, lifetime); // Destroy after set lifetime
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ally"))
        {
            Debug.Log("Projectile has hit player");
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();

            if (healthSystem != null)
            {
                healthSystem.Damage(damage, null);
                Debug.Log($"Enemy projectile hit {other.name}, dealing {damage} damage.");
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("Obstacle") || other.CompareTag("Cover"))
        {
            Destroy(gameObject);
        }
    }
}
