using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;
using DG.Tweening;
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
    public bool playerInSafeZone = false;
    bool _canDeal = true;
    public AudioSource ChaseMusic;
    public AudioSource CloseMusic;
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
        _animator.SetBool("Run", false);
        if (!waiting)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
                randomDirection += agent.transform.position;


                waitCoroutine = StartCoroutine(WaitBeforeNextDestination(randomDirection));
            }
        }
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < 30f)
        {
            if(!CloseMusic.isPlaying)
            {
                CloseMusic.Play();
                CloseMusic.DOFade(1, 1f);
            }
            if (distance < 20)
            {
                CloseMusic.DOPitch(0.5f, 1f);
            }
        }
        if(playerInSafeZone|| distance > 30f)
        {
            CloseMusic.Play();
            CloseMusic.DOFade(0, 1f).OnComplete(() =>
            {
                CloseMusic.Stop();
            });
        }
        
    }
    void Chase()
    {
        if(!playerInSafeZone)
        {
            if(!ChaseMusic.isPlaying)
            {
                ChaseMusic.DOFade(1, 0.5f);
                ChaseMusic.Play();
                CloseMusic.Stop();
                CloseMusic.volume = 0;
            }
            agent.speed = 3.5f;
            agent.SetDestination(target.position);
            _animator.SetBool("Run", true);
            waiting = false;
            if(_canDeal)
            {
                StartCoroutine(Chase_Damage());
            }
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance < 2f)
            {
                FindObjectOfType<GameManager>().PlayAgain();
            }
        }
        else
        {
            currentState = AIState.Patrol;
            ChaseMusic.DOFade(0, 0.5f).OnComplete(() => 
            {
                ChaseMusic.Stop();
            });
        }
    }

    IEnumerator Chase_Damage()
    {
        _canDeal = false;
        yield return new WaitForSeconds(1f);
        target.GetComponent<Health>().DealDamage(1);
        _canDeal = true;
    }

    IEnumerator Look_Delay()
    {
        ChaseMusic.Play();
        agent.SetDestination(transform.position);
        _animator.SetBool("Idle", true);
        yield return new WaitForSeconds(0.5f);
        _animator.SetTrigger("Look");
        yield return new WaitForSeconds(2f);
        currentState = AIState.Chase;
        _animator.ResetTrigger("Look");
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
        if(!playerInSafeZone)
        {
            Vector3 directionToTarget = target.position - transform.position;
            float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
            if (directionToTarget.magnitude < detectionRange && angleToTarget < fieldOfViewAngle / 2f)
            {
                currentState = AIState.Chase;
                return;
            }
            if (directionToTarget.magnitude < closeRangeThreshold && angleToTarget > 90f)
            {
                StartCoroutine(Look_Delay());
            }
        }

    }


}
