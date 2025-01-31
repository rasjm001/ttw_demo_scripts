using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to player position
    public Transform[] coverPoints; // Array of cover points
    public float shootingRange = 20f; // Distance to start shooting
    public float attackRange = 10f; // Distance to move towards player
    public float movementSpeed = 5f; // AI movement speed
    public float shootingDelay = 1f; // Time between shots
    public GameObject bulletPrefab; // Bullet prefab
    public Transform firePoint; // Where bullets spawn
    public float bulletSpeed = 25f; // Bullet travel speed

    private NavMeshAgent agent;
    private float timeToShoot;
    private int currentCoverIndex = 0;
    private bool isInCover = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timeToShoot = shootingDelay;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            MoveTowardsPlayer();

            if (distanceToPlayer <= shootingRange)
            {
                ShootAtPlayer();
            }
        }
        else
        {
            SeekCover();
        }
    }

    private void MoveTowardsPlayer()
    {
        agent.SetDestination(player.position);
        agent.speed = movementSpeed;
        isInCover = false;
    }

    private void ShootAtPlayer()
    {
        if (timeToShoot <= 0)
        {
            // Create bullet and shoot toward the player
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direction = (player.position - firePoint.position).normalized;
                rb.velocity = direction * bulletSpeed;
            }

            timeToShoot = shootingDelay;
        }
        else
        {
            timeToShoot -= Time.deltaTime;
        }
    }

    private void SeekCover()
    {
        if (isInCover)
        {
            ShootAtPlayer();
        }
        else
        {
            MoveToNextCover();
        }
    }

    private void MoveToNextCover()
    {
        if (Vector3.Distance(transform.position, coverPoints[currentCoverIndex].position) < 1f)
        {
            isInCover = true;
            StartCoroutine(WaitAndMove());
        }
        else
        {
            agent.SetDestination(coverPoints[currentCoverIndex].position);
            agent.speed = movementSpeed;
        }
    }

    private System.Collections.IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(2f);
        isInCover = false;
        currentCoverIndex = (currentCoverIndex + 1) % coverPoints.Length;
        SeekCover();
    }
}
