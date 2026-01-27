using System.Threading;
using UnityEngine;

public class RadioRelay : MonoBehaviour, Interactable
{
    [SerializeField] GameObject tooltip;
    [SerializeField] RadioTower tower;
    [SerializeField] Animator animator;
    private GameController controller;
    private PlayerController player;
    private bool inRange;
    public bool active;

    void Start()
    {
        active = false;

        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = controller.PLAYER;
    }
    private void Update()
    {
        if (inRange)
            tooltip.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<PlayerController>(out var player))
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
        animator.SetBool("selfActive", true);
        tower.Refresh();
    }
}
