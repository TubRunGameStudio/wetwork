using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthValue;


    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null) return;

        player.Heal(healthValue);
    }

}
