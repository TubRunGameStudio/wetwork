using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RadioRelay : MonoBehaviour, Interactable
{
    [SerializeField] GameObject tooltip;
    [SerializeField] RadioTower tower;
    [SerializeField] Animator animator;
    private GameController controller;
    private PlayerController player;
    private bool inRange;
    public bool active;
    private bool firstUpdate = false;

    void Start()
    {
        float towerDistance = Vector3.Distance(transform.position, tower.transform.position);
        if (towerDistance > RadioTower.RANGE)
            throw new RadioRangeException($"Tower at position {transform.position} is {towerDistance} distance away from tower, greater than range {RadioTower.RANGE}");

        active = false;

        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = controller.PLAYER;

        active = SceneState.GetRelay(SceneState.GetFullPathName(gameObject));

    }
    private void Update()
    {
        if (active && !firstUpdate)
        {
            animator.SetBool("selfActive", true);
            tower.Refresh();
            firstUpdate = true;
        }

        if (inRange)
            tooltip.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<PlayerController>(out var player))
            return;

        if (active)
            return;

        inRange = true;
        player.SetInteractable(this);
        tooltip.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<PlayerController>(out var player))
            return;

        inRange = false;
        player.SetInteractable(null);
        tooltip.SetActive(false);
    }

    public void Interact()
    {
        active = true;
        SceneState.SetRelay(SceneState.GetFullPathName(gameObject), active);
        animator.SetBool("selfActive", true);
        tower.Refresh();
    }
    public void AllActive()
    {
        animator.SetBool("allActive", true);
    }

    private class RadioRangeException : System.Exception
    {
        public RadioRangeException() 
        { }

        public RadioRangeException(string message)
            : base(message)
        { }

        public RadioRangeException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
