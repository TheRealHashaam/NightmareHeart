using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;
public class NurseAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 patrolDestination;
    private float patrolRange = 40f;
    private float waitTime = 3f;

    private bool waiting = false;
    private Coroutine waitCoroutine;
    Animator _animator;
    public Transform target;
    public float detectionRange = 10f;
    public float fieldOfViewAngle = 45f;
    public float closeRangeThreshold = 5f;
    private bool playerInSafeZone = false;
    private enum AIState
    {
        Patrol,
        Chase
    }

    private AIState currentState = AIState.Patrol;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        switch (currentState)
        {
            case AIState.Patrol:
                Patrol();
                break;
            case AIState.Chase:
                Chase();
                break;
        }
        if (currentState == AIState.Patrol)
        {
            DetectTarget();
        }
    }

    void Patrol()
    {
        agent.speed = 2;
        if (!waiting)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
                randomDirection += agent.transform.position;


                waitCoroutine = StartCoroutine(WaitBeforeNextDestination(randomDirection));
            }
        }
    }

    void Chase()
    {
        if(!playerInSafeZone)
        {
            agent.speed = 3;
            agent.SetDestination(target.position);
            _animator.SetBool("Run", true);
            waiting = false;
        }
        else
        {
            currentState = AIState.Patrol;
        }
    }

    IEnumerator Look_Delay()
    {
        agent.SetDestination(transform.position);
        _animator.SetBool("Idle", true);
        yield return new WaitForSeconds(0.5f);
        _animator.SetTrigger("Look");
        yield return new WaitForSeconds(2f);
        currentState = AIState.Chase;
    }

    private IEnumerator WaitBeforeNextDestination(Vector3 randomDirection)
    {
        waiting = true;
        _animator.SetBool("Idle", true);
        _animator.SetBool("Walk", false);
        yield return new WaitForSeconds(waitTime);
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, patrolRange, NavMesh.AllAreas);
        patrolDestination = hit.position;
        agent.SetDestination(patrolDestination);
        waiting = false;
        _animator.SetBool("Idle", false);
        _animator.SetBool("Walk", true);
    }

    private void DetectTarget()
    {
        Vector3 directionToTarget = target.position - transform.position;
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

        // Check if target is within detection range and within the AI's field of view
        if (directionToTarget.magnitude < detectionRange && angleToTarget < fieldOfViewAngle / 2f)
        {
            RaycastHit hit;

            // Perform a raycast to check if there are obstacles between the AI and the target
            if (Physics.Raycast(transform.position, directionToTarget, out hit, detectionRange))
            {
                if (hit.collider.gameObject == target.gameObject)
                {
                    // Target detected, transition to chase state
                    currentState = AIState.Chase;
                    return;
                }
            }
        }

        // Check if target is very close regardless of the direction
        if (directionToTarget.magnitude < closeRangeThreshold)
        {
            StartCoroutine(Look_Delay());
            
        }
    }


}
