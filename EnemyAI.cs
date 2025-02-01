using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum AIState { Defending, Pushing }
    public AIState currentState = AIState.Defending;

    private Transform player; // Now automatically assigned
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
    GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
    if (playerObj != null)
    {
        player = playerObj.transform;
    }
    else
    {
        Debug.LogError("EnemyAI: No object with tag 'Player' found in the scene!");
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
            ShootAtPlayer();
        }
    }

    // ---------------- PUSHING STATE ----------------
    private void PushTowardsPlayer()
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
