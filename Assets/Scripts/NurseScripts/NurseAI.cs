using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;
public class NurseAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 patrolDestination;
    private float patrolRange = 40f; // Adjust this value as needed
    private float waitTime = 3f; // Adjust this value as needed

    private bool waiting = false;
    private Coroutine waitCoroutine;
    Animator _animator;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
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
}
