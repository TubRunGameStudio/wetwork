using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] int damage;

    public void Complete()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
            player.Damage(damage);

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
            enemy.Damage();
    }
}
