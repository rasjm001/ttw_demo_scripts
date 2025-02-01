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

    private Vector3 shootDirection;

    public void Initialize(Vector3 targetPosition)
    {
        // Calculate direction toward the target
        shootDirection = (targetPosition - transform.position).normalized;

        // Apply inaccuracy (random offset)
        shootDirection += new Vector3(
            Random.Range(-accuracy, accuracy),
            Random.Range(-accuracy, accuracy),
            Random.Range(-accuracy, accuracy)
        );

        shootDirection.Normalize(); // Ensure consistent speed

        Destroy(gameObject, lifetime); // Destroy after set lifetime
    }

    private void Update()
    {
        transform.position += shootDirection * speed * Time.deltaTime;
    }

    private void OnTriggerE
