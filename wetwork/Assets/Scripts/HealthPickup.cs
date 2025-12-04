using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthValue;


    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null) return;

        player.Heal(healthValue);
        Destroy(gameObject);
    }

}
