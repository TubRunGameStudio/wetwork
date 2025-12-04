using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int maxHealth;
    [SerializeField] PlayerHealthBar healthBar;

    private Rigidbody2D rb;
    private float movementX;
    private float movementY;

    private int health;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        health = maxHealth;
        healthBar.setHealth(health);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x * 1.6f;
        movementY = movementVector.y * .9f;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, movementY);
        rb.linearVelocity = movement * speed;
    }

    public void Heal(int heal)
    {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;

        healthBar.setHealth(health);
    }
    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);

        healthBar.setHealth(health);
    }
}
