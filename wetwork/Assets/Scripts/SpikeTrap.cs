using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] int damage;


    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null) return;

        player.Damage(damage);
    }

}
