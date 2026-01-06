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
            if(timer > 1)
            {
                PlayerController player = destination.GetComponent<PlayerController>();
                animator.SetBool("Shoot", true);
                player.Damage(1);
                timer = 0;
            } else
            {
                animator.SetBool("Shoot", false);
            }
            agent.isStopped = true;
        }
        else
        {
            // animations
            SetWalkAnimation(transform.position, prevPos);

            // behavior
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
                        ChangeState(State.NORMAL, destination);
                    }
                }
            }

            agent.SetDestination(destination.transform.position);
            fov.SetOrigin(transform.position);
            Vector3 diff = transform.position - prevPos;
            fov.SetAimDirection(diff);

            prevPos = transform.position;
        }
    }

    private void SetWalkAnimation(Vector3 curr, Vector3 prev)
    {
        animator.SetBool("Walking", true);

        float diffX = curr.x - prev.x;
        float diffY = curr.y - prev.y;

        if (diffY < 0)
            animator.SetBool("North", false);
        else if (diffY > 0)
            animator.SetBool("North", true);

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (diffX > 0)
            sprite.flipX = false;
        else if (diffX < 0)
            sprite.flipX = true;

    }

    private void SetAimAnimation(Vector3 self, Vector3 target)
    {
        animator.SetBool("Walking", false);
        float diffX = target.x - self.x;
        float diffY = target.y - self.y;

        if (diffY < 0)
            animator.SetBool("North", false);
        else if (diffY > 0)
            animator.SetBool("North", true);
    }

    public GameObject GetDestination()
    {
        return destination;
    }

    public void SetAlert(GameObject obj)
    {
        ChangeState(State.ALERT, obj);
    }

    public void SetCaution(GameObject obj)
    {
        ChangeState(State.CAUTION, obj);
    }

    private void ChangeState(State state, GameObject des)
    {
        if(state == State.ALERT)
        {
            alertShout.SetActive(true);
            cautionShout.SetActive(false);
            agent.speed = speed + 1;
            GameObject.Destroy(lastKnownPosition);
            timer = 0;
            SetAimAnimation(transform.position, des.transform.position);
            destination = des;
        }
        else if(state == State.CAUTION)
        {
            alertShout.SetActive(false);
            cautionShout.SetActive(true);
            lastKnownPosition = des;
            destination = des;
        } else
        {
            alertShout.SetActive(false);
            cautionShout.SetActive(false);
            agent.speed = speed;
            GameObject.Destroy(lastKnownPosition);
        }
            this.state = state;
    }

    public void Damage()
    {
        Destroy(transform.parent.gameObject);
    }

}
