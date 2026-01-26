using UnityEngine;

public class RadioTower : MonoBehaviour, Interactable
{
    [SerializeField] GameObject tooltip;
    private GameController controller;
    private PlayerController player;
    private bool inRange;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = controller.PLAYER;
    }
    private void Update()
    {
        if(inRange)
            tooltip.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
        player.SetInteractable(this);
        tooltip.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
        player.SetInteractable(null);
        tooltip.SetActive(false);
    }

    public void Interact()
    {
        controller.SetMinimap(true);
    }
}
