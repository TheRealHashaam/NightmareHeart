using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform Pos;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    [ContextMenu("Go")]
    public void GoToPos()
    {
        agent.SetDestination(Pos.position);
    }

}
