using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1.5f; // Shots per second
    public float detectionRange = 20f;
    
    private Transform target;
    private bool canShoot = true;

    private void Update()
    {
        FindTarget();

        if (target != null && canShoot)
        {
            StartCoroutine(FireProjectile());
        }
    }

    void FindTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player && Vector3.Distance(transform.position, player.transform.position) <= detectionRange)
        {
            target = player.transform;
        }
        else
        {
            target = null;
        }
    }

    IEnumerator FireProjectile()
    {
        canShoot = false;

        // Instantiate the bullet and shoot towards the player
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        bullet.transform.LookAt(target); // Make the bullet face the player

        yield return new WaitForSeconds(1f / fireRate); // Delay based on fire rate
        canShoot = true;
    }
}