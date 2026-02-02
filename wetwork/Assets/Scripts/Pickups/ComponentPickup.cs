using UnityEngine;
using static PlayerInventory;

public class ComponentPickup : MonoBehaviour
{
    [SerializeField] ComponentType componentType;
    [SerializeField] int amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInventory player = collision.GetComponent<PlayerInventory>();

        if (player == null) return;
        player.Pickup(componentType, amount);

        Destroy(gameObject);
    }
}
