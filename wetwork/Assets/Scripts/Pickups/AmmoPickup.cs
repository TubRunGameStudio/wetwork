using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public enum Ammo { CAMERA, GRENADE};
    [SerializeField] Ammo ammoType;
    [SerializeField] int ammount;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null) return;
        string weapon = ammoType.ToString();
        player.Pickup(weapon, ammount);
        Destroy(gameObject);
    }

}
