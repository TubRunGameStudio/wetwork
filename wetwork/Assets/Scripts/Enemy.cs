using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal.Internal;

public class Enemy : MonoBehaviour
{
    [SerializeField] List<GameObject> path;
    [SerializeField] private Animator animator;

    private NavMeshAgent agent;
    private GameObject destination;
    private int index;
    private Vector3 prevPos;

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

        // animations
        SetAnimation(transform.position, prevPos);
        prevPos = transform.position;
    }

    private void SetAnimation(Vector3 curr, Vector3 prev)
    {
        float diffX = curr.x - prev.x;
        float diffY = curr.y - prev.y;

        if (diffX > 0 && diffY < 0)
            animator.SetInteger("Direction", 1);
        else if (diffX > 0 && diffY > 0)
            animator.SetInteger("Direction", 2);
        else if (diffX < 0 && diffY > 0)
            animator.SetInteger("Direction", 3);
        else if (diffX < 0 && diffY < 0)
            animator.SetInteger("Direction", 4);

    }

    public GameObject GetDestination()
    {
        return destination;
    }

    public void SetDestination(GameObject des)
    {
        destination = des;
    }
}
