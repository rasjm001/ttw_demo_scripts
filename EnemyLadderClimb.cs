using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyLadderClimb : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;
    private bool isClimbing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!isClimbing)
        {
            agent.SetDestination(target.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            StartCoroutine(ClimbLadder(other.transform));
        }
    }

    IEnumerator ClimbLadder(Transform ladder)
    {
        isClimbing = true;
        agent.enabled = false; // Disable NavMesh agent to move manually

        // Move upwards manually
        while (Vector3.Distance(transform.position, ladder.position) > 0.5f)
        {
            transform.position += Vector3.up * Time.deltaTime * 3;
            yield return null;
        }

        agent.enabled = true; // Re-enable NavMesh Agent
        isClimbing = false;
    }
}

