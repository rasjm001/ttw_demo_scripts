using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Akila.FPSFramework; // Add this to access the HealthSystem class

public class EnemyProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float speed = 50f;
    public float damage = 10f;
    public float lifetime = 5f;
    public float accuracy = 0.1f; // Spread factor (0 = perfect aim, higher = less accurate)

    private Vector3 shootDirection;

    private void Start()
    {
        // Apply accuracy by randomly modifying the direction slightly
        shootDirection = transform.forward + new Vector3(
            Random.Range(-accuracy, accuracy), 
            Random.Range(-accuracy, accuracy), 
            Random.Range(-accuracy, accuracy)
        );

        shootDirection.Normalize(); // Normalize to keep consistent speed
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += shootDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if we hit a valid target
        if (other.CompareTag("Player") || other.CompareTag("Ally"))
        {
            Debug.Log("Projectile has hit player");
            HealthSystem healthSystem = other.GetComponent<HealthSystem>();

            if (healthSystem != null)
            {
                healthSystem.Damage(damage, null); // 'null' since there's no specific attacker
                Debug.Log($"Enemy projectile hit {other.name}, dealing {damage} damage.");
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("Obstacle") || other.CompareTag("Cover"))
        {
            Destroy(gameObject); // Destroy if it hits walls or cover
        }
    }
}
