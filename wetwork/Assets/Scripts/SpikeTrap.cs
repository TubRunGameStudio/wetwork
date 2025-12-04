using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] int damage;


    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null) return;

        player.Damage(damage);
    }

}
