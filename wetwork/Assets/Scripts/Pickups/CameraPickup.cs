using UnityEngine;

public class CameraPickup : MonoBehaviour
{
    [SerializeField] int healthValue;


    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null) return;

        player.Pickup(Camera.NAME, 5);
        Destroy(gameObject);
    }

}
