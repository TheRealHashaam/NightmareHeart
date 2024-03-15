using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;
public class Patrol : Node
{
    private NavMeshAgent agent;
    private Vector3 patrolDestination;
    private float patrolRange = 10f; // Adjust this value as needed
    private float waitTime = 3f; // Adjust this value as needed

    private bool waiting = false;
    private Coroutine waitCoroutine;

    // Constructor to initialize NavMeshAgent
    public Patrol(NavMeshAgent navAgent)
    {
        agent = navAgent;
    }

    public override NodeState Evaluate()
    {
        if (agent == null)
        {
            Debug.LogWarning("NavMeshAgent not assigned to the Patrol node.");
            return NodeState.FAILURE;
        }

        if (!waiting)
        {
            // If the agent is not currently pathfinding, assign a random destination within patrol range
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                // Generate a random destination within the patrol range
                Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
                randomDirection += agent.transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, patrolRange, NavMesh.AllAreas);

                // Set the generated destination
                patrolDestination = hit.position;
                agent.SetDestination(patrolDestination);

                // Start waiting coroutine
                waitCoroutine = StartCoroutine(WaitBeforeNextDestination());
            }
        }

        // Since the patrol behavior is continuous, always return RUNNING
        return NodeState.RUNNING;
    }

    private IEnumerator WaitBeforeNextDestination()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        waiting = false;
    }
}
