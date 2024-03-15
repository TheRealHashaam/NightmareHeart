using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;
public class NurseAI : BehaviorTree.Tree
{
    protected override Node SetupTree()
    {
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component is missing on NurseAI GameObject.");
            return null;
        }
        Node root = new Patrol(navMeshAgent);

        return root;
    }
}
