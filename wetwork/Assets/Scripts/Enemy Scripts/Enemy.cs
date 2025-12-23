using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal.Internal;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<GameObject> path;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject alertShout;
    [SerializeField] private GameObject cautionShout;
    [SerializeField] private FieldOfView fov;
    [SerializeField] private float speed;

    private enum State
    {
        ALERT,
        CAUTION,
        NORMAL
    }

    private NavMeshAgent agent;
    private GameObject destination;
    private int index;
    private Vector3 prevPos;
    private State state;
    private GameObject lastKnownPosition;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        destination = path[0];
        index = 0;
        state = State.NORMAL;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (state == State.ALERT)
        {
            if(timer > .5)
            {
                Debug.Log("Bang Bang");
                timer = 0;
            }
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
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
                        ChangeState(State.NORMAL);
                    }
                }
            }

            agent.SetDestination(destination.transform.position);
            fov.SetOrigin(transform.position);
            Vector3 diff = transform.position - prevPos;
            fov.SetAimDirection(diff);

            // animations
            SetAnimation(transform.position, prevPos);
            prevPos = transform.position;
        }
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

    public void SetAlert(GameObject des)
    {
        ChangeState(State.ALERT);
        destination = des;
    }

    public void SetCaution(GameObject des)
    {
        ChangeState(State.CAUTION);

        // Set player last known position
        lastKnownPosition = des;
        destination = des;
    }

    private void ChangeState(State state)
    {
        if(state == State.ALERT)
        {
            alertShout.SetActive(true);
            cautionShout.SetActive(false);
            agent.speed = speed + 1;
            GameObject.Destroy(lastKnownPosition);
            timer = 0;
        }
        else if(state == State.CAUTION)
        {
            alertShout.SetActive(false);
            cautionShout.SetActive(true);
        } else
        {
            alertShout.SetActive(false);
            cautionShout.SetActive(false);
            agent.speed = speed;
            GameObject.Destroy(lastKnownPosition);
        }
            this.state = state;
    }

}
