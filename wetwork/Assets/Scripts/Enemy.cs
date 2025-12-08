using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal.Internal;

public class Enemy : MonoBehaviour
{
    [SerializeField] List<GameObject> path;

    private NavMeshAgent agent;
    private GameObject destination;
    private int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        destination = path[0];
        index = 0;
    }

    private void Update()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // Reached destination
                    if (index == path.Count - 1)
                        index = 0;
                    else index++;

                    destination = path[index];
                }
            }
        }

        agent.SetDestination(destination.transform.position);
    }
}
