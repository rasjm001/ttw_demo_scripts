using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum AIState { Defending, Pushing }
    public AIState currentState = AIState.Defending;

    private Transform player; // Automatically assigned
    public Transform[] coverPoints;
    public float shootingRange = 20f;
    public float attackRange = 10f;
    public float movementSpeed = 5f;
    public float shootingDelay = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 25f;
    public float health = 10f;

    private NavMeshAgent agent;
    private float timeToShoot;
    private int currentCoverIndex = 0;
    private bool isInCover = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timeToShoot = shootingDelay;

        // Find the player automatically
        GameObject playerObj = GameObject.FindGameObjectWithTag("playerTarget");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("EnemyAI: No object with tag 'playerTarget' found in the scene!");
        }

        // Find all cover points automatically
        GameObject[] coverObjects = GameObject.FindGameObjectsWithTag("Cover");
        if (coverObjects.Length > 0)
        {
            coverPoints = new Transform[coverObjects.Length];
            for (int i = 0; i < coverObjects.Length; i++)
            {
                coverPoints[i] = coverObjects[i].transform;
            }
        }
        else
        {
            Debug.LogError("EnemyAI: No objects with tag 'Cover' found in the scene!");
        }
    }

    private void Update()
    {
        if (player == null) return; // Ensure the script doesn't run without a player
        
        switch (currentState)
        {
            case AIState.Defending:
                DefendPosition();
                break;

            case AIState.Pushing:
                PushTowardsPlayer();
                break;
        }
    }

    // ---------------- DEFENDING STATE ----------------
    private void DefendPosition()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= shootingRange)
        {
            FacePlayer();
            ShootAtPlayer();
        }
    }

    // ---------------- PUSHING STATE ----------------
    private void PushTowardsPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            // Stop moving and attack
            agent.ResetPath(); // Stops the enemy from moving
            FacePlayer();
            ShootAtPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            agent.SetDestination(player.position);
            agent.speed = movementSpeed;
            isInCover = false;
        }
        else
        {
            agent.ResetPath(); // Stop moving once within attack range
        }
    }

    private void FacePlayer()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Keep rotation horizontal
        transform.forward = direction;
    }

    private void ShootAtPlayer()
    {
        if (timeToShoot <= 0)
        {
            // Get the direction towards the player
            Vector3 direction = (player.position - firePoint.position).normalized;

            // Instantiate the projectile and initialize it
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            EnemyProjectile enemyProjectile = bullet.GetComponent<EnemyProjectile>();
            
            if (enemyProjectile != null)
            {
                enemyProjectile.Initialize(player.position);  // Pass player position to the projectile
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
            FacePlayer();
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

    private IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(2f);
        isInCover = false;
        currentCoverIndex = (currentCoverIndex + 1) % coverPoints.Length;
        SeekCover();
    }

    // ---------------- TOGGLE STATES ----------------
    public void SetState(AIState newState)
    {
        currentState = newState;
    }
}
